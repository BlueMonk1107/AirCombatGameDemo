using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private float _speed;

    public void Init(float speed)
    {
        _speed = speed;
    }

    public void Move(Vector2 direction)
    {
        if (_speed != 0)
            transform.Translate(direction * _speed * Time.deltaTime,Space.World);
     
    }
}