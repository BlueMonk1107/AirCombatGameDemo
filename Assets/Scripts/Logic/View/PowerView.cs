using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerView : PlaneView {
    protected override void InitComponent()
    {
        gameObject.AddComponent<BulletMgr>().Init(PowerBulletModel.Single);
    }
}
