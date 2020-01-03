[BindPrefab(Paths.PREFAB_STRENGTHEN_VIEW, Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class StrengthenController : ControllerBase
{
    protected override void InitChild()
    {
        transform.Find("Switchplayer").gameObject.AddComponent<SwitchPlayerController>();
        transform.Find("Property").gameObject.AddComponent<PlanePropertyController>();
        transform.ButtonAction("Upgrades/Upgrades", Upgrades);
        transform.ButtonAction("Back", UIManager.Single.Back);
    }

    private void Upgrades()
    {
        //判断是否能够升级
        //花费是否足够 当前等级是否超限
        var key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES + DataKeys.COST_UNIT);
        var value = DataMgr.Single.Get<string>(key);

        key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES + GameStateModel.Single.PlaneLevel);
        var cost = DataMgr.Single.Get<int>(key);

        var money = GameStateModel.Single.GetMoney(value);

        var levelMax = DataMgr.Single.Get<int>(DataKeys.LEVEL_MAX);

        if (money >= cost && GameStateModel.Single.PlaneLevel < levelMax)
        {
            ChangeMenoy(value, cost);
            ChangeLevel();
            ChangeData();
        }
        else
        {
            UIManager.Single.ShowDialog("你没钻石了！");
        }
    }

    private void ChangeMenoy(string costUnit, int cost)
    {
        var money = GameStateModel.Single.GetMoney(costUnit);
        GameStateModel.Single.SetMoney(costUnit, money - cost);
    }

    private void ChangeLevel()
    {
        var key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES_RATIO);
        var level = GameStateModel.Single.PlaneLevel;
        level++;
        DataMgr.Single.Set(key, level);
    }

    private void ChangeData()
    {
        //获取升级系数，修改数据
        var key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES_RATIO);
        var ratio = DataMgr.Single.Get<int>(key);

        ChangeData(ratio, PropertyItem.ItemKey.value, PlaneProperty.Property.attack);
        ChangeData(ratio, PropertyItem.ItemKey.maxVaue, PlaneProperty.Property.attack);
        ChangeData(ratio, PropertyItem.ItemKey.grouth, PlaneProperty.Property.attack);
        ChangeData(ratio, PropertyItem.ItemKey.value, PlaneProperty.Property.life);
        ChangeData(ratio, PropertyItem.ItemKey.maxVaue, PlaneProperty.Property.life);
        ChangeData(ratio, PropertyItem.ItemKey.grouth, PlaneProperty.Property.life);
    }


    private void ChangeData(int ratio, PropertyItem.ItemKey itemKey, PlaneProperty.Property property)
    {
        var key = KeysUtil.GetNewKey(itemKey, property.ToString());
        var value = DataMgr.Single.Get<int>(key);
        value *= ratio;
        DataMgr.Single.Set(key, value);
    }
}