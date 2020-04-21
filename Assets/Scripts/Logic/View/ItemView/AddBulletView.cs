using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBulletView : ItemViewBase,IBuffCarrier
{
    public BuffType Type
    {
        get { return BuffType.LEVEL_UP; }
    } 
    
    protected override IEffectView GetEffectView()
    {
        return new AddBulletEffectView();
    }

    protected override GameAudio GetGameAudio()
    {
        return GameAudio.Get_Item;
    }

    protected override string SpritePath()
    {
        return Paths.PICTURE_ADD_BULLET;
    }

    public static AddBulletView GetObject()
    {
        var gameObject = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_ITEM_ITEM);
        return gameObject.AddComponent<AddBulletView>();
    }
}
