[BindPrefab(Paths.PREFAB_SETTING_VIEW, Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class SettingController : ControllerBase
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
        GameStateModel.Single.GameState = GameState.PAUSE;
    }

    private void Exit()
    {
        UIManager.Single.Back();
        GameStateModel.Single.TargetScene = SceneName.Main;
        UIManager.Single.Show(Paths.PREFAB_LOADING_VIEW);
    }

    private void Continue()
    {
        GameStateModel.Single.GameState = GameState.CONTINUE;
        UIManager.Single.Back();
    }

    private void ChangeHand()
    {
        var hand = GameStateModel.Single.HandMode;
        GameStateModel.Single.HandMode = hand == HandMode.LEFT ? HandMode.RIGHT : HandMode.LEFT;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_CHANGE_HAND);
    }
}