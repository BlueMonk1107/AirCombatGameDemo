using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitCustomAttributes {

	public void Init()
	{
		System.Reflection.Assembly asm = System.Reflection.Assembly.GetAssembly(typeof(BindPrefab));
		System.Type[] types = asm.GetExportedTypes();

		foreach (Type type in types)
		{
			foreach (Attribute attribute in Attribute.GetCustomAttributes(type,true))
			{
				if (attribute is BindPrefab)
				{
					BindPrefab data = attribute as BindPrefab;
					BindUtil.Bind(data.Path,type);
				}
			}
		}
	}
}
