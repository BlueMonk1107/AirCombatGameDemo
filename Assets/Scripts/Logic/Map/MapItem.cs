using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour,IUpdate
{
	private float _offset;
	private Transform _camera;
	private SpriteRenderer _renderer;

	public void Init(float offset,Transform camera)
	{
		_offset = offset;
		_camera = camera;
		_renderer = GetComponent<SpriteRenderer>();
		LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
		SetSprite();
	}

	private void SetSprite()
	{
		int level = GameModel.Single.SelectedLevel;
		string name = Const.MAP_PREFIX + level;
		var sprite = LoadMgr.Single.Load<Sprite>(Paths.PICTURE_MAP_FOLDER + name);
		if (sprite == null)
		{
			sprite = LoadMgr.Single.Load<Sprite>(Paths.PICTURE_MAP_FOLDER + Const.MAP_PREFIX + 0);
		}
		
		_renderer.sprite = sprite;
	}

	private bool JugdeMove(float offset,Transform camera)
	{
		return camera.position.y - transform.position.y >= offset;
	}

	private void Move(float offset)
	{
		var pos = transform.position;
		pos.y += offset * 2;
		transform.position = pos;
	}

	public void UpdateFun()
	{
		if (JugdeMove(_offset, _camera))
		{
			Move(_offset);
		}
	}

	private void OnDestroy()
	{
		LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
	}
}
