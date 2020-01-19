using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using LitJson;
using UnityEngine;

//jsonReader["planes"][0]["planeId"].Get<int>(()=>)
//jsonReader["planes"][0]["planeId"]
public class JsonReader : IReader
{
    private JsonData _data;
    private readonly Queue<KeyQueue> _keyQueues = new Queue<KeyQueue>();
    private KeyQueue _keys;
    private JsonData _tempData;

    public IReader this[string key]
    {
        get
        {
            if (!SetKey(key))
                try
                {
                    _tempData = _tempData[key];
                }
                catch (Exception e)
                {
                    Debug.LogError("在数据中无法找到对应键值，数据：" + _tempData.ToJson() + "  键值：" + key);
                }

            return this;
        }
    }

    public IReader this[int key]
    {
        get
        {
            if (!SetKey(key))
                try
                {
                    _tempData = _tempData[key];
                }
                catch (Exception e)
                {
                    Debug.LogError("在数据中无法找到对应键值，数据：" + _tempData.ToJson() + "  键值：" + key);
                }

            return this;
        }
    }

    public void Count(Action<int> callBack)
    {
        var success = SetKey<Action>(() =>
        {
            if (callBack != null)
                callBack(GetCount());
        });

        if (!success)
        {
            callBack(GetCount());
        }
        else
        {
            _keyQueues.Enqueue(_keys);
            _keys = null;
        }
    }

    public void Get<T>(Action<T> callBack)
    {
        if (_keys != null)
        {
            _keys.OnComplete(dataTemp =>
            {
                var value = GetValue<T>(dataTemp);
                ResetData();
                callBack(value);
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

        var data = GetValue<T>(_tempData);
        ResetData();
        callBack(data);
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

    private bool SetKey<T>(T key)
    {
        if (_data == null || _keys != null)
        {
            if (_keys == null)
                _keys = new KeyQueue();

            IKey keyData = new Key();
            keyData.Set(key);
            _keys.Enqueue(keyData);

            return true;
        }

        return false;
    }

    private int GetCount()
    {
        return _tempData.IsArray ? _tempData.Count : 0;
    }

    private void ExecuteKeysQueue()
    {
        if (_data == null)
            return;

        IReader reader = null;
        foreach (var keyQueue in _keyQueues)
        {
            foreach (var value in keyQueue)
                if (value is string)
                    reader = this[(string) value];
                else if (value is int)
                    reader = this[(int) value];
                else if (value is Action)
                    ((Action) value)();
                else
                    Debug.LogError("当前键值类型错误");

            keyQueue.Complete(_tempData);
        }
        
        _keyQueues.Clear();
    }

    private T GetValue<T>(JsonData data)
    {
        var converter = TypeDescriptor.GetConverter(typeof(T));

        try
        {
            if (converter.CanConvertTo(typeof(T)))
                return (T) converter.ConvertTo(data.ToString(), typeof(T));
            return (T) (object) data;
        }
        catch (Exception e)
        {
            Debug.LogError("当前类型转换出现问题，目标类型为：" + typeof(T).Name + "  data:" + data);
            return default(T);
        }
    }

    private void ResetData()
    {
        _tempData = _data;
    }
}

public class KeyQueue : IEnumerable
{
    private Action<JsonData> _complete;
    private readonly Queue<IKey> _keys = new Queue<IKey>();

    public IEnumerator GetEnumerator()
    {
        foreach (var key in _keys) yield return key.Get();
    }

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
}

public interface IKey
{
    Type KeyType { get; }
    void Set<T>(T key);
    object Get();
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