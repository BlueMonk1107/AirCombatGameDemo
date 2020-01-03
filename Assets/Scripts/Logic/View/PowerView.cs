public class PowerView : PlaneView
{
    protected override void InitComponent()
    {
        gameObject.AddComponent<BulletMgr>().Init(PowerBulletModel.Single);
    }
}