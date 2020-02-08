using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class GameDataMgr : NormalSingleton<GameDataMgr>,IInit
{
	private Dictionary<Type, object> _objects = new Dictionary<Type, object>();

	public void Init()
	{
		InitData<AllBulletData>(Paths.CONFIG_BULLET_CONFIG);
	}

	private void InitData<T>(string path)
	{
		LoadMgr.Single.LoadConfig(path, json =>
		{
			var data = JsonMapper.ToObject<T>((string) json);
			_objects.Add(typeof(T),data);
		});
	}
	
	public T Get<T>()
	{
		Type type = typeof(T);
		if (_objects.ContainsKey(type))
		{
			return (T) _objects[type];
		}
		else
		{
			Debug.LogError("当前类型未初始化，类型名："+type);
			return default(T);
		}
	}
}
