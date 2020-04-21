public class Power : ViewBase
{
    private ItemCDEffect _cdEffect;
    private ItemEffect _itemEffect;

    protected override void InitChild()
    {
        UpdateCount();
        var effect = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_ITEM_EFFECT_VIEW, transform);
        _itemEffect = effect.AddComponent<ItemEffect>();
        _cdEffect = Util.Get("Mask").Add<ItemCDEffect>();
        _cdEffect.SetShow();
    }

    public override void Show()
    {
        base.Show();
        MessageMgr.Single.AddListener(MsgEvent.EVENT_POWER, ReceivePower);
        MessageMgr.Single.AddListener(MsgEvent.EVENT_USE_BOMB, UsePower);
        MessageMgr.Single.AddListener(MsgEvent.EVENT_CHANGE_HAND, ReceiveHandState);
        UpdateShow();
    }

    private void UpdateShow()
    {
        ReceivePower();
        ReceiveHandState();
    }

    public override void Hide()
    {
        base.Hide();
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_POWER, ReceivePower);
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_USE_BOMB, UsePower);
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_CHANGE_HAND, ReceiveHandState);
    }

    private void UpdateCount()
    {
        Util.Get("Num").SetText(GameModel.Single.PowerCount);
    }

    private void ReceivePower(params object[] args)
    {
        UpdatePower();
    }
    
    private void UsePower(params object[] args)
    {
        UpdatePower();
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

    private void UpdatePower()
    {
        UpdateCount();
        UpdateState();
    }

    private void UpdateState()
    {
        if (GameModel.Single.PowerCount == 0)
        {
            _cdEffect.SetMask();
            _itemEffect.SetActive(false);
        }
        else if(GameModel.Single.PowerCount > 0)
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