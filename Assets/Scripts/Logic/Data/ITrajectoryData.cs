using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTrajectoryData
{
    public Dictionary<TrajectoryType,StraightTrajectoryData[]> TrajectoryDatas;
    private TrajectoryType _type;
    private int _id;
    private Func<StraightTrajectoryData> _getAction;

    public void Init(TrajectoryType type,int id)
    {
        _type = type;
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

    public ITrajectoryData GetData()
    {
        if (_getAction == null)
        {
            Debug.LogError("当前数据为初始化，请先调用Init方法");
            return null;
        }
        else
        {
            return _getAction();
        }
    }

    private StraightTrajectoryData GetRandomData()
    {
        int count = TrajectoryDatas[_type].Length;
        if (count > 0)
        {
            int index = Random.Range(0, count);
            return TrajectoryDatas[_type][index];
        }
        else
        {
            Debug.LogError("当前的轨迹数组长度为0");
            return null;
        }
    }

    private StraightTrajectoryData GetOneData()
    {
        if (_id < TrajectoryDatas[_type].Length)
        {
            return TrajectoryDatas[_type][_id];
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

