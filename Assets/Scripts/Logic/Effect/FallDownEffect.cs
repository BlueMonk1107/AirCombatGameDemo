using System;
using UnityEngine;

public class FallDownEffect : IEffect,IUpdate
{
    private float _slowSpeed, _speedMin;
    private MoveComponent _move;
    private float _speed;
    
    public void Init(Transform transform,float upSpeed,float slowSpeed,float speedMin)
    {
        _speed = upSpeed;
        _slowSpeed = slowSpeed;
        _speedMin = speedMin;
        _move = transform.gameObject.AddOrGet<MoveComponent>();
    }
    
    public void Begin()
    {
        LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
    }

    public void Stop(Action callBack)
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
        if (callBack != null)
            callBack();
    }

    public void Hide()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
    }

    public void Clear()
    {
        _speed = 0;
        _slowSpeed = 0;
        _speedMin = 0;
    }

    public int Times { get; set; }
    public int UpdateTimes { get; }
    public void UpdateFun()
    {
        if (_speed < _speedMin)
        {
            _speed = _speedMin;
        }
        else
        {
            _speed -= _slowSpeed * Time.deltaTime;
        }
        
        _move.Init(_speed);
        _move.Move(Vector2.up);
    }
}