[BindPrefab(Paths.PREFAB_SELECTED_HERO_VIEW, Const.BIND_PREFAB_PRIORITY_VIEW)]
public class SelectedHeroView : ViewBase
{
    protected override void InitChild()
    {
        Util.Get("Heros").Go.AddComponent<SelectHero>();
    }
}