using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyCreater
{
    void Init(ICreaterData data,AllEnemyData enemyData,EnemyTrajectoryDataMgr trajectoryData);
    float GetSpawnRatio();
    int GetSpawnNum();
    void Spawn();
    bool IsEnd();
    bool IsSpawning();
}
