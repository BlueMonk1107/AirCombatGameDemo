[BindPrefab(Paths.PREFAB_PAUSE_VIEW, Const.BIND_PREFAB_PRIORITY_VIEW)]
public class GamePauseView : ViewBase
{
    protected override void InitChild()
    {
    }

    public override void UpdateFun()
    {
        base.UpdateFun();
        ChangeHand();
    }

    private void ChangeHand()
    {
        var hand = GameStateModel.Single.HandMode;
        var text = hand == HandMode.LEFT ? "右手" : "左手";
        Util.Get("Hand/Text").SetText(text);
    }
}