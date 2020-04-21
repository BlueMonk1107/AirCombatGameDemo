using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayDetalCoroutineItem
{
	private static int _idCounter = -1;
	private float _time;
	private float _timeDetal;
	private DateTime _startTime;
	public int ID { get; private set; }
	public bool IsRunning { get; private set; }
	
	public DelayDetalCoroutineItem()
	{
		_idCounter++;
		ID = _idCounter;
	}

	public void Start(float time,Action begin,Action complete)
	{
		if (!IsRunning)
		{
			_startTime = DateTime.Now;
			_time = time;
			IsRunning = true;
			CoroutineMgr.Single.ExecuteOnce(Wait(complete));
			_time = 0;
			_timeDetal = 0;
			if (begin != null)
				begin();
		}
		else
		{
			float spendTime = (float) (DateTime.Now -_startTime).TotalSeconds;
			_timeDetal += spendTime;
		}
	}
	
	private IEnumerator Wait(Action complete)
	{
		while (IsRunning && _time != 0)
		{
			yield return new WaitForSeconds(_time);
			_time = _timeDetal;
			_timeDetal = 0;
		}

		if (complete != null)
			complete();
	}
}
