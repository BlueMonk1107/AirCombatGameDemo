[BindPrefab(Paths.PREFAB_GAME_UI_VIEW, Const.BIND_PREFAB_PRIORITY_VIEW)]
public class GameUIView : ViewBase
{
    protected override void InitChild()
    {
        Util.Get("Life").Add<Life>();
        Util.Get("Shield").Add<Shield>();
        Util.Get("Bomb").Add<Power>();
        ReceiveMessage();
    }

    public override void Show()
    {
        base.Show();
        MessageMgr.Single.AddListener(MsgEvent.EVENT_SCORE, ReceiveMessage);
    }

    public override void Hide()
    {
        base.Hide();
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_SCORE, ReceiveMessage);
    }

    public void ReceiveMessage(params object[] args)
    {
        UpdateScore();
        UpdateStars();
    }

    private void UpdateScore()
    {
        Util.Get("Score").SetText(GameModel.Single.Score);
    }

    private void UpdateStars()
    {
        Util.Get("Star/Value").SetText(GameModel.Single.Stars);
    }
}