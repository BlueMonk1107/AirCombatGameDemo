using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : ILoader {

	public GameObject LoadPrefab(string path,Transform parent = null)
	{
		GameObject prefab = Resources.Load<GameObject>(path);
		GameObject temp = Object.Instantiate(prefab,parent);
		temp.AddComponent(BindUtil.GetScriptType(path));
		return temp;
	}

	public Sprite LoadSprite(string path)
	{
		Sprite sprite = Resources.Load<Sprite>(path);
		if (sprite == null)
		{
			Debug.LogError("sprite is null,path is "+path);
			return null;
		}
		else
		{
			return sprite;
		}
	}
	
	public Sprite[] LoadAllSprites(string path)
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>(path);
		if (sprites == null || sprites.Length == 0)
		{
			Debug.LogError("no sprite in the folder,path is "+path);
			return null;
		}
		else
		{
			return sprites;
		}
	}
}
