using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BindPrefab(Paths.PREFAB_LEVELS_VIEW,Const.BIND_PREFAB_PRIORITY_VIEW)]
public class LevelsView : ViewBase {
    protected override void InitChild()
    {
        Util.Get("Levels").Go.AddComponent<LevelRoot>();
    }
}
