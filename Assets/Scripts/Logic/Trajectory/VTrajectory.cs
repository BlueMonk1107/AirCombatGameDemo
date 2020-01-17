using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTrajectory : ITrajectory
{
    private VTrajectoryData _data;
    public StraightTrajectory[] _straights;
    private float[] _xPos = new float[3];
    private StraightTrajectory _currentTrajectory;
    
    public void Init(ITrajectoryData data)
    {
        if (data is VTrajectoryData)
        {
            _data = data as VTrajectoryData;
        }
        else
        {
            Debug.LogError("当前传入数据类型错误，传入类型为："+data);
        }

        _xPos = _data.XPos;
        InitStraights(_data);
        _currentTrajectory = _straights[0];
    }

    private void InitStraights(VTrajectoryData data)
    {
        _straights = new StraightTrajectory[2];
        _straights[0] = new StraightTrajectory();
        _straights[0].Init((float)data.Angle);
        _straights[1] = new StraightTrajectory();
        _straights[1].Init(-(float)data.Angle);
    }

    public float GetY(float x, Vector2 startPos)
    {
        _currentTrajectory = _straights[GetTrajectoryIndex(x)];
        return _currentTrajectory.GetY(x, startPos);
    }

    private int GetTrajectoryIndex(float x)
    {
        if (x < _xPos[1])
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public float GetX(float y, Vector2 startPos)
    {
        Debug.LogError("W轨迹的GetX方法无法获取正确的值");
        return 0;
    }

    public bool SetCurrentTrajectory(float x)
    {
        if (x >= _xPos[0] && x <= _xPos[2])
        {
            _currentTrajectory = _straights[GetTrajectoryIndex(x)];
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 GetDirection()
    {
        return _currentTrajectory.GetDirection();
    }

    public float GetZRotate()
    {
        return 0;
    }
}