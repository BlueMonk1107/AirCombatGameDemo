[BindPrefab(Paths.PREFAB_LOADING_VIEW, Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class LoadingController : ControllerBase
{
    protected override void InitChild()
    {
    }

    public override void Show()
    {
        base.Show();
        if (GameStateModel.Single.TargetScene != GameStateModel.Single.CurrentScene
            && GameStateModel.Single.TargetScene != SceneName.NULL)
        {
            SceneMgr.Single.AsyncLoadScene(GameStateModel.Single.TargetScene);
            LifeCycleMgr.Single.Add(LifeName.UPDATE, this);
        }
    }

    public override void UpdateFun()
    {
        base.UpdateFun();
        if (SceneMgr.Single.Process() == 1)
        {
            if (GameStateModel.Single.TargetScene == SceneName.Game)
                UIManager.Single.Show(Paths.PREFAB_GAME_UI_VIEW);
            else if (GameStateModel.Single.TargetScene == SceneName.Main) UIManager.Single.Back();

            UIManager.Single.Hide(Paths.PREFAB_LOADING_VIEW);
        }
    }

    public override void Hide()
    {
        base.Hide();
        if (GameStateModel.Single.TargetScene != SceneName.NULL) GameStateModel.Single.TargetScene = SceneName.NULL;

        LifeCycleMgr.Single.Remove(LifeName.UPDATE, this);
    }
}