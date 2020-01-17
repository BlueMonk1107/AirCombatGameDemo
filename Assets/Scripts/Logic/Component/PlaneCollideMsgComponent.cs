using System.Linq;
using UnityEngine;

public class PlaneCollideMsgComponent : MonoBehaviour, IColliderMsg
{
    private IBehaviour _behaviour;
    private IBullet _selfBullet;
    
    // Use this for initialization
    private void Start()
    {
        _selfBullet = GetComponentInChildren<IBullet>();
        _behaviour = GetComponent<IBehaviour>();
    }

    public void ColliderMsg(Transform other)
    {
        var bullet = other.GetComponentInChildren<IBullet>();
        if (other.tag == Tags.BULLET
            && bullet != null
            && _selfBullet != null
            && bullet.Tagets.Contains(_selfBullet.Owner)
        )
        {
            if (_behaviour != null) _behaviour.Injure(bullet.GetAttack());
        }
        else if (_selfBullet != null && _selfBullet.GetTargetTags().Contains(other.tag))
        {
            if (_behaviour != null) _behaviour.Dead();
        }
    }
}