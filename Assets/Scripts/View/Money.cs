using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : ViewBase {
    protected override void InitChild()
    {
        
    }

    public override void UpdateFun()
    {
        Util.Get("Star/BG/Text").SetText(DataMgr.Single.Get<int>(DataKeys.STAR));
        Util.Get("Diamond/BG/Text").SetText(DataMgr.Single.Get<int>(DataKeys.DIAMOND));
    }
}
