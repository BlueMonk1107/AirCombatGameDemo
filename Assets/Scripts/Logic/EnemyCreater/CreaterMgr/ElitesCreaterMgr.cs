using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElitesCreaterMgr :ISubEnemyCreaterMgr,IGameProcessTriggerEvent {
	
	private List<IEnemyCreater> _elitesCreaters;
	private int _spawnElitesLimit;
	
	public void Init()
	{
		_elitesCreaters = new List<IEnemyCreater>();
	}

	public void InitCreater(Transform parent, AllEnemyData enemyData, EnemyTrajectoryDataMgr trajectoryData, LevelData levelData)
	{
		_elitesCreaters = GameUtil.InitCreater(EnemyType.Elites,parent,enemyData,trajectoryData,levelData);
	}
	
	private void Spawn()
	{
		var creater = GetValidCreater();
		if(creater != null)
			creater.Spawn();
	}

	private IEnemyCreater GetValidCreater()
	{
		return GetCreater(_elitesCreaters);
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

	public bool JudgeEnd()
	{
		foreach (IEnemyCreater creater in _elitesCreaters)
		{
			if (!creater.IsEnd())
				return false;
		}

		return true;
	}

	public List<GameProcessTriggerEvent> GetTriggerEvents()
	{
		var list = new List<GameProcessTriggerEvent>();
		list.Add(GameUtil.GetTriggerEvent(0.3f,Spawn,false,JudgeEnd));
		list.Add(GameUtil.GetTriggerEvent(0.6f,Spawn,false,JudgeEnd));
		return list;
	}
}
