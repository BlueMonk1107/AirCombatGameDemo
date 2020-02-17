using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTrajectoryDataMgr
{
    public Dictionary<PathType, ITrajectoryData[]> TrajectoryDatas;
    private int _id;
    private Func<PathType,ITrajectoryData> _getAction;

    public void Init(int id)
    {
        _id = id;

        if (_id < 0)
        {
            _getAction = GetRandomData;
        }
        else
        {
            _getAction = GetOneData;
        }
    }

    public ITrajectoryData GetData(PathType type)
    {
        if (_getAction == null)
        {
            Debug.LogError("当前数据为初始化，请先调用Init方法");
            return null;
        }
        else
        {
            return _getAction(type);
        }
    }

    private ITrajectoryData GetRandomData(PathType type)
    {
        if (!TrajectoryDatas.ContainsKey(type))
            return null;
        
        int count = TrajectoryDatas[type].Length;
        if (count > 0)
        {
           
            int index = Random.Range(0, count);
            return TrajectoryDatas[type][index];
        }
        else
        {
            Debug.LogError("当前的轨迹数组长度为0");
            return null;
        }
    }

    private ITrajectoryData GetOneData(PathType type)
    {
        if (!TrajectoryDatas.ContainsKey(type))
            return null;
        
        if (_id < TrajectoryDatas[type].Length)
        {
            return TrajectoryDatas[type][_id];
        }
        else
        {
            Debug.LogError("当前ID不存在，id："+_id);
            return null;
        }
    }
}

public interface ITrajectoryData
{
    
}

public class StraightTrajectoryData : ITrajectoryData
{
    public double Angle;
}

public class VTrajectoryData : ITrajectoryData
{
    public double Angle;
    public float[] XPos;
}

public class EllipseData : ITrajectoryData
{
    public float XPos;
    public Vector2 StartPos;
    public Vector2 Center;
    public double YRatioInScreen;
    public double XRadius;
    public double YRadius;
    /// <summary>
    /// 指定椭圆形由多少的线段构成
    /// </summary>
    public int Precision;
}

public class RotateData : ITrajectoryData
{
    public double StartAngle;
    public double EndAngle;
    public double RotateOffset;
}

