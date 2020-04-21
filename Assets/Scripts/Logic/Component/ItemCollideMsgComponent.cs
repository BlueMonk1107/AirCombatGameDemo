using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollideMsgComponent : MonoBehaviour,IColliderMsg
{
	private Action _onCollideEvent;

	public void Init(Action collideEvent)
	{
		_onCollideEvent = collideEvent;
	}

	public void ColliderMsg(Transform other)
	{
		if (other.tag == Tags.PLAYER && _onCollideEvent != null)
		{
			_onCollideEvent();
		}
	}
}
