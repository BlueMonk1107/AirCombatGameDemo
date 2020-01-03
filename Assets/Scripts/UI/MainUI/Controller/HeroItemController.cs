using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroItemController : ControllerBase
{
    private Hero _hero;

    protected override void InitChild()
    {
        var heroName = GetComponent<Image>().sprite.name;

        try
        {
            _hero = (Hero) Enum.Parse(typeof(Hero), heroName);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        GetComponent<Button>().onClick.AddListener(Selected);
    }

    private void Selected()
    {
        GameStateModel.Single.SelectedHero = _hero;
        AudioMgr.Single.Play(_hero.ToString());
    }

    public override void UpdateFun()
    {
        base.UpdateFun();
        if (_hero != GameStateModel.Single.SelectedHero) AudioMgr.Single.Stop(_hero.ToString());
    }

    public override void Hide()
    {
        base.Hide();
        AudioMgr.Single.Stop(_hero.ToString());
    }
}