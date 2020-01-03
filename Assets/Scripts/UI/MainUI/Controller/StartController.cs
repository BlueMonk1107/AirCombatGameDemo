[BindPrefab(Paths.PREFAB_START_VIEW, Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class StartController : ControllerBase
{
    protected override void InitChild()
    {
        transform.ButtonAction("Start", () =>
        {
            UIManager.Single.Show(Paths.PREFAB_SELECTED_HERO_VIEW);
            AudioMgr.Single.PlayOnce(UIAduio.UI_StartGame.ToString());
        }, false);

        //AudioMgr.Single.PlayBG(BGAudio.Game_BG);
    }
}