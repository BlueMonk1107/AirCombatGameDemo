using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateModel : NormalSingleton<GameStateModel> {

	public SceneName CurrentScene { get; set; }
	public SceneName TargetScene { get; set; }
	public Hero SelectedHero { get; set; }
	public int SelectedPlaneId { get; set; }

	public int Level
	{
		get
		{
			string key = KeysUtil.GetPropertyKeys(DataKeys.LEVEL);
			return DataMgr.Single.Get<int>(key);
		}
	}

	public int GetMoney(string key)
	{
		int money = 0;
		switch (key)
		{
			case DataKeys.STAR:
				money = DataMgr.Single.Get<int>(DataKeys.STAR);
				break;
			case DataKeys.DIAMOND:
				money = DataMgr.Single.Get<int>(DataKeys.DIAMOND);
				break;
			default:
				Debug.LogError("当前传入的key无法识别，key："+key);
				break;
		}

		return money;
	}

	public void SetMoney(string key,int money)
	{
		switch (key)
		{
			case DataKeys.STAR:
				DataMgr.Single.Set(DataKeys.STAR,money);
				break;
			case DataKeys.DIAMOND:
				DataMgr.Single.Set(DataKeys.DIAMOND,money);
				break;
			default:
				Debug.LogError("当前传入的key无法识别，key："+key);
				break;
		}
	}
}
