using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddExpView : ItemViewBase {
    protected override IEffectView GetEffectView()
    {
        return new AddExpEffectView();
    }

    protected override GameAudio GetGameAudio()
    {
        return GameAudio.Get_Item;
    }

    protected override void ItemLogic()
    {
        base.ItemLogic();
        GameModel.Single.TempLevel++;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_GAME_EXP_LEVEL_UP);
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_GAME_UPDATE_LEVEL);
    }

    protected override string SpritePath()
    {
        return Paths.PICTURE_ADD_EXP;
    }
    
    public static AddExpView GetObject()
    {
        var gameObject = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_ITEM_ITEM);
        return gameObject.AddComponent<AddExpView>();
    }
}
