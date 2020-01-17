using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPath
{
    void Init(Transform trans,ITrajectoryData trajectory);
    Vector3 GetPos(int id);
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

    public abstract Vector3 GetPos(int id);

    public abstract Vector2 GetDirection();
    public abstract bool NeedMoveWithCamera();
}
