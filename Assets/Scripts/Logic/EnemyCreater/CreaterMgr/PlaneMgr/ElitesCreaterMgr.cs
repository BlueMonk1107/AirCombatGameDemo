using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElitesCreaterMgr :IPlaneEnemyMgr {
	
	private List<IEnemyCreater> _elitesCreaters;
	private int _spawnElitesLimit;
	private IPlaneEnemyMgr _normalCreater;
	
	public void Init()
	{
		_elitesCreaters = new List<IEnemyCreater>();
	}

	public void InitData(LevelData levelData,IPlaneEnemyMgr normalCreater)
	{
		_spawnElitesLimit = levelData.NormalDeadNumForSpawnElites;
		_normalCreater = normalCreater;
	}

	public void AddCraterItem(IEnemyCreater item)
	{
		_elitesCreaters.Add(item);
	}

	public void Spawn()
	{
		var creater = GetValidCreater();
		if(creater != null)
			creater.Spawn();
	}

	private IEnemyCreater GetValidCreater()
	{
		if (_normalCreater.GetSpawnNum() >= _spawnElitesLimit)
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

	public bool JudgeEnd()
	{
		foreach (IEnemyCreater creater in _elitesCreaters)
		{
			if (!creater.IsEnd())
				return false;
		}

		return true;
	}

	public int GetSpawnNum()
	{
		int count = 0;
		foreach (IEnemyCreater creater in _elitesCreaters)
		{
			count += creater.GetSpawnNum();
		}

		return count;
	}
}
