using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarView : ItemViewBase
{
    protected override IEffectView GetEffectView()
    {
        return new StarEffectView();
    }

    protected override GameAudio GetGameAudio()
    {
        return GameAudio.Get_Gold;
    }

    protected override void ItemLogic()
    {
        GameModel.Single.Stars++;
        PoolMgr.Single.Despawn(gameObject);
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_SCORE);
    }

    protected override string SpritePath()
    {
        return Paths.PICTURE_STAR;
    }

    public static StarView GetObject()
    {
        var starGo = PoolMgr.Single.Spawn(Paths.PREFAB_ITEM_ITEM);
        return starGo.AddOrGet<StarView>();
    }
}