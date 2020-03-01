using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileView : PlaneView,IUpdate
{
	private int _numOfWarning;
	private float _eachWarningTime;
	private float _speed;
	private MoveComponent _move;
	private bool _startMove;
	private Action _endAction;

	public void Init(float x,int numOfWarning,float eachWarningTime,float speed)
	{
		_startMove = false;
		_numOfWarning = numOfWarning;
		_eachWarningTime = eachWarningTime;
		_speed = speed;
		base.Init();
		InitPos(x);
		InitLight();
	}
	
	protected override void OnEnable()
	{
		base.OnEnable();
		LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
	}
	
	
	private void OnDisable()
	{
		if (_endAction != null)
			_endAction();
		
		LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
	}

	private void InitPos(float x)
	{
		SpriteRenderer render = GetComponent<SpriteRenderer>();
		float height = render.bounds.size.y;
		float y = GameUtil.GetCameraMax().y + height / 2;
		var pos = transform.position;
		pos.x = x;
		pos.y = y;
		transform.position = pos;
	}

	protected override void InitComponent()
	{
		gameObject.AddOrGet<CameraMove>();
		_move = gameObject.AddOrGet<MoveComponent>();
		_move.Init(_speed);
		gameObject.AddOrGet<AutoDespawnComponent>();
		gameObject.AddOrGet<ColliderComponent>();
		gameObject.AddOrGet<ItemCollideMsgComponent>().Init(CollidePlayer);
	}

	private void CollidePlayer()
	{
		PoolMgr.Single.Despawn(gameObject);
		AudioMgr.Single.PlayOnce(GameAudio.Explode_Plane.ToString());
		AniMgr.Single.PlaneDestroyAni(transform.position);
	}

	private void InitLight()
	{
		var go = PoolMgr.Single.Spawn(Paths.PREFAB_ITEM_LIGHT);
		var warning = go.AddOrGet<MissileWarning>();
		go.transform.SetParent(transform);
		warning.AddEndListener(StartMove);
		warning.Init(transform.position.x,_numOfWarning,_eachWarningTime);
		
	}

	private void StartMove()
	{
		_startMove = true;
		Destroy(gameObject.GetComponent<CameraMove>());
	}

	public void AddEndListener(Action endAction)
	{
		_endAction = endAction;
	}

	public int Times { get; set; }
	public int UpdateTimes { get; }
	public void UpdateFun()
	{
		if (_startMove)
		{
			_move.Move(Vector2.down);
		}
	}
}
