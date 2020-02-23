using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessTriggerEvent {

	public float Ratio { get; private set; }
	public Action Action { get; private set; }
	public bool NeedPauseProcess { get; private set; }
	public Func<bool> IsEnd { get; private set; }

	public void AddEvent(float ratio,Action action,bool needPauseProcess,Func<bool> isEnd)
	{
		Ratio = ratio;
		Action = action;
		NeedPauseProcess = needPauseProcess;
		IsEnd = isEnd;
	}
}
