using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyItemController : ControllerBase
{

    private string _key;
    
    public void Init(string key)
    {
        _key = key;
    }
    protected override void InitChild()
    {
        InitButtonAction();
    }
    
    private void InitButtonAction()
    {
        transform.ButtonAction("Add", AddAction);
    }

    private void AddAction()
    {
        string key = KeysUtil.GetPropertyKeys(_key + DataKeys.COST_UNIT);
        string unit = DataMgr.Single.Get<string>(key);
        int money = GameStateModel.Single.GetMoney(unit);

        key = KeysUtil.GetNewKey(PropertyItem.ItemKey.cost, _key);
        int cost = DataMgr.Single.Get<int>(key);

        if (money >= cost)
        {
            ChangeData();
        }
        else
        {
            UIManager.Single.ShowDialog("你没星星了！");
        }
    }

    private void ChangeData()
    {
        string valueKey = KeysUtil.GetNewKey(PropertyItem.ItemKey.value,_key);
        int value = GetValue(valueKey);
        string grouthKey = KeysUtil.GetNewKey(PropertyItem.ItemKey.grouth,_key);
        int grouth = GetValue(grouthKey);
        value += grouth;
		
        DataMgr.Single.SetObject(valueKey,value);
    }
    
    private int GetValue(string key)
    {
        return DataMgr.Single.Get<int>(key);
    }
}
