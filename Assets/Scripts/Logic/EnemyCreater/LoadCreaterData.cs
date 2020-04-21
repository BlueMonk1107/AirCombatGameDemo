using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class LoadCreaterData : NormalSingleton<LoadCreaterData> {
    
    private AllEnemyData _allEnemyData;
    private EnemyTrajectoryDataMgr _trajectoryData;
    private EnemyCreaterConfigData _createrData;
    private Action<AllEnemyData, EnemyTrajectoryDataMgr, EnemyCreaterConfigData> _callBack;

    public void Init(Action<AllEnemyData,EnemyTrajectoryDataMgr,EnemyCreaterConfigData> callBack)
    {
        if (_allEnemyData != null && _trajectoryData != null && _createrData != null)
        {
            if(callBack != null)
                callBack(_allEnemyData,_trajectoryData,_createrData);
            
            return;
        }
        _callBack = callBack;
        InitTrajectoryData();
        InitEnemyData();
        InitCreaterData();
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
        
        if(_callBack != null)
            _callBack(_allEnemyData,_trajectoryData,_createrData);
        
    }
}
