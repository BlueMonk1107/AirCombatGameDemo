using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialStateFactory : NormalSingleton<SpecialStateFactory> {

	public static IBuff GetBuff(BuffType type)
	{
		switch (type)
		{
			case BuffType.LEVEL_UP:
				return new LevelUpState();
		}

		Debug.LogError("当前状态并不在工厂的配置中，类型："+type);
		return null;
	}
	
	public static IDebuff GetDebuff(DebuffType type)
	{
		switch (type)
		{
		}

		Debug.LogError("当前状态并不在工厂的配置中，类型："+type);
		return null;
	}
}
