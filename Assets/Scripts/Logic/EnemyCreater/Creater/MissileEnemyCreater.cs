using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemyCreater : MonoBehaviour,IEnemyCreater,IUpdate {
    
    public void Init(ICreaterData data, AllEnemyData enemyData, EnemyTrajectoryDataMgr trajectoryData)
    {
        throw new System.NotImplementedException();
    }

    public float GetSpawnRatio()
    {
        throw new System.NotImplementedException();
    }

    public int GetSpawnNum()
    {
        throw new System.NotImplementedException();
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public bool IsEnd()
    {
        throw new System.NotImplementedException();
    }

    public bool IsSpawning()
    {
        throw new System.NotImplementedException();
    }

    public int Times { get; set; }
    public int UpdateTimes { get; }
    public void UpdateFun()
    {
        throw new System.NotImplementedException();
    }
}
