using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BindPrefab(Paths.PREFAB_STRENGTHEN_VIEW,Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class StrengthenController : ControllerBase {
    protected override void InitChild()
    {
        transform.Find("Switchplayer").gameObject.AddComponent<SwitchPlayerController>();
        transform.Find("Property").gameObject.AddComponent<PlanePropertyController>();
        transform.ButtonAction("Upgrades/Upgrades",Upgrades);
        transform.ButtonAction("Back",UIManager.Single.Back);
    }

    private void Upgrades()
    {
        //判断是否能够升级
        //花费是否足够 当前等级是否超限
        string key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES + DataKeys.COST_UNIT);
        string value = DataMgr.Single.Get<string>(key);
        
        key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES + GameStateModel.Single.PlaneLevel);
        int cost = DataMgr.Single.Get<int>(key);

        int money = GameStateModel.Single.GetMoney(value);

        int levelMax = DataMgr.Single.Get<int>(DataKeys.LEVEL_MAX);

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

    private void ChangeMenoy(string costUnit,int cost)
    {
        int money = GameStateModel.Single.GetMoney(costUnit);
        GameStateModel.Single.SetMoney(costUnit,money - cost);
    }

    private void ChangeLevel()
    {
        string key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES_RATIO);
        int level = GameStateModel.Single.PlaneLevel;
        level++;
        DataMgr.Single.Set<int>(key, level);
    }

    private void ChangeData()
    {
        //获取升级系数，修改数据
        string key = KeysUtil.GetPropertyKeys(DataKeys.UPGRADES_RATIO);
        int ratio = DataMgr.Single.Get<int>(key);

        ChangeData(ratio, PropertyItem.ItemKey.value, PlaneProperty.Property.attack);
        ChangeData(ratio, PropertyItem.ItemKey.maxVaue, PlaneProperty.Property.attack);
        ChangeData(ratio, PropertyItem.ItemKey.grouth, PlaneProperty.Property.attack);
        ChangeData(ratio, PropertyItem.ItemKey.value, PlaneProperty.Property.life);
        ChangeData(ratio, PropertyItem.ItemKey.maxVaue, PlaneProperty.Property.life);
        ChangeData(ratio, PropertyItem.ItemKey.grouth, PlaneProperty.Property.life);
    }
    

    private void ChangeData(int ratio,PropertyItem.ItemKey itemKey,PlaneProperty.Property property)
    {
        string key = KeysUtil.GetNewKey(itemKey, property.ToString());
        int value = DataMgr.Single.Get<int>(key);
        value *= ratio;
        DataMgr.Single.Set(key,value);
    }
}
