using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCreaterMgr : ISubEnemyCreaterMgr,IGamePrecessNormalEvent  {

    private List<IEnemyCreater> _normalCreaters;

    public void Init()
    {
        _normalCreaters = new List<IEnemyCreater>();
    }

    public void InitCreater(Transform parent, AllEnemyData enemyData, EnemyTrajectoryDataMgr trajectoryData, LevelData levelData)
    {
        _normalCreaters = GameUtil.InitCreater(EnemyType.Normal,parent,enemyData,trajectoryData,levelData);  
    }

    private void Spawn()
    {
        var creater = GetValidCreater();
        if(creater != null)
            creater.Spawn();
    }
    
    private IEnemyCreater GetValidCreater()
    {
        return GetCreater(_normalCreaters);
    }
	
    private IEnemyCreater _temp;
    private IEnemyCreater GetCreater(List<IEnemyCreater> list)
    {
        _temp = null;
        foreach (IEnemyCreater creater in list)
        {
            if (_temp == null || _temp.GetSpawnRatio() > creater.GetSpawnRatio())
            {
                if (!creater.IsSpawning())
                {
                    _temp = creater;
                }
            }
        }

        return _temp;
    }
	
    public bool JudgeEnd()
    {
        foreach (IEnemyCreater creater in _normalCreaters)
        {
            if (!creater.IsEnd())
                return false;
        }
		
        return true;
    }

    public int GetSpawnNum()
    {
        int count = 0;
        foreach (IEnemyCreater creater in _normalCreaters)
        {
            count += creater.GetSpawnNum();
        }

        return count;
    }

    public List<GameProcessNormalEvent> GetNormalEvents()
    {
        var list = new List<GameProcessNormalEvent>();
        list.Add(GameUtil.GetNormalEvent(Spawn,GetSpawnNum,GetTotalNum()));
        return list;
    }

    private int GetTotalNum()
    {
        int total = 0;
        foreach (var creater in _normalCreaters)
        {
            total += creater.GetSpawnTotalNum();
        }

        return total;
    }
}