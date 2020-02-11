using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnTopPath : PathBase
{
    private EnterPath _enterPath = new EnterPath();
    
    public override void Init(Transform trans, ITrajectoryData trajectory)
    {
        base.Init(trans, trajectory);
        _enterPath.InitByRatio(trans,0,0.8f,EnterPath.MoveDirection.UP_TO_DOWN);
    }

    public override Vector3 GetInitPos(int id)
    {
        return _enterPath.GetInitPos(id);
    }

    public override Vector2 GetDirection()
    {
        return _enterPath.GetDirection();
    }

    public override bool NeedMoveWithCamera()
    {
        return true;
    }
}
