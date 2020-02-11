using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPath : IPath
{
    public enum MoveDirection
    {
        UP_TO_DOWN,
        LEFT_TO_RIGHT,
        RIGHT_TO_LEFT
    }

    private IEnterPath _enter;
    private Vector3 _initPos;

    public void Init(Transform trans, ITrajectoryData trajectory)
    {
    }

    public void InitByOffsetY(Transform trans,float x,float offsetY,MoveDirection direction)
    {
        _enter = GetEnter(direction);
        if(_enter == null)
            return;
        _enter.Init(trans,offsetY);
        _initPos = _enter.InitPos(x);
    }

    public void InitByRatio(Transform trans,float x,float yRatio,MoveDirection direction)
    {
        float offsetY = (GameUtil.GetCameraMax().y - GameUtil.GetCameraMin().y) * yRatio;
        InitByOffsetY(trans, x, offsetY, direction);
    }

    private IEnterPath GetEnter(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.UP_TO_DOWN:
                return new UpToDown();
            case MoveDirection.LEFT_TO_RIGHT:
                return new LeftToRight();
            case MoveDirection.RIGHT_TO_LEFT:
                return new RightToLeft();
            default:
                Debug.LogError("当前类型未进行配置，名称为："+direction);
                return null;
        }
    }

    public Vector3 GetInitPos(int id)
    {
        return _initPos;
    }

    public Vector2 GetDirection()
    {
        if(_enter == null)
            return Vector2.zero;
        
        return _enter.EnterDirection();
    }

    public bool NeedMoveWithCamera()
    {
        return true;
    }
}

public interface IEnterPath
{
    void Init(Transform trans,float offsetY);
    Vector3 InitPos(float x);
    Vector2 EnterDirection();
}

public class UpToDown : IEnterPath
{
    private Transform _trans;
    private float _topInitY;
    private float _offsetY;
    private Vector3 _initPos;
    
    public void Init(Transform trans,float offsetY)
    {
        _trans = trans;
        _offsetY = offsetY;
        
        var size = _trans.GetComponent<SpriteRenderer>().bounds.size;
        _topInitY = GameUtil.GetCameraMax().y + size.y / 2;
    }
    
    public Vector3 InitPos(float x)
    {
        _initPos = new Vector3(x,_topInitY,_trans.position.z);

        return _initPos;
    }

    public Vector2 EnterDirection()
    {
        if (_trans.position.y > GetTargetPos().y)
        {
            return Vector2.down;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private Vector3 GetTargetPos()
    {
        return  new Vector3(_initPos.x,GameUtil.GetCameraMin().y + _offsetY,_initPos.z);
    }
}

public class LeftToRight : IEnterPath
{
    private Transform _trans;
    private float _offsetY;
    private Vector3 _initPos;
    private float _leftInitX;
    private float _rightX;
    
    public void Init(Transform trans, float offsetY)
    {
        _trans = trans;
        _offsetY = offsetY;
        
        var size = _trans.GetComponent<SpriteRenderer>().bounds.size;
        var halfWidth = size.x / 2;
        _rightX = GameUtil.GetCameraMax().x - halfWidth;
        _leftInitX = GameUtil.GetCameraMin().x - halfWidth;
    }

    public Vector3 InitPos(float x)
    {
        _initPos = new Vector3(_leftInitX, GameUtil.GetCameraMin().y + _offsetY, _trans.position.z);
        return _initPos;
    }

    public Vector2 EnterDirection()
    {
        if (_trans.position.x > GetTargetPos().x)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.zero;
        }
    }
    
    private Vector3 GetTargetPos()
    {
        return  new Vector3(_rightX,_initPos.y,_initPos.z);
    }
}

public class RightToLeft : IEnterPath
{
    private Transform _trans;
    private float _offsetY;
    private Vector3 _initPos;
    private float _rightInitX;
    private float _leftX;
    
    public void Init(Transform trans, float offsetY)
    {
        _trans = trans;
        _offsetY = offsetY;
        var size = _trans.GetComponent<SpriteRenderer>().bounds.size;
        var halfWidth = size.x / 2;
        _leftX = GameUtil.GetCameraMin().x + halfWidth;
        _rightInitX = GameUtil.GetCameraMax().x + halfWidth;
        
    }

    public Vector3 InitPos(float x)
    {
        _initPos = new Vector3(_rightInitX, GameUtil.GetCameraMin().y + _offsetY, _trans.position.z);
        return _initPos;
    }

    public Vector2 EnterDirection()
    {
        if (_trans.position.x < GetTargetPos().x)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.zero;
        }
    }
    
    private Vector3 GetTargetPos()
    {
        return  new Vector3(_leftX,_initPos.y,_initPos.z);
    }
}