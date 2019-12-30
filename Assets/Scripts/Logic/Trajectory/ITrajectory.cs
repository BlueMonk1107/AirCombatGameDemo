using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrajectory
{
    void Init(float angle);
    float GetX(float y,Vector2 startPos);
    Vector2 GetDirection();
    float GetZRotate();
}

/// <summary>
/// 直线轨迹
/// </summary>
public class StraightTrajectory : ITrajectory
{
    private float _k;
    private Vector2 _direction;
    private float _zRotate;
    
    public void Init(float angle)
    {
        _direction = new Vector2(Mathf.Cos(angle* Mathf.Deg2Rad),Mathf.Sin(angle* Mathf.Deg2Rad));
        _k = Mathf.Tan(angle * Mathf.Deg2Rad);
        _zRotate = angle - 90;
    }
    
    public float GetX(float y,Vector2 startPos)
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