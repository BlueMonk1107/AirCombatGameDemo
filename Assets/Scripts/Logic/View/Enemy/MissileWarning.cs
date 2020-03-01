using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWarning : PlaneView
{
	private Action _endAction;
	private MissileWarningEffectView _effect;

	public void Init(float x,int numOfWarning,float eachWarningTime)
	{
		base.Init();
		InitPos(x);
		
		if(_effect == null)
			_effect = new MissileWarningEffectView();
		
		_effect.Init(transform,numOfWarning,eachWarningTime,_endAction);
		_effect.Begin();
	}

	private void InitPos(float x)
	{
		float y = GameUtil.GetCameraMin().y + GameUtil.GetCameraSize().y / 2;
		
		var pos = transform.position;
		pos.x = x;
		pos.y = y;
		SetPos(pos);
	}

	public void AddEndListener(Action endAction)
	{
		_endAction = endAction;
	}
}
