using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAni : MonoBehaviour,IUpdate
{

	private Sprite[] _sprites;
	private int _index;
	private SpriteRenderer _renderer;
	private Action _callBack;

	public void Init(Sprite[] sprites,Action callBack)
	{
		_index = 0;
		_sprites = sprites;

		if (_renderer == null)
			_renderer = GetComponent<SpriteRenderer>();
		_callBack = callBack;
	}

	private void OnEnable()
	{
		LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
	}

	private void OnDisable()
	{
		LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
	}

	public int Times { get; set; }
	public int UpdateTimes { get; }
	public void UpdateFun()
	{
		if (_index < _sprites.Length)
		{
			_renderer.sprite = _sprites[_index];
			_index++;
		}
		else
		{
			if (_callBack != null)
				_callBack();
		}
	}
}
