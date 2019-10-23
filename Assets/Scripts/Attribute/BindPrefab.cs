using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class BindPrefab : Attribute
{
	public string Path { get; private set; }

	public BindPrefab(string path)
	{
		Path = path;
	}
}
