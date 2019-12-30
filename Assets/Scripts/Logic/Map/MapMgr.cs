using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr : BGView
{
	public void Init(Transform camera)
	{
		Transform map0 = transform.Find("map_0");
		Transform map1 = transform.Find("map_1");
		float offset = Mathf.Abs(map1.position.y - map0.position.y);

		var item0 = map0.gameObject.AddComponent<MapItem>();
		item0.Init(offset,camera);
		var item1 = map1.gameObject.AddComponent<MapItem>();
		item1.Init(offset,camera);
	}
}
