using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : NormalSingleton<DataMgr>,IDataMemory
{
    private IDataMemory _dataMemory;

    public DataMgr()
    {
        _dataMemory = new PlayerPrefsMemory();
    }

    public T Get<T>(string key)
    {
        return _dataMemory.Get<T>(key);
    }

    public void Set<T>(string key, T value)
    {
        _dataMemory.Set(key,value);
    }

    public void Clear(string key)
    {
        _dataMemory.Clear(key);
    }

    public void ClearAll()
    {
        _dataMemory.ClearAll();
    }
}
