using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class EnemyCreaterMgr : MonoBehaviour,IUpdate
{
    private int _elitesCount;
    private int _normalCount;
    private AllEnemyData _allEnemyData;
    private EnemyTrajectoryData _trajectoryData;
    private EnemyCreaterConfigData _createrData;
    private List<EnemyCreater> _creaters;

    private int _enemyActiveNumMax;

    public void Init()
    {
        _creaters = new List<EnemyCreater>();
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
            _trajectoryData = new EnemyTrajectoryData();
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

    private void InitCreater(AllEnemyData enemyData,EnemyTrajectoryData trajectoryData,EnemyCreaterConfigData data)
    {
        var levelId = GameModel.Single.SelectedLevel;
        var levelData = data.LevelDatas[levelId];
        foreach (var createrData in levelData.CreaterDatas)
        {
            SpawnCreater(createrData,enemyData,trajectoryData);
            InitData(createrData);
        }

        _enemyActiveNumMax = levelData.EnemyNumMax;
    }

    private void SpawnCreater(CreaterData data,AllEnemyData enemyData,EnemyTrajectoryData trajectoryData)
    {
        var go = new GameObject();
        var creater = go.AddComponent<EnemyCreater>();
        creater.Init(data, enemyData, trajectoryData);
        go.transform.SetParent(transform);
        _creaters.Add(creater);
    }

    private void InitData(CreaterData data)
    {
        int totoalNum = data.QueuePlaneNum * data.QueueNum;
        switch (data.Type)
        {
            case EnemyType.Normal:
                _normalCount += totoalNum;
                break;
            case EnemyType.Elites:
                _elitesCount += totoalNum;
                break;
        }
    }

    private void Spawn(object[] paras)
    {
        var creater = GetNeedSpawnCreater();
        
        if(creater != null)
            creater.Spawn();
    }

    private EnemyCreater GetNeedSpawnCreater()
    {
        EnemyCreater temp = null;
        foreach (EnemyCreater creater in _creaters)
        {
            if (temp == null || temp.GetSpawnRatio() > creater.GetSpawnRatio())
            {
                if (!creater.IsSpawning())
                {
                    temp = creater;
                }
            }
        }

        return temp;
    }

    public int Times { get; set; }

    public int UpdateTimes
    {
        get { return 30; }
    }

    public void UpdateFun()
    {
        if(!GameStateModel.Single.IsGaming)
            return;

        if (PoolMgr.Single.GetActiveNum(Paths.PREFAB_PLANE) < _enemyActiveNumMax)
        {
            Spawn(null);
        }
    }
}