using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class EnemyCreaterMgr : MonoBehaviour,IUpdate
{
    public enum CreaterState
    {
        OTHER_START,
        OTHER_END,
        BOSS
    }
    
    private AllEnemyData _allEnemyData;
    private EnemyTrajectoryDataMgr _trajectoryData;
    private EnemyCreaterConfigData _createrData;
    private List<IEnemyCreater> _normalCreaters;
    private List<IEnemyCreater> _elitesCreaters;
    private List<IEnemyCreater> _needSpawnCreaters;
    private IEnemyCreater _bossCreater;

    private int _enemyActiveNumMax;
    private int _spawnElitesLimit;
    private CreaterState _state;

    public void Init()
    {
        _state = CreaterState.OTHER_START;
        _needSpawnCreaters = new List<IEnemyCreater>();
        _normalCreaters = new List<IEnemyCreater>();
        _elitesCreaters = new List<IEnemyCreater>();
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
        
        foreach (var createrData in levelData.PlaneCreaterDatas)
        {
            SpawnCreater(createrData,enemyData,trajectoryData);
        }

        _enemyActiveNumMax = levelData.EnemyNumMax;
        _spawnElitesLimit = levelData.NormalDeadNumForSpawnElites;
    }

    private void SpawnCreater(PlaneCreaterData data,AllEnemyData enemyData,EnemyTrajectoryDataMgr trajectoryData)
    {
        var go = new GameObject();
        var creater = go.AddComponent<PlaneEnemyCreater>();
        creater.Init(data, enemyData, trajectoryData);
        go.transform.SetParent(transform);
        AddCraterItem(data,creater);
    }

    private void AddCraterItem(PlaneCreaterData data,IEnemyCreater item)
    {
        switch (data.Type)
        {
            case EnemyType.Normal:
                _normalCreaters.Add(item);
                break;
            case EnemyType.Elites:
                _elitesCreaters.Add(item);
                break;
            case EnemyType.Boss:
                _bossCreater = item;
                break;
            case EnemyType.Item:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Spawn(object[] paras)
    {
        var creater = GetNeedSpawnCreater();

        foreach (IEnemyCreater IEnemyCreater in creater)
        {
            if (IEnemyCreater != null)
            {
                IEnemyCreater.Spawn();
            } 
        }
    }

    private bool JudgeEnd()
    {
        foreach (IEnemyCreater creater in _normalCreaters)
        {
            if (!creater.IsEnd())
                return false;
        }

        foreach (IEnemyCreater creater in _elitesCreaters)
        {
            if (!creater.IsEnd())
                return false;
        }

        return true;
    }

    private List<IEnemyCreater> GetNeedSpawnCreater()
    {
        _needSpawnCreaters.Clear();
        _needSpawnCreaters.Add(GetNormalCreater());
        _needSpawnCreaters.Add(GetElitesCreater());

        return _needSpawnCreaters;
    }

    private int GetNormalSpawnNum()
    {
        int count = 0;
        foreach (IEnemyCreater creater in _normalCreaters)
        {
            count += creater.GetSpawnNum();
        }

        return count;
    }

    
    private IEnemyCreater GetNormalCreater()
    {
        return GetCreater(_normalCreaters);
    }

    private IEnemyCreater GetElitesCreater()
    {
        if (GetNormalSpawnNum() >= _spawnElitesLimit)
        {
            _spawnElitesLimit += _spawnElitesLimit;
            return GetCreater(_elitesCreaters);
        }
        else
        {
            return null;
        }
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
    

    public int Times { get; set; }

    public int UpdateTimes
    {
        get { return 30; }
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
                Spawn(null);
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

    private void SpawnBoss()
    {
        _bossCreater.Spawn();
    }
}