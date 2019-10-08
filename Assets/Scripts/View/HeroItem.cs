using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HeroItem : MonoBehaviour
{
	private Color _default = Color.white;
	private Color _grey = Color.gray;
	private float _time = 0.5f;
	private Image _image;
	private Action _callBack;

	// Use this for initialization
	void Start ()
	{
		_image = GetComponent<Image>();
		GetComponent<Button>().onClick.AddListener(Selected);
		UnSelected();
	}

	private void Selected()
	{
		if(_callBack != null)
			_callBack();
		
		_image.DOKill();
		_image.DOColor(_default, _time);
	}

	public void UnSelected()
	{
		_image.DOKill();
		_image.DOColor(_grey, _time);
	}

	public void AddResetListener(Action callBack)
	{
		_callBack = callBack;
	}
}
