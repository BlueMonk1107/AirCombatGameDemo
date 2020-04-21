using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectMgr : MonoBehaviour
{
	private IEffectView _effect;
	
	public void Init(BulletType bulletType)
	{
		switch (bulletType)
		{
			case BulletType.Enemy_Boss_1:
				_effect = BulletEffectPoolConfig.Single.Boss1Pool.Spawn();
				break;
		}
		
		if(_effect == null)
			return;
		
		_effect.Init(transform);
		_effect.Begin();
	}

	public void OnDisable()
	{
		if(_effect == null)
			return;
		
		_effect.Stop(null);
		_effect = null;
	}
}
