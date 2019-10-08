using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class BindPrefab : Attribute
{
	public BindPrefab(string path ,Type type)
	{
		Debug.Log("BindPrefab");
		BindUtil.Bind(path,type);
	}
}
