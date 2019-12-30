using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour,IUpdate
{

	private float _speed;
	private MoveComponent _move;

	// Use this for initialization
	void Start ()
	{
		_speed = 0;
		var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_Game_CONFIG);
		reader["cameraSpeed"].Get<float>((value) =>
		{
			_speed = value;
			_move = gameObject.AddComponent<MoveComponent>();
			_move.Init(_speed);
		});
		LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
		
	}

	private void OnDestroy()
	{
		LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
	}

	public void UpdateFun()
	{
		_move.Move(Vector2.up);
	}
}
