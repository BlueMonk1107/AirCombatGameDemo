public class LifeItem : ViewBase
{
    private int _lifeMin;

    protected override void InitChild()
    {
        var id = int.Parse(transform.name);
        SetItemPos(id);
        _lifeMin = GetLifeMin(id);
    }

    private int GetLifeMin(int id)
    {
        var eachLife = GameModel.Single.LifeMax / Const.LIFE_ITEM_NUM;
        return id * eachLife;
    }

    private void SetItemPos(int id)
    {
        var rect = transform.Rect();
        var width = rect.rect.height;
        var pos = rect.anchoredPosition;
        pos.x = width / 2 + width * id;
        rect.anchoredPosition = pos;
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

    public void ReceiveMessage(params object[] args)
    {
        if (GameModel.Single.Life < _lifeMin) 
            Hide();
    }
}