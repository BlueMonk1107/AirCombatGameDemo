using DG.Tweening;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DORotate(Vector3.forward * 90, 0.05f).SetLoops(-1, LoopType.Incremental);
    }

    private void OnDisable()
    {
        transform.DOKill();
        transform.localEulerAngles = Vector3.zero;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}