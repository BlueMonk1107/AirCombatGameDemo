using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour,IUpdate
{

	private float _speed;

	// Use this for initialization
	void Start ()
	{
		_speed = 0;
		var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_Game_CONFIG);
		reader["cameraSpeed"].Get<float>((value) => { _speed = value; });
		LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
	}

	private void OnDestroy()
	{
		LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
	}

	public void UpdateFun()
	{
		transform.Translate(0,_speed * Time.deltaTime,0);
	}
}
