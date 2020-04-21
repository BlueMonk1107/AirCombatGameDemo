using UnityEngine;

public class MapMgr : BGView
{
    public void Init(Transform camera)
    {
        var map0 = transform.Find("map_0");
        var map1 = transform.Find("map_1");
        var offset = Mathf.Abs(map1.position.y - map0.position.y);

        var item0 = map0.gameObject.AddComponent<MapItem>();
        item0.Init(offset, camera);
        var item1 = map1.gameObject.AddComponent<MapItem>();
        item1.Init(offset, camera);
    }
}