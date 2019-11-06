using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HeroItem : MonoBehaviour
{
	private Color _default = Color.gray;
	private Color _selected = Color.white;
	private float _time = 0.5f;
	private Image _image;
	private Action _callBack;
	
	// Use this for initialization
	void Start ()
	{
		_image = transform.GetComponent<Image>();
		GetComponent<Button>().onClick.AddListener(Selected);
		Unselected();
	}

	private void Selected()
	{
		if (_callBack != null)
			_callBack();
		
		_image.DOKill();
		_image.DOColor(_selected, _time);
	}

	public void Unselected()
	{
		_image.DOKill();
		_image.DOColor(_default, _time);
	}

	public void AddResetListener(Action action)
	{
		_callBack = action;
	}
}
