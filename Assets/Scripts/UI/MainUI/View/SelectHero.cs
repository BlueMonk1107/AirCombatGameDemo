using UnityEngine;

public class SelectHero : ViewBase
{
    protected override void InitChild()
    {
        foreach (Transform trans in transform) trans.gameObject.AddComponent<HeroItem>();
    }
}