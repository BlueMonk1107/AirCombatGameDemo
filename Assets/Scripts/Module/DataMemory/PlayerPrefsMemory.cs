using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerPrefsMemory : IDataMemory
{
    private Dictionary<Type, Func<string, object>> _dataGetters = new Dictionary<Type, Func<string, object>>()
    {
        {typeof(int), (key) => PlayerPrefs.GetInt(key, 0)},
        {typeof(string), (key) => PlayerPrefs.GetString(key, "")},
        {typeof(float), (key) => PlayerPrefs.GetFloat(key, 0)}
    };
    private Dictionary<Type, Action<string, object>> _dataSetters = new Dictionary<Type, Action<string, object>>()
    {
        {typeof(int), (key,value) => PlayerPrefs.SetInt(key, (int)value)},
        {typeof(string), (key,value) => PlayerPrefs.SetString(key,(string)value)},
        {typeof(float), (key,value) => PlayerPrefs.SetFloat(key, (float)value)}
    };

    public T Get<T>(string key)
    {
        Type type = typeof(T);
        var converter = TypeDescriptor.GetConverter(type);

        if (_dataGetters.ContainsKey(type))
        {
            return (T) converter.ConvertTo(_dataGetters[type](key), type);
        }
        else
        {
            Debug.LogError("当前数据存储中无此类型数据，类型名称为："+typeof(T).Name);
            return default(T);
        }
    }

    public void Set<T>(string key,T value)
    {
        Type type = typeof(T);

        if (_dataSetters.ContainsKey(type))
        {
            _dataSetters[type](key, value);
        }
        else
        {
            Debug.LogError("当前数据存储中无此类型数据，类型名称为："+typeof(T).Name
                                                 +"所以无法存储数据，key:"+key+" value:"+value);
        }
    }

    public void Clear(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
