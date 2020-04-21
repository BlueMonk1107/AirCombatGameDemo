[BindPrefab(Paths.PREFAB_STRENGTHEN_VIEW, Const.BIND_PREFAB_PRIORITY_VIEW)]
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
        var key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES + DataKeys.NAME);
        var data = DataMgr.Single.Get<string>(key);
        Util.Get("Upgrades/Text").SetText(data);
        //花费
        key = KeysUtil.GetPropertyKeys(DataKeys.LEVEL);
        var level = DataMgr.Single.Get<int>(key);
        key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES + level);
        var cost = DataMgr.Single.Get<int>(key);
        Util.Get("Upgrades/Upgrades/Text").SetText(cost);
    }
}