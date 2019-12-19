using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BindPrefab(Paths.PREFAB_STRENGTHEN_VIEW,Const.BIND_PREFAB_PRIORITY_VIEW)]
public class StrengthenView : ViewBase 
{
    protected override void InitChild()
    {
        Util.Get("Switchplayer").Go.AddComponent<SwitchPlayer>();
        Util.Get("Property").Go.AddComponent<PlaneProperty>();
        Util.Get("Money").Go.AddComponent<Money>();
    }

    public override void UpdateFun()
    {
        base.UpdateFun();
        UpdateLevelView();
    }

    private void UpdateLevelView()
    {
        //名称
        string key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES + DataKeys.NAME);
        string data = DataMgr.Single.Get<string>(key);
        Util.Get("Upgrades/Text").SetText(data);
        //花费
        key = KeysUtil.GetPropertyKeys(DataKeys.LEVEL);
        int level = DataMgr.Single.Get<int>(key);
        key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES + level);
        int cost = DataMgr.Single.Get<int>(key);
        Util.Get("Upgrades/Upgrades/Text").SetText(cost);
    }
}
