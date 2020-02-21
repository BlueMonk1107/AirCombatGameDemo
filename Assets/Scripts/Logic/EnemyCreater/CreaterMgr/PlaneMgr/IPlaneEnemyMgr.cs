using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaneEnemyMgr
{
	void Init();
	void AddCraterItem(IEnemyCreater item);
	void Spawn();
	int GetSpawnNum();
	bool JudgeEnd();
}
