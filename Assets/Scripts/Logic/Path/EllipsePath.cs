using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipsePath : PathBase
{
	private EnterPath _enter = new EnterPath();
	private EllipseData _data;

	public override void Init(Transform trans, ITrajectoryData data)
	{
		base.Init(trans,data);
		if (data is EllipseData)
		{
			_data = data as EllipseData;
		}
		else
		{
			Debug.LogError("当前传入椭圆形路径的数据类型错误，类型为："+data);
			return;
		}
		
		_data.Center = new Vector2(0,(GameUtil.GetCameraMax().y - GameUtil.GetCameraMin().y) * (float)_data.YRatioInScreen);

		float offsetY =  _data.Center.y + (float)_data.YRadius;
		_enter.InitByOffsetY(_trans,0,offsetY,EnterPath.MoveDirection.UP_TO_DOWN);
		
		Vector2 startPos = new Vector2(_trans.position.x,offsetY);
		_data.StartPos = startPos;
		
		_trajectory = new EllipseTrajectory();
		_trajectory.Init(_data);
	}
	
	public override Vector3 GetInitPos(int id)
	{
		return _enter.GetInitPos(id);
	}

	public override Vector2 GetDirection()
	{
		_data.XPos = _trans.position.x;
		if (_enter.GetDirection() == Vector2.zero)
		{
			return _trajectory.GetDirection();
		}
		else
		{
			return _enter.GetDirection();
		}
		
	}

	public override bool NeedMoveWithCamera()
	{
		return true;
	}
}
