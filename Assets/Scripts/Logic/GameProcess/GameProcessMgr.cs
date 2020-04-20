using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessMgr : MonoBehaviour, IUpdate
{
	private List<GameProcessNormalEvent> _normalEvents;
	private List<GameProcessTriggerEvent> _triggerEvents;
	private List<GameProcessTriggerEvent> _curTriggerEvents;
	private float _totalNum;
	private bool _start;
	private int _enemyActiveNumMax;
	private GameProcessTriggerEvent _temp;
	private GameObject _createrGo;
	
	public void Init()
	{
		_start = false;
		_curTriggerEvents = new List<GameProcessTriggerEvent>();
		InitCreater();
		LifeCycleMgr.Single.Add(LifeName.UPDATE, this);
		MessageMgr.Single.AddListener(MsgEvent.EVENT_START_GAME,StartGame);
		MessageMgr.Single.AddListener(MsgEvent.EVENT_END_ONCE,EndOnce);
	}

	private void OnDestroy()
	{
		LifeCycleMgr.Single.Remove(LifeName.UPDATE, this);
		MessageMgr.Single.RemoveListener(MsgEvent.EVENT_START_GAME,StartGame);
		MessageMgr.Single.RemoveListener(MsgEvent.EVENT_END_ONCE,EndOnce);
	}

	private void EndOnce(object[] paras)
	{
		ClearData();
		CoroutineMgr.Single.Delay(Const.WAIT_LEVEL_START_TIME,StartNewLevel);
	}

	private void ClearData()
	{
		if(_createrGo != null)
			Destroy(_createrGo);
		
		_start = false;
		_curTriggerEvents.Clear();
	}

	private void StartNewLevel()
	{
		MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_START_ONCE);
		InitCreater();
	}

	private void InitCreater()
	{
		_createrGo = new GameObject("CreaterMgr");
		_createrGo.transform.SetParent(transform);
		_createrGo.AddComponent<EnemyCreaterMgr>().Init(InitData);
	}

	private void InitData(EnemyCreaterMgr createrMgr)
	{
		_triggerEvents = createrMgr.GetTriggerEvents();
		_triggerEvents.Sort((x, y) => x.Ratio.CompareTo(y.Ratio));

		_normalEvents = createrMgr.GetNormalEvents();

		_totalNum = GetTotalNum();

		_enemyActiveNumMax = createrMgr.GetActiveNumMax();
	}

	private int GetTotalNum()
	{
		int toatal = 0;
		foreach (var normalEvent in _normalEvents)
		{
			toatal += normalEvent.SpawnTotalNum;
		}

		return toatal;
	}

	private int GetSpawnedNum()
	{
		int toatal = 0;
		foreach (var normalEvent in _normalEvents)
		{
			if (normalEvent.SpawnedNum != null)
				toatal += normalEvent.SpawnedNum();
		}

		return toatal;
	}

	private float GetProcessRatio()
	{
		return GetSpawnedNum() / _totalNum;
	}


	private void StartGame(object[] paras)
	{
		_start = true;
	}

	public int Times { get; set; }
	public int UpdateTimes
	{
		get { return 30; }
	}
	public void UpdateFun()
	{
		if(!_start)
			return;
		
		if(!JudgeTriggerEnd())
			return;

		UpdateNormals();

		UpdateTriggers();
	}

	private bool JudgeTriggerEnd()
	{
		bool isEnd = true;
		foreach (var triggerEvent in _curTriggerEvents)
		{
			if (!triggerEvent.IsEnd())
			{
				if (triggerEvent.Action != null)
					triggerEvent.Action();
				isEnd = false;
			}
		}

		return isEnd;
	}

	private void UpdateNormals()
	{
		if (PoolMgr.Single.GetActiveNum(Paths.PREFAB_PLANE) < _enemyActiveNumMax)
		{
			foreach (var normalEvent in _normalEvents)
			{
				if (normalEvent.SpawnAction != null)
					normalEvent.SpawnAction();
			}
		}
	}

	private void UpdateTriggers()
	{
		int index = 0;
		for (int i = 0; i < _triggerEvents.Count; i++)
		{
			_temp = _triggerEvents[index];
			if (GetProcessRatio() >= _temp.Ratio)
			{
				_curTriggerEvents.Clear();
				
				if(_temp.IsEnd != null && _temp.NeedPauseProcess)
					_curTriggerEvents.Add(_temp);

				if (_temp.Action != null)
					_temp.Action();

				index++;
			}
			else
			{
				break;
			}
		}
		
		_triggerEvents.RemoveRange(0,index);
	}
}
