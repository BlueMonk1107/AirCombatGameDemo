using UnityEngine;

public class BulletBehaviour : MonoBehaviour, IBehaviour
{
    public void Injure(int value)
    {
        Destroy();
    }

    public void Dead()
    {
        Destroy();
    }

    private void Destroy()
    {
        AniMgr.Single.BulletDestroyAni(transform.position);
        PoolMgr.Single.Despawn(gameObject);
    }
}