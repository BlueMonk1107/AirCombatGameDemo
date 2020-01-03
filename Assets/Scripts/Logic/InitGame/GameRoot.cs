using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public void Init()
    {
        var layer = gameObject.AddComponent<GameLayerMgr>();
        layer.Init();


        var camera = transform.Find("Main Camera");
        camera.AddComponent<CameraMove>();
        camera.AddComponent<EnemyCreaterMgr>().Init();

        var mapGo = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_MAP_MGR);
        var map = mapGo.AddComponent<MapMgr>();
        map.Init(camera);

        var player = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_PLANE);
        player.AddComponent<PlayerView>();
    }
}