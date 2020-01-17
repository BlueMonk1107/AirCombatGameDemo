using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PoolMgr : IInit
{
    private static PoolMgr _single;
    private static GameObject _selfGo;
    private Action _initComplete;

    private Dictionary<string, GameObjectPool> _pools;

    public static PoolMgr Single
    {
        get
        {
            if (GameStateModel.Single.CurrentScene == SceneName.Game)
            {
                if (_single == null)
                {
                    _single = new PoolMgr();
                    _selfGo = new GameObject("PoolMgr");
                }

                return _single;
            }

            Debug.LogError("非游戏场景无法使用PoolMgr");
            return null;
        }
    }

    public async void Init()
    {
        _pools = new Dictionary<string, GameObjectPool>();
        var config = new PoolConfig();
        GameObject temp = null;
        GameObjectPool pool = null;
        foreach (var data in config.Data)
        {
            temp = LoadMgr.Single.LoadPrefab(data.Path);
            pool = new GameObjectPool();
            pool.Init(temp, _selfGo.transform, data.PreloadCount, data.AutoDestroy);
            _pools.Add(data.Path, pool);
            await Task.Delay(100);
        }

        if (_initComplete != null)
            _initComplete();
    }

    public void Init(Action callBack)
    {
        _initComplete = callBack;
        Init();
    }

    public GameObject Spawn(string path)
    {
        if (_pools.ContainsKey(path))
        {
            return _pools[path].Spawn();
        }

        Debug.LogError("当前预制没有在池的管理中，预制路径:" + path);
        return null;
    }

    public bool Despawn(GameObject go)
    {
        var goName = go.name.Replace("(Clone)", "");

        foreach (var pair in _pools)
        {
            if (pair.Key.Contains(goName))
            {
                pair.Value.Despawn(go);
                return true;
            }
        }

        return false;
    }

    public int GetActiveNum(string path)
    {
        if (_pools.ContainsKey(path))
        {
            return _pools[path].GetActiveNum();
        }
        else
        {
            Debug.LogError("当前调用内存池并不存在，path:"+path);
            return 0;
        }
    }
}