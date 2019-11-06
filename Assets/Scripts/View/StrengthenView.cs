using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BindPrefab(Paths.STRENGTHEN_VIEW)]
public class StrengthenView : ViewBase 
{
    protected override void InitChild()
    {
        Util.Get("Switchplayer").Go.AddComponent<SwitchPlayer>();
        Util.Get("Property").Go.AddComponent<PlaneProperty>();
    }
}
