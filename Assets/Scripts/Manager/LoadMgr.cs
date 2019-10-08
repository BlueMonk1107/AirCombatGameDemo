using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class LoadMgr : NormalSingleton<LoadMgr> 
{
	public GameObject LoadPrefab(string path,Transform parent = null)
	{
		GameObject prefab = Resources.Load<GameObject>(path);
		GameObject temp = Object.Instantiate(prefab,parent);
		temp.AddComponent(BindUtil.GetScriptType(path));
		return temp;
	}
}
