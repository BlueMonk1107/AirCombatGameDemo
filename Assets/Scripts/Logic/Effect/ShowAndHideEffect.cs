using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShowAndHideEffect : IEffect
{
	private SpriteRenderer _renderer;
	private float _duration;
	private int _executeTimes;
	private Action _endCallback;
	private int _times;

	public void Init(Transform transform,float duration,int executeTimes,Action endCallback)
	{
		_times = 0;
		_renderer = transform.GetComponent<SpriteRenderer>();
		_duration = duration;
		_executeTimes = executeTimes;
		_endCallback = endCallback;
	}

	public void Begin()
	{
		if (_renderer == null)
		{
			Debug.LogError("当前组件未初始化");
			return;
		}

		SetAlpha(0);
		_renderer.DOFade(1, _duration / 2).OnComplete(Hide);
	}

	private void SetAlpha(float a)
	{
		var color = _renderer.color;
		color.a = a;
		_renderer.color = color;
	}

	public void Stop(Action callBack)
	{
		SetAlpha(0);

		if (callBack != null)
			callBack();
	}

	public void Hide()
	{
		if (_renderer == null)
		{
			Debug.LogError("当前组件未初始化");
			return;
		}
		
		_renderer.DOFade(0, _duration / 2).OnComplete(() =>
		{
			_times++;
			if (_times == _executeTimes)
			{
				if (_endCallback != null)
					_endCallback();
			}
			else if (_times < _executeTimes)
			{
				Begin();
			}
		});
	}

	public void Clear()
	{
		_endCallback = null;
		_duration = 0;
		_renderer = null;
	}
}
