using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class EnemyCreaterMgr : MonoBehaviour,IGameProcessTriggerEvent,IGamePrecessNormalEvent
{
    private ISubEnemyCreaterMgr[] _subMgrs =
    {
        new NormalCreaterMgr(), 
        new ElitesCreaterMgr(), 
        new BossCreaterMgr(), 
        new MissileCreaterMgr()
    };

    private Action<EnemyCreaterMgr> _dataComplete;
    private int _enemyActiveNumMax;

    public void Init(Action<EnemyCreaterMgr> dataComplete)
    {
        _dataComplete = dataComplete;
        foreach (var mgr in _subMgrs)
        {
            mgr.Init();
        }

        LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
        
        LoadCreaterData.Single.Init(InitCreater);
    }

    private void OnDestroy()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);

        foreach (var mgr in _subMgrs)
        {
            if (mgr is IDestroy)
            {
                (mgr as IDestroy).Destroy();
            }
        }
    }
    
    private void InitCreater(AllEnemyData enemyData,EnemyTrajectoryDataMgr trajectoryData,EnemyCreaterConfigData data)
    {
        var levelId = GameModel.Single.CurrentLevel;
        var levelData = data.LevelDatas[levelId];
        
        foreach (var mgr in _subMgrs)
        {
            mgr.InitCreater(transform,enemyData,trajectoryData,levelData);
        }

        _enemyActiveNumMax = levelData.EnemyNumMax;

        if (_dataComplete != null)
            _dataComplete(this);
    }

    public int GetActiveNumMax()
    {
        return _enemyActiveNumMax;
    }


    public int Times { get; set; }

    public int UpdateTimes
    {
        get { return 30; }
    }

    public List<GameProcessTriggerEvent> GetTriggerEvents()
    {
        List<GameProcessTriggerEvent> list = new List<GameProcessTriggerEvent>();
        foreach (var mgr in _subMgrs)
        {
            if (mgr is IGameProcessTriggerEvent)
            {
                var temp = mgr as IGameProcessTriggerEvent;
                list.AddRange(temp.GetTriggerEvents());
            }
        }

        return list;
    }

    public List<GameProcessNormalEvent> GetNormalEvents()
    {
        List<GameProcessNormalEvent> list = new List<GameProcessNormalEvent>();
        foreach (var mgr in _subMgrs)
        {
            if (mgr is IGamePrecessNormalEvent)
            {
                var temp = mgr as IGamePrecessNormalEvent;
                list.AddRange(temp.GetNormalEvents());
            }
        }

        return list;
    }
}