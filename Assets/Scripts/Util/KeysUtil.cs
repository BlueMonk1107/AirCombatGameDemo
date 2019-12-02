using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysUtil {

	public static string GetPropertyKeys(string name)
	{
		int id = GameStateModel.Single.SelectedPlaneId;
		return id + name;
	}
	
	public static string GetPropertyKeys(int id,string name)
	{
		return id + name;
	}
	
	public static string GetNewKey(PropertyItem.ItemKey key,string propertyName)
	{
		return GetPropertyKeys(propertyName + key);
	}
}
