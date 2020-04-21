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
        var key = KeysUtil.GetPropertyKeys(_key + DataKeys.COST_UNIT);
        var unit = DataMgr.Single.Get<string>(key);
        var money = GameStateModel.Single.GetMoney(unit);

        key = KeysUtil.GetNewKey(PropertyItem.ItemKey.cost, _key);
        var cost = DataMgr.Single.Get<int>(key);

        if (money >= cost)
            ChangeData();
        else
            UIManager.Single.ShowDialog("你没星星了！");
    }

    private void ChangeData()
    {
        var valueKey = KeysUtil.GetNewKey(PropertyItem.ItemKey.value, _key);
        var value = GetValue(valueKey);
        var grouthKey = KeysUtil.GetNewKey(PropertyItem.ItemKey.grouth, _key);
        var grouth = GetValue(grouthKey);
        value += grouth;

        DataMgr.Single.SetObject(valueKey, value);
    }

    private int GetValue(string key)
    {
        return DataMgr.Single.Get<int>(key);
    }
}