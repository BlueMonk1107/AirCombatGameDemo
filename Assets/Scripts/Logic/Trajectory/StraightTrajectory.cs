

using UnityEngine;

/// <summary>
/// 直线轨迹
/// </summary>
public class StraightTrajectory : ITrajectory
{
    private Vector2 _direction;
    private float _k;
    private float _zRotate;

    public void Init(float angle)
    {
        _direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        _k = Mathf.Tan(angle * Mathf.Deg2Rad);
        _zRotate = angle - 90;
    }

    public void Init(ITrajectoryData data)
    {
        if (data is StraightTrajectoryData)
        {
            var temp = (StraightTrajectoryData) data;
            Init((float)temp.Angle);
        }
        else
        {
            Debug.LogError("当前传入数据不是直线轨迹数据.data:"+data);
        }
    }

    public float GetY(float x, Vector2 startPos)
    {
        return (x - startPos.x) * _k + startPos.y;
    }

    public float GetX(float y, Vector2 startPos)
    {
        return (y - startPos.y) / _k + startPos.x;
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }

    public float GetZRotate()
    {
        return _zRotate;
    }
}