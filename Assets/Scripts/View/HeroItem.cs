using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HeroItem : ViewBase
{
	private Color _default = Color.gray;
	private Color _selected = Color.white;
	private float _time = 0.5f;
	private Image _image;
	private Hero _hero;
	
	protected override void InitChild()
	{
		_image = transform.GetComponent<Image>();
		string heroName = _image.sprite.name;
		try
		{
			_hero = (Hero) Enum.Parse(typeof(Hero), heroName);
		}
		catch (Exception e)
		{
			Debug.Log(e);
		}
		
		Unselected();
	}

	public override void UpdateFun()
	{
		bool isSelected = _hero == GameStateModel.Single.SelectedHero;

		if (isSelected)
		{
			Selected();
		}
		else
		{
			Unselected();
		}
	}

	private void Selected()
	{
		_image.DOKill();
		_image.DOColor(_selected, _time);
	}

	public void Unselected()
	{
		_image.DOKill();
		_image.DOColor(_default, _time);
	}
}
