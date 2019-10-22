using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using LitJson;
using Object = System.Object;

public class JsonReader : IReader
{
    private JsonData _data;
    private JsonData _tempData;
    private Queue<KeysQueue> _keysQueues = new Queue<KeysQueue>();
    private KeysQueue _tempKeysQueue;

    public IReader this[string key]
    {
        get
        {
            if (_data == null || _tempKeysQueue != null)
            {
                if (_tempKeysQueue == null)
                {
                    _tempKeysQueue = new KeysQueue();
                }
                
                IKey keydata = new Key<string>();
                keydata.Set(key);
                _tempKeysQueue.Enqueue(keydata);

                return this;
            }
            InitTempData();
            _tempData = _tempData[key];
            return this;
        }
    }

    public IReader this[int key]
    {
        get
        {
            if (_data == null || _tempKeysQueue != null)
            {
                if (_tempKeysQueue == null)
                {
                    _tempKeysQueue = new KeysQueue();
                }
                
                IKey keydata = new Key<int>();
                keydata.Set(key);
                _tempKeysQueue.Enqueue(keydata);

                return this;
            }
            InitTempData();
            _tempData = _tempData[key];
            return this;
        }
    }

    private void InitTempData()
    {
        if (_tempData == null)
        {
            _tempData = _data;
        }
    }

    public JsonReader(string path)
    {
        GetTextForStreamingAssets(path, (json) =>
        {
            _data = JsonMapper.ToObject(json);
        });
    }

    public void Get<T>(Action<T> complete)
    {
        if (_tempKeysQueue != null)
        {
            _tempKeysQueue.OnComplete((data) =>
            {
                T value = GetValue<T>(data);
                complete(value);
            });
        }
        
        if (complete == null || _tempData == null)
        {
            _tempData = null;
            return;
        }

        T temp = GetValue<T>(_tempData);
        complete(temp);
    }

    private T GetValue<T>(JsonData data)
    {
        //这里涉及类型转换问题
        var converter = TypeDescriptor.GetConverter(typeof(T));
        return (T) converter.ConvertTo(data.ToString(), typeof(T));
    }

    public void GetTextForStreamingAssets(string path, Action<string> complete)
    {
        if (complete == null)
            return;

        string localPath = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            localPath = path;
        }
        else
        {
            localPath = "file:///" + path;
        }

        WWW www = new WWW(localPath); 

        if (www.error != null)
        {
            Debug.LogError("error : " + localPath);
            complete(null);
        }

        while (!www.isDone)
        {
        }

        Debug.Log("WWW.text=  " + www.text);
        complete(www.text);
    }
}

public class KeysQueue
{
    private Queue<IKey> _keys = new Queue<IKey>();
    private Action<JsonData> _complete;

    public void Enqueue(IKey key)
    {
        _keys.Enqueue(key);
    }
    
    public IKey Dequeue()
    {
        return _keys.Dequeue();
    }

    public void Clear()
    {
        _keys.Clear();
    }

    public void OnComplete(Action<JsonData> complete)
    {
        _complete = complete;
    }
}

public interface IKey
{
    void Set<T>(T key);
    T Get<T>();
}

public class Key<T> : IKey
{

    private object _key;

    public void Set<T>(T key)
    {
        _key = key;
    }

    public T Get<T>()
    {
        return (T)_key;
    }
}