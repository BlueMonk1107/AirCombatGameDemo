using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPath : PathBase
{
    public override void Init(Transform trans,ITrajectoryData data)
    {
        base.Init(trans, data);
        _trajectory = new StraightTrajectory();
        _trajectory.Init(data);
    }

    public override Vector3 GetPos(int id)
    {
        var render = _trans.GetComponent<SpriteRenderer>();
        float height = render.bounds.max.y - render.bounds.min.y;
        Vector3 startPos = _trans.position;
        float yOffset = height * 0.5f + height * id;
        float y = startPos.y + yOffset;
        float x = _trajectory.GetX(y, startPos);
        return new Vector3(x,y,_trans.position.z);
    }

    public override Vector2 GetDirection()
    {
        return _trajectory.GetDirection().Reversal();
    }

    public override bool NeedMoveWithCamera()
    {
        return false;
    }
}
