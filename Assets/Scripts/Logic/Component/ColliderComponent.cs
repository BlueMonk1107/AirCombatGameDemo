using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderComponent : MonoBehaviour {
    private void Start()
    {
        Rigidbody2D rigidbody2D = gameObject.AddOrGet<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (IColliderMsg msg in GetComponentsInChildren<IColliderMsg>())
        {
            msg.ColliderMsg(other.transform);
        }
        
        foreach (IColliderMsg msg in other.GetComponentsInChildren<IColliderMsg>())
        {
            msg.ColliderMsg(transform);
        }
    }
}
