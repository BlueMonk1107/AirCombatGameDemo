using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMgr {
	
	private IPath _path;
	
	public void Init(Transform trans,EnemyData data, ITrajectoryData trajectoryData)
	{
		_path = PathFactory.GetPath(data.trajectoryType);
		_path.Init(trans,trajectoryData);
	}

	public Vector3 GetInitPos(int id)
	{
		return _path.GetInitPos(id);
	}

	public Vector2 GetDirection()
	{
		return _path.GetDirection();
	}
	
	public bool NeedMoveWithCamera()
	{
		return _path.NeedMoveWithCamera();
	}
}
