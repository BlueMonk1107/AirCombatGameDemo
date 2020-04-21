using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItemView : ItemViewBase {
    protected override IEffectView GetEffectView()
    {
        return new AddExpEffectView();
    }

    protected override GameAudio GetGameAudio()
    {
        return GameAudio.Get_Item;
    }

    protected override string SpritePath()
    {
        return Paths.PICTURE_SHIELD;
    }

    protected override void ItemLogic()
    {
        base.ItemLogic();
        GameModel.Single.ShieldCount++;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_SHIELD);
    }
    
    public static ShieldItemView GetObject()
    {
        var gameObject = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_ITEM_ITEM);
        return gameObject.AddComponent<ShieldItemView>();
    }
}
