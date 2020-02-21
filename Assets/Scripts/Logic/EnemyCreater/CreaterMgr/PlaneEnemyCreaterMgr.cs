using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneEnemyCreaterMgr : ISubEnemyCreaterMgr 
{
    public enum CreaterState
    {
        OTHER_START,
        OTHER_END,
        BOSS
    }
    
    private CreaterState _state;
    private int _enemyActiveNumMax;
    private int _spawnElitesLimit;

    public Dictionary<EnemyType, IPlaneEnemyMgr> _mgrs = new Dictionary<EnemyType, IPlaneEnemyMgr>()
    {
        {EnemyType.Normal, new NormalCreaterMgr()},
        {EnemyType.Elites,new ElitesCreaterMgr()},
        {EnemyType.Boss,new BossCreaterMgr()}
    };
    
    public void Init()
    {
        _state = CreaterState.OTHER_START;
        foreach (var mgr in _mgrs)
        {
            mgr.Value.Init();
        }
    }

    public void InitCreater(
        Transform              parent,
        AllEnemyData           enemyData, 
        EnemyTrajectoryDataMgr trajectoryData, 
        LevelData              levelData)
    {
        foreach (var createrData in levelData.PlaneCreaterDatas)
        {
            SpawnCreater(parent,createrData,enemyData,trajectoryData);
        }

        _enemyActiveNumMax = levelData.EnemyNumMax;
        ((ElitesCreaterMgr)_mgrs[EnemyType.Elites]).InitData(levelData,_mgrs[EnemyType.Normal]);
    }
    
    private void SpawnCreater(
        Transform              parent,
        PlaneCreaterData       data,
        AllEnemyData           enemyData,
        EnemyTrajectoryDataMgr trajectoryData)
    {
        var go = new GameObject();
        var creater = go.AddComponent<PlaneEnemyCreater>();
        creater.Init(data, enemyData, trajectoryData);
        go.transform.SetParent(parent);
        _mgrs[data.Type].AddCraterItem(creater);
    }

    private bool JudgeEnd()
    {
        foreach (var mgr in _mgrs)
        {
            if (mgr.Key == EnemyType.Boss)
                continue;

            if (!mgr.Value.JudgeEnd())
                return false;
        }

        return true;
    }

    public void Spawn()
    {
        foreach (var mgr in _mgrs)
        {
            if (mgr.Key == EnemyType.Boss)
                continue;

            mgr.Value.Spawn();
        }
    }
    
    private void SpawnBoss()
    {
        _mgrs[EnemyType.Boss].Spawn();
    }

    public void UpdateFun()
    {
        if(GameStateModel.Single.GameState == GameState.END)
            return;
        
        if(_state == CreaterState.BOSS)
            return;

        if (_state == CreaterState.OTHER_START)
        {
            if (PoolMgr.Single.GetActiveNum(Paths.PREFAB_PLANE) < _enemyActiveNumMax)
            {
                Spawn();
                if (JudgeEnd())
                {
                    _state = CreaterState.OTHER_END;
                }
            }
        }
        else if(_state == CreaterState.OTHER_END)
        {
            if (PoolMgr.Single.GetActiveNum(Paths.PREFAB_PLANE) == 0)
            {
                GameUtil.ShowWarnning();
                CoroutineMgr.Single.Delay(Const.WAIT_BOSS_TIME,SpawnBoss);
                _state = CreaterState.BOSS;
            }
        }
    }
    
   
}
