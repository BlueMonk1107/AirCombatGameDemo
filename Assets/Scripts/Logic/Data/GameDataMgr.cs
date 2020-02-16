using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class GameDataMgr : NormalSingleton<GameDataMgr>,IInit
{
	private Dictionary<Type, object> _objects = new Dictionary<Type, object>();

	private BulletName[] _bossBullets =
	{
		BulletName.Enemy_Boss_0,
		BulletName.Enemy_Boss_1
	};

	public void Init()
	{
		InitData<AllBulletData>(Paths.CONFIG_BULLET_CONFIG,InitBulletData);
	}

	private void InitData<T>(string path,Action callback)
	{
		LoadMgr.Single.LoadConfig(path, json =>
		{
			var data = JsonMapper.ToObject<T>((string) json);
			_objects.Add(typeof(T),data);
			if (callback != null)
				callback();
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

	private void InitBulletData()
	{
		
		IReader reader = ReaderMgr.Single.GetReader(Paths.CONFIG_BULLET_CONFIG);

		foreach (BulletName bullet in _bossBullets)
		{
			TaskQueueMgr<JsonData>.Single.AddQueue(()=>reader[bullet.ToString()]["Events"]);
		}
		
		TaskQueueMgr<JsonData>.Single.Execute(CallBack);


		
	}

	private void CallBack(JsonData[] data)
	{
		Dictionary<BulletName,List<BulletEventData> > datas = new Dictionary<BulletName, List<BulletEventData>>();
		for (int i = 0; i < data.Length; i++)
		{
			datas[_bossBullets[i]] = new List<BulletEventData>();
			foreach (JsonData jsonData in data[i])
			{
				var json = jsonData["Data"].ToJson();
				var type = (BulletEventType) int.Parse(jsonData["Type"].ToJson());
				BulletEventData temp;
				switch (type)
				{
					case BulletEventType.ChangeSpeed:
						temp = JsonMapper.ToObject<ChangeSpeedData>(json);
						break;
					case BulletEventType.ChangeTrajectory:
						temp = JsonMapper.ToObject<ChangeTrajectoryData>(json);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
						
				datas[_bossBullets[i]].Add(temp);
			}
		}
		
		
		AllBulletData bulletData = _objects[typeof(AllBulletData)] as AllBulletData;
		foreach (var pair in datas)
		{
			switch (pair.Key)
			{
				case BulletName.Enemy_Boss_0:
					for (int i = 0; i < bulletData.Enemy_Boss_0.Events.Length; i++)
					{
						bulletData.Enemy_Boss_0.Events[i].Data = pair.Value[i];
					}
					break;
				case BulletName.Enemy_Boss_1:
					for (int i = 0; i < bulletData.Enemy_Boss_1.Events.Length; i++)
					{
						bulletData.Enemy_Boss_1.Events[i].Data = pair.Value[i];
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
	}
}
