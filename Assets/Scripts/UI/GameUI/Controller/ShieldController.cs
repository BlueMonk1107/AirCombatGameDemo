public class ShieldController : ControllerBase
{
    protected override void InitChild()
    {
        transform.ButtonAction("Icon", Click);
    }

    private void Click()
    {
        if (GameModel.Single.ShieldCount > 0)
        {
            GameModel.Single.ShieldCount--;
            MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_USE_SHIELD);
        }
    }
}