using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneProperty : ViewBase {
    protected override void InitChild()
    {
        LoadMgr.Single.LoadPrefab(Paths.PROPERTY_ITEM);
    }

    public void UpdateData(int id)
    {
        
    }
}
