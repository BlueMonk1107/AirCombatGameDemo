using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader
{
    GameObject LoadPrefab(string path);
    GameObject LoadPrefabAndInstantiate(string path, Transform parent = null);
    void LoadConfig(string path, Action<object> complete);
    T Load<T>(string path) where T : UnityEngine.Object;
    T[] LoadAll<T>(string path) where T : UnityEngine.Object;
}
