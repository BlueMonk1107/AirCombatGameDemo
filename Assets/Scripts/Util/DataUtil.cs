using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public static class DataUtil  {

	public static void SetJsonData(this DataMgr mgr,string key, JsonData data)
	{
		IJsonWrapper jsonWrapper = data;
		switch (data.GetJsonType())
		{
			case JsonType.None:
				Debug.Log("当前jsondata数据为空");
				break;
			case JsonType.Object:
				SetObjectData(key,data);
				break;
			case JsonType.String:
				DataMgr.Single.Set(key,jsonWrapper.GetString());
				break;
			case JsonType.Int:
				DataMgr.Single.Set(key,jsonWrapper.GetInt());
				break;
			case JsonType.Long:
				DataMgr.Single.Set(key,(int)jsonWrapper.GetLong());
				break;
			case JsonType.Double:
				DataMgr.Single.Set(key,(float)jsonWrapper.GetDouble());
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private static void SetObjectData(string oldkey,JsonData data)
	{
		foreach (string key in data.Keys)
		{
			string newKey = oldkey + key;
			if (!DataMgr.Single.Contains(newKey))
			{
				DataMgr.Single.SetJsonData(newKey,data[key]);
			}
		}
	}
}
