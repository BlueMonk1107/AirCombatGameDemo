using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MsgEventTest : ITest
{
	public void Execute()
	{
		HashSet<int> hashSet = new HashSet<int>();
		Type type = typeof(MsgEvent);
		FieldInfo[] infos = type.GetFields();
		foreach (var info in infos)
		{
			var value = info.GetRawConstantValue();
			if (value is int)
			{
				if (!hashSet.Add((int) value))
				{
					Debug.LogError("MsgEvent中有重复项，重复值为:"+value);
				}
			}
			else
			{
				Debug.LogError("属性："+info.Name+" 类型错误，此类所有常量必须是int类型");
			}
		}
	}
}
