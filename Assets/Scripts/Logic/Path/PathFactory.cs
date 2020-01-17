using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFactory  {

    public static IPath GetPath(TrajectoryType type)
    {
        switch (type)
        {
            case TrajectoryType.Straight:
                return new StraightPath();
            case TrajectoryType.W:
                return new WPath();
            default:
                Debug.LogError("当前轨迹未添加，名称："+type);
                return null;
        }
    }
}
