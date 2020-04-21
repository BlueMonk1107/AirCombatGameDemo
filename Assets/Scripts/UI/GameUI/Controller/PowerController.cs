public class PowerController : ControllerBase
{
    protected override void InitChild()
    {
        transform.ButtonAction("Icon", Click);
    }

    private void Click()
    {
        if (GameModel.Single.PowerCount > 0)
        {
            GameModel.Single.PowerCount--;
            MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_USE_BOMB);
        }
    }
}