[BindPrefab(Paths.PREFAB_PAUSE_VIEW, Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class GamePauseController : ControllerBase
{
    protected override void InitChild()
    {
        transform.ButtonAction("Exit", Exit);
        transform.ButtonAction("Continue", Continue);
        transform.ButtonAction("Hand", ChangeHand);
    }

    public override void Show()
    {
        base.Show();
        GameStateModel.Single.Pause = true;
    }

    private void Exit()
    {
        GameStateModel.Single.Pause = false;
        GameStateModel.Single.TargetScene = SceneName.Main;
        UIManager.Single.Show(Paths.PREFAB_LOADING_VIEW);
        UIManager.Single.Back();
    }

    private void Continue()
    {
        GameStateModel.Single.Pause = false;
        UIManager.Single.Back();
    }

    private void ChangeHand()
    {
        var hand = GameStateModel.Single.HandMode;
        GameStateModel.Single.HandMode = hand == HandMode.LEFT ? HandMode.RIGHT : HandMode.LEFT;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_CHANGE_HAND);
    }
}