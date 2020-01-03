using System.Collections.Generic;
using UnityEngine;

public class Life : ViewBase
{
    private List<LifeItem> _items;

    protected override void InitChild()
    {
        _items = new List<LifeItem>();
        InitItem();
        UpdateLife();
    }

    public override void Show()
    {
        base.Show();
        MessageMgr.Single.AddListener(MsgEvent.EVENT_HP, ReceiveMessage);
    }

    public override void Hide()
    {
        base.Hide();
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_HP, ReceiveMessage);
    }


    private void InitItem()
    {
        GameObject item = null;
        for (var i = 0; i < Const.LIFE_ITEM_NUM; i++)
        {
            item = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_LIFE_ITEM_VIEW, transform);
            item.name = i.ToString();
            _items.Add(item.AddComponent<LifeItem>());
        }
    }

    private void UpdateLife()
    {
        Util.Get("Value").SetText(GameModel.Single.Life);
    }

    public void ReceiveMessage(params object[] args)
    {
        UpdateLife();
    }
}