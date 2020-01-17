using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IBehaviour
{
	private LifeComponent _life;
	private void Start()
	{
		_life = GetComponent<LifeComponent>();
		if (_life == null)
		{
			Debug.LogError("当前飞机没有添加生命值组件，物体名称:"+gameObject.name);
		}
	}

	public void Injure(int value)
	{
		if(_life.Life <= 0)
			return;
		
		var life = _life.Life - value;
		if (life <= 0)
		{
			_life.Life = 0;
			Dead();
		}
		else
		{
			_life.Life = life;
		}
	}

	public void Dead()
	{
		//AniMgr.Single.PlaneDestroyAni(transform.position);
		GameModel.Single.Score++;
		MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_SCORE);
		PoolMgr.Single.Despawn(gameObject);
		AudioMgr.Single.PlayOnce(GameAudio.Explode_Plane.ToString());
		var starGo = PoolMgr.Single.Spawn(Paths.PREFAB_STAR);
		var star = starGo.AddOrGet<StarView>();
		star.SetPos(transform.position);
		var typeC = GetComponent<EnemyTypeComponent>();
	}
}
