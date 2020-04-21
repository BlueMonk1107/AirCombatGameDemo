using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneEnemyCreater : MonoBehaviour,IEnemyCreater,IUpdate
{
    private int _id;
    private Sprite _sprite;
    private EnemyType _type;
    private EnemyData _enemyData;
    /// <summary>
    /// 已经生成的队列数量
    /// </summary>
    private int _spawnQueueNum;
    private PlaneCreaterData _data;
    private EnemyTrajectoryDataMgr _trajectoryData;
    /// <summary>
    /// 当前生成队列中最后一个敌机
    /// </summary>
    private PlaneEnemyView _lastEnemy;

    public void Init(ICreaterData data,AllEnemyData enemyData,EnemyTrajectoryDataMgr trajectoryData)
    {
        if (data is PlaneCreaterData)
        {
            _data = data as PlaneCreaterData;
        }
        else
        {
            Debug.LogError("传入数据类型错误，类型为:"+data);
            return;
        }
        _trajectoryData = trajectoryData;
        InitPos((float) _data.X);
        InitEnemyData(_data,enemyData,trajectoryData);
        LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
    }

    private void OnDestroy()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
    }

    private void InitPos(float x)
    {
        var yMax = GameUtil.GetCameraMax().y;
        var xMin = GameUtil.GetCameraMin().x;
        var xMax = GameUtil.GetCameraMax().x;

        var pos = new Vector3();
        pos.x = x;
        pos.y = yMax;
        pos.z = (float) GameLayer.PLANE;

        if (pos.x < xMin)
            pos.x = xMin;
        else if (pos.x > xMax) pos.x = xMax;

        transform.position = pos;
    }

    private void InitEnemyData(PlaneCreaterData data,AllEnemyData enemyData,EnemyTrajectoryDataMgr trajectoryData)
    {
        _id = Random.Range(data.IdMin, data.IdMax + 1);
        _type = data.Type;
        var spriteName = string.Format(Const.ENEMY_PREFIX, _type, _id);
        _sprite = LoadMgr.Single.Load<Sprite>(Paths.PICTURE_ENEMY_FOLDER + spriteName);
        
        var allData = enemyData.GetData(data.Type);
        _enemyData = allData.FirstOrDefault(u => u.id == _id);
        if (_enemyData == null)
        {
            Debug.LogError("不存在ID为"+_id+"的敌方单位数据，type:"+data.Type);
        }
        else
        {
            trajectoryData.Init(_enemyData.trajectoryID);
        }
    }

    private PlaneEnemyView SpawnEnemy(int id, ITrajectoryData data)
    {
        var plane = PoolMgr.Single.Spawn(Paths.PREFAB_PLANE);
        var enemy = plane.AddOrGet<PlaneEnemyView>();
        enemy.SetPos(transform.position);
        enemy.Init(id,_type, _enemyData, _sprite, data);
        return enemy;
    }

    public float GetSpawnRatio()
    {
        return _spawnQueueNum / (float)_data.QueueNum;
    }

    public bool IsEnd()
    {
        return GetSpawnRatio() == 1;
    }

    public int GetSpawnNum()
    {
        return _spawnQueueNum * _data.QueuePlaneNum;
    }

    public int GetSpawnTotalNum()
    {
        return _data.QueuePlaneNum * _data.QueueNum;
    }

    public bool IsSpawning()
    {
        return _lastEnemy != null;
    }

    public void Spawn()
    {
        ITrajectoryData data = _trajectoryData.GetData(_enemyData.trajectoryType);
        if (_spawnQueueNum < _data.QueueNum)
        {
            _spawnQueueNum++;

            for (int i = 0; i < _data.QueuePlaneNum; i++)
            {
                _lastEnemy = SpawnEnemy(i,data);
            }
        }
    }

    public int Times { get; set; }

    public int UpdateTimes
    {
        get { return 30; }
    }

    public void UpdateFun()
    {
        if (_lastEnemy != null && _lastEnemy.Renderer.Renderer.bounds.max.y < GameUtil.GetCameraMax().y)
        {
            _lastEnemy = null;
        }
    }
}