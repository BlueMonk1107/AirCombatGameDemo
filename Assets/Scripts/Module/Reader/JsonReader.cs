using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using LitJson;
using UnityEngine;
using UnityEngine.Events;

//jsonReader["planes"][0]["planeId"].Get<int>(()=>)
//jsonReader["planes"][0]["planeId"]
public class JsonReader : IReader
{
    private JsonData _data;
    private JsonData _tempData;
    private KeyQueue _keys;
    private Queue<KeyQueue> _keyQueues = new Queue<KeyQueue>();
    
    public IReader this[string key]
    {
        get
        {
            if (!SetKey(key))
            {
                _tempData = _tempData[key];
            }
            return this;
        }
    }

    public IReader this[int key]
    {
        get
        {
            if (!SetKey(key))
            {
                _tempData = _tempData[key];
            }
            return this;
        }
    }

    private bool SetKey<T>(T key)
    {
        if (_data == null || _keys != null)
        {
            if(_keys == null)
                _keys = new KeyQueue();
                
            IKey keyData = new Key();
            keyData.Set(key);
            _keys.Enqueue(keyData);

            return true;
        }

        return false;
    }

    public void Get<T>(Action<T> callBack)
    {
        if (_keys != null)
        {
            _keys.OnComplete((dataTemp) =>
            {
                T value = GetValue<T>(dataTemp);
                callBack(value);
                ResetData();
            });
            
            _keyQueues.Enqueue(_keys);
            _keys = null;
            ExecuteKeysQueue();
            return;
        }
        
        if (callBack == null)
        {
            Debug.LogWarning("当前回调方法为空，不返回数据");
            ResetData();
            return;
        }

        T data = GetValue<T>(_tempData);
        callBack(data);
        ResetData();
    }

    private void ExecuteKeysQueue()
    {
        if(_data == null)
            return;

        IReader reader = null;
        foreach (KeyQueue keyQueue in _keyQueues)
        {
            foreach (object value in keyQueue)
            {
                if (value is string)
                {
                    reader = this[(string) value];
                }
                else if(value is int)
                {
                    reader = this[(int) value];
                }
                else
                {
                    Debug.LogError("当前键值类型错误");
                }
            }
            
            keyQueue.Complete(_tempData);
        }
    }

    private T GetValue<T>(JsonData data)
    {
        var converter = TypeDescriptor.GetConverter(typeof(T));

        try
        {
            if (converter.CanConvertTo(typeof(T)))
            {
                return (T) converter.ConvertTo(data.ToString(), typeof(T));
            }
            else
            {
                return (T) (object) data;
            }
        }
        catch (Exception e)
        {
           Debug.LogError("当前类型转换出现问题，目标类型为："+typeof(T).Name);
           return default(T);
        }
        
    }

    private void ResetData()
    {
        _tempData = _data;
    }

    public void SetData(object data)
    {
        if (data is string)
        {
            _data = JsonMapper.ToObject(data as string);
            ResetData();
            ExecuteKeysQueue();
        }
        else
        {
            Debug.LogError("当前传入数据类型错误，当前类只能解析json");
        }
    }

    public ICollection<string> Keys()
    {
        if (_tempData == null)
            return new string[0];
        
        return _tempData.Keys;
    }
}

public class KeyQueue : IEnumerable
{
    private Queue<IKey> _keys = new Queue<IKey>();
    private Action<JsonData> _complete;

    public void Enqueue(IKey key)
    {
        _keys.Enqueue(key);
    }
    
    public void Dequeue()
    {
        _keys.Dequeue();
    }

    public void Clear()
    {
        _keys.Clear();
    }

    public void Complete(JsonData data)
    {
        if (_complete != null)
            _complete(data);
    }

    public void OnComplete(Action<JsonData> complete)
    {
        _complete = complete;
    }
    
    public IEnumerator GetEnumerator()
    {
        foreach (IKey key in _keys)
        {
            yield return key.Get();
        }
    }
}

public interface IKey
{
    void Set<T>(T key);
    object Get();
    Type KeyType { get; }
}

public class Key : IKey
{
    private object _key;
    public Type KeyType { get; private set; }

    public void Set<T1>(T1 key)
    {
        _key = key;
    }

    public object Get()
    {
        return _key;
    }
}