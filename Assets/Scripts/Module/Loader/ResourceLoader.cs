using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceLoader : ILoader {

	public GameObject LoadPrefab(string path,Transform parent = null)
	{
		GameObject prefab = Resources.Load<GameObject>(path);
		GameObject temp = UnityEngine.Object.Instantiate(prefab,parent);
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

	public void LoadConfig(string path, Action<object> complete)
	{
		if (complete == null)
			return;

		string localPath = "";
		if (Application.platform == RuntimePlatform.Android)
		{
			localPath = path;
		}
		else
		{
			localPath = "file:///" + path;
		}

		WWW www = new WWW(localPath); 

		if (www.error != null)
		{
			Debug.LogError("load config error,path is " + localPath);
			complete(null);
		}

		while (!www.isDone)
		{
		}

		Debug.Log("load config success,path is "+path);
		complete(www.text);
	}
}
