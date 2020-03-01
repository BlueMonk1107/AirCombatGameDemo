using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletPointMgr : MonoBehaviour
{
	private Vector3[] _points;
	private EllipseTrajectory _trajectory;
	private float _xMin;
	private float _validWidth;
	private Vector3[] _tempArray;
	private float _width;
	private float _centerX;
	
	public void Init()
	{
		_width = transform.GetComponentInParent<SpriteRenderer>().bounds.size.x;
		InitTrajectory(_width);
		InitXLimit(_width);
	}

	private void InitTrajectory(float width)
	{
		EllipseData data = new EllipseData();
		data.XRadius = width / 2;
		data.YRadius = 0.3f;
		var pos = transform.position;
		pos.y -= (float)data.YRadius;
		_centerX = pos.x;
		data.Center = pos; 
        
		_trajectory = new EllipseTrajectory();
		_trajectory.Init(data);
	}

	private void InitXLimit(float width)
	{
		float offset = width / 4;
		_xMin = -offset;
		_validWidth = offset*2;
	}

	public Vector3[] GetPoints(int count,Vector3 pos)
	{
		Vector3[] posArray = GetRelativePos(count);
		_tempArray = new Vector3[count];
		for (int i = 0; i < count; i++)
		{
			_tempArray[i] = new Vector3(posArray[i].x+pos.x,posArray[i].y+pos.y,posArray[i].z+pos.z);
		}

		return _tempArray;
	}

	private Vector3[] GetRelativePos(int count)
	{
		if (_points == null || _points.Length != count)
		{
			InitTrajectory(_width);
			_points = new Vector3[count];

			int piece = count + 1;
			float offset = _validWidth / piece;

			float x = _xMin + _centerX;
			for (int i = 0; i < count; i++)
			{
				x += offset;
				_points[i] = new Vector3(x - transform.position.x,_trajectory.GetY(x,Vector2.zero)[0] - transform.position.y);
			}

			return _points;
		}
		else
		{
			return _points;
		}
	}
}
