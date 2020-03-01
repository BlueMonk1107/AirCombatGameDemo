[BindPrefab(Paths.PREFAB_GAME_UI_VIEW, Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class GameUIController : ControllerBase
{
    protected override void InitChild()
    {
        transform.AddComponent<ShieldController>("Shield");
        transform.AddComponent<PowerController>("Bomb");
        transform.ButtonAction("Pause", () => UIManager.Single.Show(Paths.PREFAB_SETTING_VIEW));
    }
}