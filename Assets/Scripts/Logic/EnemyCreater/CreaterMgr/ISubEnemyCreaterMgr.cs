using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubEnemyCreaterMgr
{
	void Init();
	void InitCreater(Transform parent,AllEnemyData enemyData, EnemyTrajectoryDataMgr trajectoryData, LevelData levelData);
}
