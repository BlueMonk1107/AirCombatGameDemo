using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataMemory
{
    T Get<T>(string key);
    void Set<T>(string key,T value);
    void Clear(string key);
    void ClearAll();
}
