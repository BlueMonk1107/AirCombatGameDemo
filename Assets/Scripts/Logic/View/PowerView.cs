using UnityEngine;

public class PowerView : MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<EmitBulletMgr>().Init(BulletType.Power);
        gameObject.AddOrGet<AutoDestroyComponent>().Init(10);
    }
}