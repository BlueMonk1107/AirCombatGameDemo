using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {

    public void Init()
    {
        var layer = gameObject.AddComponent<GameLayerMgr>();
        layer.Init();
        
        Transform camera = transform.Find("Main Camera");
        camera.AddComponent<CameraMove>();

        var mapGo = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_MAP_MGR);
        var map = mapGo.AddComponent<MapMgr>();
        map.Init(camera);

        var player = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_PLAYER);
        player.AddComponent<PlayerView>();
    }
}
