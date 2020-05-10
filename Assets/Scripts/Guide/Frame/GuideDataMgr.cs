using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideDataMgr {
	
	private static GuideDataMgr _single;

	public static GuideDataMgr Single
	{
		get
		{
			if (_single == null)
			{
				var t = new GuideDataMgr();
				_single = t;
			}

			return _single;
		}
	}

	public void SaveData(int key,bool value = true)
	{
		PlayerPrefs.SetInt(key.ToString(),Convert.ToInt32(value));
	}
	
	public bool GetData(int key)
	{
		int result = PlayerPrefs.GetInt(key.ToString(),Convert.ToInt32(false));
		return Convert.ToBoolean(result);
	}
}
