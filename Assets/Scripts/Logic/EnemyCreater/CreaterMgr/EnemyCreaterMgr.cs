using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class EnemyCreaterMgr : MonoBehaviour,IUpdate
{
    private AllEnemyData _allEnemyData;
    private EnemyTrajectoryDataMgr _trajectoryData;
    private EnemyCreaterConfigData _createrData;

    private ISubEnemyCreaterMgr[] _subMgrs =
    {
        new PlaneEnemyCreaterMgr(), 
        new MissileCreaterMgr()
    };

    public void Init()
    {
        foreach (var mgr in _subMgrs)
        {
            mgr.Init();
        }
        InitTrajectoryData();
        InitEnemyData();
        InitCreaterData();
        MessageMgr.Single.AddListener(MsgEvent.EVENT_START_GAME,Spawn);
        LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
    }

    private void OnDestroy()
    {
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_START_GAME,Spawn);
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
    }

    private void InitEnemyData()
    {
        LoadMgr.Single.LoadConfig(Paths.CONFIG_ENEMY, (value) =>
        {
            string json = (string) value;
            _allEnemyData = JsonMapper.ToObject<AllEnemyData>(json);
            Callback();
        });
    }

    private void InitTrajectoryData()
    {
        LoadMgr.Single.LoadConfig(Paths.CONFIG_ENEMY_TRAJECTORY, (value) =>
        {
            var json = (string) value;
            var dic = JsonUtil.JsonConvertToDic(json);
            _trajectoryData = new EnemyTrajectoryDataMgr();
            _trajectoryData.TrajectoryDatas = dic;

            Callback();
        });
    }
    
    private void InitCreaterData()
    {
        LoadMgr.Single.LoadConfig(Paths.CONFIG_LEVEL_ENEMY_DATA, (value) =>
        {
            string json = (string) value;
            _createrData = JsonMapper.ToObject<EnemyCreaterConfigData>(json);
            Callback();
        });
    }

    private void Callback()
    {
        if(_allEnemyData == null || _trajectoryData == null || _createrData == null)
            return;
        InitCreater(_allEnemyData,_trajectoryData,_createrData);
        
    }

    private void InitCreater(AllEnemyData enemyData,EnemyTrajectoryDataMgr trajectoryData,EnemyCreaterConfigData data)
    {
        var levelId = GameModel.Single.SelectedLevel;
        var levelData = data.LevelDatas[levelId];
        
        foreach (var mgr in _subMgrs)
        {
            mgr.InitCreater(transform,enemyData,trajectoryData,levelData);
        }
    }

    private void Spawn(object[] paras)
    {
        foreach (var mgr in _subMgrs)
        {
            mgr.Spawn();
        }
    }
   

    public int Times { get; set; }

    public int UpdateTimes
    {
        get { return 30; }
    }

    public void UpdateFun()
    {
        foreach (var mgr in _subMgrs)
        {
            mgr.UpdateFun();
        }
    }

   
}