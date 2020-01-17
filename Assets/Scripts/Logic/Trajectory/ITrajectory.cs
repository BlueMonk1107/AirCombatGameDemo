using UnityEngine;

public interface ITrajectory
{
    void Init(ITrajectoryData data);
    float GetY(float x, Vector2 startPos);
    float GetX(float y, Vector2 startPos);
    Vector2 GetDirection();
    float GetZRotate();
}