using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemView : ItemViewBase {
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
        return Paths.PICTURE_POWER;
    }
    
    protected override void ItemLogic()
    {
        base.ItemLogic();
        GameModel.Single.PowerCount++;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_POWER);
    }
    
    public static PowerItemView GetObject()
    {
        var gameObject = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_ITEM_ITEM);
        return gameObject.AddComponent<PowerItemView>();
    }
}
