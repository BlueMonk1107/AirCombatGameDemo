using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : ViewBase
{
    protected override void InitChild()
    {
    }
    

    public override void Show()
    {
        base.Show();
        UpdateSprite();
    }

    public override void UpdateFun()
    {
        UpdateSprite();
    }
    

    private void UpdateSprite()
    {
        int id = GameStateModel.Single.SelectedPlaneId;
        int level = GameStateModel.Single.PlaneLevel;
        Util.Get("Icon").SetSprite(PlaneSpritesModel.Single[id,level]);
    }
}
