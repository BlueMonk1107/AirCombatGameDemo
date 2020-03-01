using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemyCreater : MonoBehaviour,IEnemyCreater
{
    private MissileCreaterData _data;
    private bool _isSpawning;
    private int _endMissileNum;
    private int _spawnedNum;
    
    public void Init(ICreaterData data, AllEnemyData enemyData, EnemyTrajectoryDataMgr trajectoryData)
    {
        if (data is MissileCreaterData)
        {
            _data = data as MissileCreaterData;
        }
        else
        {
            Debug.LogError("当前传入参数错误，参数类型为："+data);
            return;
        }

        _isSpawning = false;
        _endMissileNum = 0;
        _spawnedNum = 0;
    }

    public float GetSpawnRatio()
    {
        if (_data == null)
        {
            Debug.LogError("当前数据未初始化");
            return 1;
        }
        else
        {
            return GetSpawnNum() / (float)GetSpawnTotalNum();
        }
    }

    public int GetSpawnNum()
    {
        return _spawnedNum;
    }

    public int GetSpawnTotalNum()
    {
        if (_data == null)
        {
            Debug.LogError("当前数据未初始化");
            return 0;
        }
        else
        {
            return _data.SpwanCount;
        }
    }

    public void Spawn()
    {
        if (!_isSpawning)
        {
            _isSpawning = true;
            SpawnNew();
        }
    }

    private void SpawnNew()
    {
        _spawnedNum++;
        var missileGo = PoolMgr.Single.Spawn(Paths.PREFAB_ENEMY_MISSILE);
        MissileView missile = missileGo.AddOrGet<MissileView>();
        missile.Init((float)_data.X,_data.NumOfWarning,(float)_data.EachWarningTime,(float)_data.Speed);
        missile.AddEndListener(MissileEnd);
    }

    private void MissileEnd()
    {
        _endMissileNum++;

        if (!IsEnd())
        {
            SpawnNew();
        }

        _isSpawning = !IsEnd();
    }

    public bool IsEnd()
    {
        return _endMissileNum >= _data.SpwanCount;
    }

    public bool IsSpawning()
    {
        return _isSpawning;
    }
}
