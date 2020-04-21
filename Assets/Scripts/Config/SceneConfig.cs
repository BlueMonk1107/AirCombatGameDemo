public class SceneConfig : NormalSingleton<SceneConfig>, IInit
{
    public void Init()
    {
        AddLoaded();
    }

    private void AddLoaded()
    {
        SceneMgr.Single.AddSceneLoaded(SceneName.Game, callBack => PoolMgr.Single.Init(callBack));
        SceneMgr.Single.AddSceneLoaded(SceneName.Game, callBack =>
        {
            var gameRoot = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_GAME_ROOT);
            var root = gameRoot.AddComponent<GameRoot>();
            root.Init();
            callBack();
        });
    }
}