using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrajectoryDataUtil  {

	public static ITrajectory[] GetStraightArray(int[] data)
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
