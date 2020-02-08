using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrajectoryDataUtil  {

	public static ITrajectory[] GetTrajectory(PathType type,int level,BulletData bulletData)
	{
		ITrajectory[] array;
		switch (type)
		{
			case PathType.Straight:
				var data = bulletData.trajectory[level];
				array  = new ITrajectory[data.Length];
				for (int i = 0; i < data.Length; i++)
				{
					var s = new StraightTrajectory();
					s.Init(data[i]);
					array[i] = s;
				}

				return array;
			default:
				Debug.LogError("当前轨迹未添加，名称："+type);
				return null;
		}
	}
}
