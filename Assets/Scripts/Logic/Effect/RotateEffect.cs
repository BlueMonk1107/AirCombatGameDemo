using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEffect : IEffect,IUpdate
{
    private float _offsetAngle;
    private Transform _transform;

    public void Init(Transform transform, float offsetAngle)
    {
        _offsetAngle = offsetAngle;
        _transform = transform;
    }
    
    public void Begin()
    {
        LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
    }

    public void Stop(Action callBack)
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
        Clear();
    }

    public void Hide()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
        Clear();
    }

    public void Clear()
    {
        _offsetAngle = 0;
        _transform = null;
    }

    public int Times { get; set; }
    public int UpdateTimes { get; }
    public void UpdateFun()
    {
        if(_transform == null)
            return;
        
        _transform.Rotate(Vector3.forward*_offsetAngle*Time.deltaTime);
    }
}
