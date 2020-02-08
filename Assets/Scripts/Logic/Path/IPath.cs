using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 路径接口，提供具体的路径的计算方法
/// </summary>
public interface IPath
{
    void Init(Transform trans,ITrajectoryData trajectory);
    Vector3 GetInitPos(int id);
    Vector2 GetDirection();
    bool NeedMoveWithCamera();
}

public abstract class PathBase : IPath
{
    protected PathState _state;
    protected ITrajectory _trajectory;
    protected Transform _trans;
    public virtual void Init(Transform trans, ITrajectoryData trajectory)
    {
        _trans = trans;
    }

    public abstract Vector3 GetInitPos(int id);

    public abstract Vector2 GetDirection();
    public abstract bool NeedMoveWithCamera();
}
