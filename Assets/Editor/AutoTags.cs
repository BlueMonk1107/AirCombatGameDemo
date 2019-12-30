using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[InitializeOnLoad]
public class AutoTags  
{

	static AutoTags()
	{
		AddUnityTag();
		AddPrefabTag();
	}

	private static void AddUnityTag()
	{
		Type type = typeof(Tags);
		var infos = type.GetFields();
		foreach (FieldInfo info in infos)
		{
			InternalEditorUtility.AddTag((string)info.GetRawConstantValue());
		}
	}

	private static void AddPrefabTag()
	{
		GameObject prefab = Resources.Load<GameObject>(Paths.PREFAB_PLAYER);
		prefab.tag = Tags.PLAYER;
		prefab = Resources.Load<GameObject>(Paths.PREFAB_BULLET);
		prefab.tag = Tags.BULLET;
		prefab = Resources.Load<GameObject>(Paths.EFFECT_SHIELD);
		prefab.tag = Tags.SHIELD;
		foreach (GameObject o in Resources.LoadAll<GameObject>(Paths.ENEMY_FOLDER))
		{
			o.tag = Tags.ENEMY;
		}
	}
}
