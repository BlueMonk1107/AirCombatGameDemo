using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrajectoryDataUtil  {

	public static ITrajectory[] GetTrajectory(PathType type,int level,BulletData bulletData)
	{
		switch (type)
		{
			case PathType.Straight:
				return GetStraightArray(bulletData.trajectory[level]);
			case PathType.Rotate:
				
			default:
				Debug.LogError("当前轨迹未添加，名称："+type);
				return null;
		}
	}

	public static ITrajectory[] GetTrajectory(PathType type, int level, ChangeTrajectoryData data)
	{
		switch (type)
		{
			case PathType.Straight:
				return GetStraightArray(data.trajectory[level]);
			case PathType.Rotate:
			default:
				Debug.LogError("当前轨迹未添加，名称："+type);
				return null;
		}
	}

	private static ITrajectory[] GetStraightArray(int[] data)
	{
		ITrajectory[] array;
		array  = new ITrajectory[data.Length];
		for (int i = 0; i < data.Length; i++)
		{
			var s = new StraightTrajectory();
			s.Init(data[i]);
			array[i] = s;
		}

		return array;
	}
}
