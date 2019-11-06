using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class LoadMgr : NormalSingleton<LoadMgr>,ILoader
{
	[SerializeField]
	private ILoader _loader;

	public LoadMgr()
	{
		_loader  = new ResourceLoader();
	}

	public GameObject LoadPrefab(string path, Transform parent = null)
	{
		return _loader.LoadPrefab(path, parent);
	}

	public void LoadConfig(string path, Action<object> complete)
	{
		_loader.LoadConfig(path,complete);
	}

	public T Load<T>(string path) where T : Object
	{
		return _loader.Load<T>(path);
	}

	public T[] LoadAll<T>(string path) where T : Object
	{
		return _loader.LoadAll<T>(path);
	}
}
