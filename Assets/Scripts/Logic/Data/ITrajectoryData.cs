using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTrajectoryDataMgr
{
    public Dictionary<TrajectoryType, ITrajectoryData[]> TrajectoryDatas;
    private int _id;
    private Func<TrajectoryType,ITrajectoryData> _getAction;

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

    public ITrajectoryData GetData(TrajectoryType type)
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

    private ITrajectoryData GetRandomData(TrajectoryType type)
    {
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

    private ITrajectoryData GetOneData(TrajectoryType type)
    {
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

