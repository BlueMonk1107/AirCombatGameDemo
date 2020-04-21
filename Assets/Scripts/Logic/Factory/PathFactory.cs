using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFactory  {

    public static IPath GetPath(PathType type)
    {
        switch (type)
        {
            case PathType.Straight:
                return new StraightPath();
            case PathType.W:
                return new WPath();
            case PathType.StayOnTop:
                return new StayOnTopPath();
            case PathType.Ellipse:
                return new EllipsePath();
            default:
                Debug.LogError("当前轨迹未添加，名称："+type);
                return null;
        }
    }
}
