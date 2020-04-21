public class Shield : ViewBase
{
    private ItemCDEffect _cdEffect;
    private ItemEffect _itemEffect;

    protected override void InitChild()
    {
        var effect = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_ITEM_EFFECT_VIEW, transform);
        _itemEffect = effect.AddComponent<ItemEffect>();
        _cdEffect = Util.Get("Mask").Add<ItemCDEffect>();
        _cdEffect.SetShow();
    }

    public override void Show()
    {
        base.Show();
        MessageMgr.Single.AddListener(MsgEvent.EVENT_SHIELD, ReceiveShield);
        MessageMgr.Single.AddListener(MsgEvent.EVENT_USE_SHIELD, UseShield);
        MessageMgr.Single.AddListener(MsgEvent.EVENT_CHANGE_HAND, ReceiveHandState);
        UpdateShow();
    }

    private void UpdateShow()
    {
        ReceiveShield();
        ReceiveHandState();
    }

    public override void Hide()
    {
        base.Hide();
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_SHIELD, ReceiveShield);
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_USE_SHIELD, UseShield);
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_CHANGE_HAND, ReceiveHandState);
    }

    private void UpdateCount()
    {
        Util.Get("Num").SetText(GameModel.Single.ShieldCount);
    }

    private void ReceiveShield(params object[] args)
    {
        UpdateShield();
    }

    private void UseShield(params object[] args)
    {
        UpdateShield();
        UpdateEffect();
    }

    private void UpdateEffect()
    {
        if (GameModel.Single.ShieldCount > 0)
        {
            _cdEffect.StartCD(() => _itemEffect.SetActive(true));
        }
        
        _itemEffect.SetActive(false);
    }

    private void ReceiveHandState(params object[] args)
    {
        UpdateHandState();
    }

    private void UpdateShield()
    {
        UpdateCount();
        UpdateState();
    }

    private void UpdateState()
    {
        if (GameModel.Single.ShieldCount == 0)
        {
            _cdEffect.SetMask();
            _itemEffect.SetActive(false);
        }
        else if(GameModel.Single.ShieldCount > 0)
        {
            _cdEffect.SetShow();
            _itemEffect.SetActive(true);
        }
    }

    private void UpdateHandState()
    {
        GameUtil.ChangeHandPos(transform.Rect());
    }
}