using UnityEngine;

/// <summary>
/// 轨迹接口，提供基础图形轨迹的计算方法
/// </summary>
public interface ITrajectory
{
    void Init(ITrajectoryData data);
    float[] GetY(float x, Vector2 startPos);
    float[] GetX(float y, Vector2 startPos);
    Vector2 GetDirection();
    float GetZRotate();
}