using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneProperty : ViewBase {
    
    public enum Property
    {
        attack = 0,
        fireRate,
        life,
        COUNT
    }

    private List<PropertyItem> _items;
    
    protected override void InitChild()
    {
        _items = new List<PropertyItem>((int)Property.COUNT);
        
        for (Property i = 0; i < Property.COUNT; i++)
        {
            GameObject item = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_PROPERTY_ITEM, transform);
            PropertyItem itemS = item.AddComponent<PropertyItem>();
            itemS.Init(i.ToString());
            _items.Add(itemS);
        }
    }
    
}
