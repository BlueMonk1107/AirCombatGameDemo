public class PowerView : PlaneView
{
    protected override void InitComponent()
    {
        gameObject.AddComponent<EmitBulletMgr>().Init(BulletType.Power);
    }
}