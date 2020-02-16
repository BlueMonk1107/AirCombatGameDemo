using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourceLoader : ILoader
{
    public GameObject LoadPrefab(string path)
    {
        var prefab = Resources.Load<GameObject>(path);

        return prefab;
    }

    public GameObject LoadPrefabAndInstantiate(string path, Transform parent = null)
    {
        var prefab = LoadPrefab(path);
        var temp = Object.Instantiate(prefab, parent);
        return temp;
    }

    public T Load<T>(string path) where T : Object
    {
        var sprite = Resources.Load<T>(path);
        if (sprite == null)
        {
            Debug.LogError("未找到对应图片，路径：" + path);
            return null;
        }

        return sprite;
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        var sprites = Resources.LoadAll<T>(path);
        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("当前路径下未找到对应资源，路径：" + path);
            return null;
        }

        return sprites;
    }

    public void LoadConfig(string path, Action<object> complete)
    {
        CoroutineMgr.Single.ExecuteOnce(Config(path, complete));
    }

    private IEnumerator Config(string path, Action<object> complete)
    {
        if (Application.platform != RuntimePlatform.Android)
            path = "file://" + path;

        var www = new WWW(path);
        yield return www;

        if (www.error != null)
        {
            Debug.LogError("加载配置错误，路径为：" + path);
            yield break;
        }

        complete(www.text);
        //Debug.Log("文件加载成功，路径为：" + path);
    }
}