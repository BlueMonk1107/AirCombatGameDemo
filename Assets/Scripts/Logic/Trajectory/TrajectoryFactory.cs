using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryFactory  {

	public static ITrajectory GetTrajectory(TrajectoryType type)
	{
		switch (type)
		{
			case TrajectoryType.Straight:
				return new StraightTrajectory();
			case TrajectoryType.W:
				return new VTrajectory();
			default:
				Debug.LogError("当前轨迹未添加，名称："+type);
				return null;
		}
	}
}
