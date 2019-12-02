using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycleAddConfig : IInit{

    public ArrayList Objects { get; private set; }

    public void Init()
    {
        Objects = new ArrayList();
        Add();
    }
    
    private void Add()
    {
        Objects.Add(ConfigMgr.Single);
        Objects.Add(new InitCustomAttributes());
        Objects.Add(PlaneSpritesModel.Single);
        Objects.Add(AudioMgr.Single);
    }
}
