using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public void Init()
    {
        var layer = gameObject.AddComponent<GameLayerMgr>();
        layer.Init();
        
        GameModel.Single.InitData();

        var camera = transform.Find("Main Camera");
        camera.AddComponent<CameraMove>();
        camera.AddComponent<GameProcessMgr>().Init();

        var mapGo = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_MAP_MGR);
        var map = mapGo.AddComponent<MapMgr>();
        map.Init(camera);

        var player = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_PLANE);
        player.AddComponent<PlayerView>();

        gameObject.AddComponent<GameEvent>();
    }
}