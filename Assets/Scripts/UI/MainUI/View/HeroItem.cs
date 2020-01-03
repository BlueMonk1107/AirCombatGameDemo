using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HeroItem : ViewBase
{
    private readonly Color _default = Color.gray;
    private Hero _hero;
    private Image _image;
    private readonly Color _selected = Color.white;
    private readonly float _time = 0.5f;

    protected override void InitChild()
    {
        _image = transform.GetComponent<Image>();
        var heroName = _image.sprite.name;
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
        var isSelected = _hero == GameStateModel.Single.SelectedHero;

        if (isSelected)
            Selected();
        else
            Unselected();
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