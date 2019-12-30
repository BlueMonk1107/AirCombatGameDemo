using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IUpdate,IBullet
{
	private float _speed;
	private MoveComponent _move;
	private IBulletModel _model;
	private ITrajectory _trajectory;
	private Vector3 _startPos;

	public BulletOwner Owner
	{
		get { return _model.Owner; }
	}

	public int GetAttack()
	{
		return _model.GetAttack();
	}

	public BulletOwner[] Tagets
	{
		get { return _model.Tagets; }
	}

	private SpriteRenderer _renderer;
	

	// Use this for initialization
	void Start () {
		_model.GetBulletSpeed((value) =>
		{
			_speed = value;
			_move = gameObject.AddComponent<MoveComponent>();
			_move.Init(_speed);
		});

		gameObject.AddComponent<CollideMsgComponent>();
		gameObject.AddComponent<BulletBehaviour>();
	}

	public void Init(IBulletModel model,ITrajectory trajectory,Vector3 pos)
	{
		if (_renderer == null)
		{
			_renderer = GetComponent<SpriteRenderer>();
		}
		_model = model;
		_renderer.sprite = model.Sprite();
		_startPos = pos;
		_trajectory = trajectory;
		SetPos(pos);
	}
	
	private void OnEnable()
	{
		LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
	}

	private void OnDisable()
	{
		LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
	}

	public void UpdateFun()
	{
		if (_move != null)
		{
			_move.Move(_trajectory.GetDirection());
			SetAngle();
		}
	}

	private void SetAngle()
	{
		var eular = transform.localEulerAngles;
		eular.z = _trajectory.GetZRotate();
		transform.localEulerAngles = eular;
	}

	public void SetPos(Vector3 pos)
	{
		transform.position = pos;
	}
	
	private void OnBecameInvisible()
	{
		PoolMgr.Single.Despawn(gameObject);
	}

	public HashSet<string> GetTargetTags()
	{
		return _model.GetTargetTags();
	}
}
