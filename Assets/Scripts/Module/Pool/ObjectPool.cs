using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectPool<T> where T : class,new()
{
    private List<T> _activeList;
    private List<T> _inactiveList;
    private float _freeTimeMax = 5;
    private DateTime _time;
    private bool _isDestroying;

    public void Init(int preloadCount, bool autoDestroy)
    {
         _activeList = new List<T>();
         _inactiveList = new List<T>();
         Preload(preloadCount);
         AutoDestroy(preloadCount,autoDestroy);
    }

    private void Preload(int preloadCount)
    {
        for (int i = 0; i < preloadCount; i++)
        {
            SpawnNew();
        }
    }

    private async void AutoDestroy(int preloadCount,bool autoDestroy)
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            var spendTime = (_time - DateTime.Now).Seconds;
            if (spendTime >= _freeTimeMax && !_isDestroying)
            {
                _isDestroying = true;
                StartDestroy(preloadCount);
            }
        }
    }
    
    private async void StartDestroy(int preloadCount)
    {
        while (_inactiveList.Count > preloadCount)
        {
            await Task.Delay(100);
            _inactiveList.RemoveAt(0);
        }

        _isDestroying = false;
    }
    
    public T Spawn()
    {
        _time = DateTime.Now;
        
        if (_inactiveList.Count > 0)
        {
            var o = _inactiveList[0];
            _activeList.Add(o);
            return o;
        }

        return SpawnNew();
    }

    private T SpawnNew()
    {
        var o = new T();
        _activeList.Add(o);
        return o;
    }

    public void Despawn(T o)
    {
        if (_activeList.Contains(o))
        {
            _activeList.Remove(o);
            _inactiveList.Add(o);
        }
    }
}
