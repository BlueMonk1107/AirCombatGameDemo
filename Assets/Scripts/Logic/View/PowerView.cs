using UnityEngine;

public class PowerView : MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<EmitBulletMgr>().Init(BulletType.Power);
    }
}