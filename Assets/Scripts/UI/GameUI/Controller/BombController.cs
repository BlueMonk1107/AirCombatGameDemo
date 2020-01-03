public class BombController : ControllerBase
{
    protected override void InitChild()
    {
        transform.ButtonAction("Icon", Click);
    }

    private void Click()
    {
        if (GameModel.Single.BombCount > 0) GameModel.Single.BombCount--;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_USE_BOMB);
    }
}