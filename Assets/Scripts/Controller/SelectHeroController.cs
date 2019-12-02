using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroController : ControllerBase {
    protected override void InitChild()
    {
        foreach (Transform trans in transform)
        {
            trans.gameObject.AddComponent<HeroItemController>();
        }
    }
}
