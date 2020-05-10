using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 引导行为接口
/// </summary>
public interface IGuideBehaviour : IGuide
{
}

public abstract class GuideBehaviourBase : IGuideBehaviour
{
	private Action _callBack;
	public virtual void OnEnter(Action callBack)
	{
		_callBack = callBack;
		OnEnterLogic();
	}

	public virtual void Update()
	{
		
	}

	protected abstract void OnEnterLogic();

	protected virtual void OnExit()
	{
		OnExitLogic();
        
		if (_callBack != null)
			_callBack();
	}
    
	protected abstract void OnExitLogic();
}
