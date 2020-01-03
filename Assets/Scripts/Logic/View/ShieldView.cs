public class ShieldView : EffectView
{
    protected override void InitComponent()
    {
        gameObject.AddComponent<ColliderComponent>();
        gameObject.AddComponent<AutoDestroyComponent>().Init(Const.SHIELD_TIME);
    }
}