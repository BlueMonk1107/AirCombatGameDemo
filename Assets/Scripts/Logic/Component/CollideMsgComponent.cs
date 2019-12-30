using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollideMsgComponent : MonoBehaviour,IColliderMsg
{
	private IBullet _selfBullet;
	private IBehaviour _behaviour;

	// Use this for initialization
	void Start () {
		_selfBullet = GetComponentInChildren<IBullet>();
		_behaviour = GetComponent<IBehaviour>();
	}

	public void ColliderMsg(Transform other)
	{
		IBullet bullet = other.GetComponentInChildren<IBullet>();
		
		if (other.tag == Tags.BULLET 
		    && bullet != null 
		    && _selfBullet != null 
		    && bullet.Tagets.Contains(_selfBullet.Owner)
		    )
		{
			if (_behaviour != null)
			{
				_behaviour.Injure(bullet.GetAttack());
			}
		}
		else if (_selfBullet != null && _selfBullet.GetTargetTags().Contains(other.tag))
		{
			if (_behaviour != null)
			{
				_behaviour.Dead();
			}
		}
	}
}
