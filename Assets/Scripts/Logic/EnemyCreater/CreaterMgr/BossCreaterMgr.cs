using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreaterMgr : ISubEnemyCreaterMgr,IGameProcessTriggerEvent,IUpdate,IDestroy {
    
    private IEnemyCreater _bossCreater;
    private bool _start;
    
    public void Init()
    {
        _start = false;
        LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
    }
    
    public void Destroy()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
    }

    public void InitCreater(Transform parent, AllEnemyData enemyData, EnemyTrajectoryDataMgr trajectoryData, LevelData levelData)
    {
        _bossCreater = GameUtil.InitCreater(EnemyType.Boss,parent,enemyData,trajectoryData,levelData)[0];
    }

    public void Spawn()
    {
        var creater = GetValidCreater();
        if(creater != null)
            creater.Spawn();
    }

    public int Times { get; set; }

    public int UpdateTimes
    {
        get { return 30; }
    }

    public void UpdateFun()
    {
        if(!_start)
            return;
        
        if (PoolMgr.Single.GetActiveNum(Paths.PREFAB_PLANE) == 0)
        {
            GameUtil.ShowWarnning();
            CoroutineMgr.Single.Delay(Const.WAIT_BOSS_TIME,Spawn);
            _start = false;
        }
    }

    private IEnemyCreater GetValidCreater()
    {
        return _bossCreater;
    }

    private bool JudgeEnd()
    {
        return GameStateModel.Single.GameState == GameState.END;
    }

    public List<GameProcessTriggerEvent> GetTriggerEvents()
    {
        var list = new List<GameProcessTriggerEvent>();
        GameProcessTriggerEvent e = new GameProcessTriggerEvent();
        e.AddEvent(1,Start,false,JudgeEnd);
        list.Add(e);
        return list;
    }

    private void Start()
    {
        _start = true;
    }

   
}
