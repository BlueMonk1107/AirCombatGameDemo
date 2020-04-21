using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpeedEffect : IEffect,IUpdate
{
    private float _slowSpeed, _speedLimit;
    private MoveComponent _move;
    private float _speed;
    private Vector2 _axis;
    
    public void Init(Transform transform,Vector2 axis,float startSpeed,float slowSpeed,float speedLimit)
    {
        _axis = axis;
        _speed = startSpeed;
        _slowSpeed = slowSpeed;
        _speedLimit = speedLimit;
        if(_move == null)
            _move = transform.AddComponent<MoveComponent>();
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
        _speedLimit = 0;
    }

    public int Times { get; set; }
    public int UpdateTimes { get; }
    public void UpdateFun()
    {
        if(_move == null)
            return;
        if (_speed < _speedLimit)
        {
            _speed = _speedLimit;
        }
        else
        {
            _speed -= _slowSpeed * Time.deltaTime;
        }
        
        _move.Init(_speed);
        _move.Move(_axis);
    }
}
