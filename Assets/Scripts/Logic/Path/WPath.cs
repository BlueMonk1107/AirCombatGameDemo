using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPath : PathBase {
    private enum StartDirection
    {
        LEFT,
        RIGHT
    }
    private float _leftX;
    private float _rightX;
    private float _offsetY;
    private List<Vector3> _startPosOffset;
    private List<float> _xPos = new List<float>();
    private List<VTrajectory> _wTrajectory;
    private float _leftInitX;
    private float _rightInitX;
    private int _vCount;
    private StartDirection _startDirection;
    
    public override void Init(Transform trans,ITrajectoryData data)
    {
        base.Init(trans,data);
        _startDirection = StartDirection.RIGHT;
        
        _state = PathState.ENTER;
        _vCount = 2;
        InitData();
        InitXRange();
        var datas = InitTrajectoryData(data);
        InitVTrajectory(datas);
    }

    private void InitVTrajectory(ITrajectoryData[] datas)
    {
        _wTrajectory = new List<VTrajectory>();
        VTrajectory temp = null;
        foreach (var data in datas)
        {
            temp = new VTrajectory();
            temp.Init(data);
            _wTrajectory.Add(temp);
            
        }
    }

    private void InitData()
    {
        var halfWidth = _trans.GetComponent<SpriteRenderer>().bounds.size.x/2;
        _leftX = GameUtil.GetCameraMin().x + halfWidth;
        _rightX = GameUtil.GetCameraMax().x - halfWidth;
        _offsetY = (GameUtil.GetCameraMax().y - GameUtil.GetCameraMin().y) * 0.8f;
        _leftInitX = GameUtil.GetCameraMin().x - halfWidth;
        _rightInitX = GameUtil.GetCameraMax().x + halfWidth;
    }
    
    private void InitXRange()
    {
        float offsetX = (_rightX - _leftX) / (_vCount*2);
        int count = GetPointCount();
        for (int i = 0; i < count; i++)
        {
            _xPos.Add(_leftX + offsetX*i);
        }
    }

    private int GetPointCount()
    {
        int count = 3;
        for (int i = 0; i < _vCount - 1; i++)
        {
            count += 2;
        }

        return count;
    }

    private ITrajectoryData[] InitTrajectoryData(ITrajectoryData data)
    {
        VTrajectoryData source = null;
        if (data is VTrajectoryData)
        {
            source = data as VTrajectoryData;
            source.Angle = -source.Angle;
        }
        else
        {
            Debug.LogError("当前传入数据类型错误，传入类型为："+data);
        }
        
        ITrajectoryData[] datas = new ITrajectoryData[_vCount];
        VTrajectoryData temp = null;
        for (int i = 0; i < _vCount; i++)
        {
            temp = new VTrajectoryData();
            temp.Angle = source.Angle;
            temp.XPos = new float[3];
            temp.XPos[0] = _xPos[i*2];
            temp.XPos[1] = _xPos[i*2+1];
            temp.XPos[2] = _xPos[i*2+2];
            datas[i] = temp;
        }

        return datas;
    }

    public override Vector3 GetPos(int id)
    {
        if (id > 0)
        {
            Debug.LogError("W阵型只支持飞机单飞");
            return Vector3.zero;
        }
        
        return new Vector3(_leftInitX, GameUtil.GetCameraMin().y + _offsetY, _trans.position.z);;
    }

    public override Vector2 GetDirection()
    {
        switch (_state)
        {
            case PathState.ENTER:
                EndEnterState();
                return EnterDirection();
            case PathState.FORWARD_MOVING:
            case PathState.BACK_MOVING:
                SetMovingState();
                return MovingDirection(GetBaseDirection());
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void EndEnterState()
    {
        if (_startDirection == StartDirection.RIGHT)
        {
            if (_trans.position.x >= _leftX)
            {
                _state = PathState.FORWARD_MOVING;
            }
        }
        else
        {
            if (_trans.position.x <= _rightX)
            {
                _state = PathState.BACK_MOVING;
            }
        }
       
    }

    private Vector2 EnterDirection()
    {
        if (_trans.position.x < _leftX)
        {
            return Vector2.right;
        }
        else if(_trans.position.x > _rightX)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private Vector2 GetBaseDirection()
    {
        foreach (VTrajectory trajectory in _wTrajectory)
        {
            if (trajectory.SetCurrentTrajectory(_trans.position.x))
            {
                return trajectory.GetDirection();
            }
        }
        
        return _wTrajectory[_wTrajectory.Count -1].GetDirection();
    }
    
    private Vector2 MovingDirection(Vector2 direction)
    {
        if (_state == PathState.BACK_MOVING)
        {
            if (_trans.position.x < _leftX)
            {
                return direction;
            }
            else
            {
                return direction.Reversal();
            }
        }
        else if(_state == PathState.FORWARD_MOVING)
        {
            if (_trans.position.x > _rightX)
            {
                return direction.Reversal();
            }
            else
            {
                return direction;
            }
        }
        else
        {
            return Vector2.zero;
        }
    }

    private void SetMovingState()
    {
        if (_trans.position.x < _leftX)
        {
            _state = PathState.FORWARD_MOVING;
        }
        else if(_trans.position.x > _rightX)
        {
            _state = PathState.BACK_MOVING;
        }
    }

    public override bool NeedMoveWithCamera()
    {
        return true;
    }
}
