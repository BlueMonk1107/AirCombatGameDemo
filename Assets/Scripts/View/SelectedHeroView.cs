using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedHeroView : ViewBase {

    public override void Init()
    {
        Util.Get("Heros").Go.AddComponent<SelectHero>();
    }
}
