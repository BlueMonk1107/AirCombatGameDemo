[BindPrefab(Paths.PREFAB_LEVELS_VIEW, Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class LevelsController : ControllerBase
{
    protected override void InitChild()
    {
        transform.ButtonAction("Back", UIManager.Single.Back);
        transform.Find("Levels").gameObject.AddComponent<LevelRootController>();
    }
}