using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyCreater
{
    void Init(CreaterData data,AllEnemyData enemyData,EnemyTrajectoryData trajectoryData);
    float GetSpawnRatio();
    void Spawn();
}
