using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ABLoader : ILoader
{
    public GameObject LoadPrefab(string path)
    {
        throw new NotImplementedException();
    }

    public GameObject LoadPrefabAndInstantiate(string path, Transform parent = null)
    {
        throw new NotImplementedException();
    }

    public void LoadConfig(string path, Action<object> complete)
    {
        throw new NotImplementedException();
    }

    public T Load<T>(string path) where T : Object
    {
        throw new NotImplementedException();
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        throw new NotImplementedException();
    }
}