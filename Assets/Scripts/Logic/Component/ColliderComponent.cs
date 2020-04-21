using UnityEngine;

public class ColliderComponent : MonoBehaviour
{
    private void Start()
    {
        var rigidbody2D = gameObject.AddOrGet<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var msg in GetComponentsInChildren<IColliderMsg>()) 
            msg.ColliderMsg(other.transform);

        foreach (var msg in other.GetComponentsInChildren<IColliderMsg>()) 
            msg.ColliderMsg(transform);
    }
}