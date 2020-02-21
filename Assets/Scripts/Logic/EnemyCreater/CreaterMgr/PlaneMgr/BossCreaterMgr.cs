using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreaterMgr : IPlaneEnemyMgr {
    
    private IEnemyCreater _bossCreater;
    
    public void Init()
    {
    }

    public void AddCraterItem(IEnemyCreater item)
    {
        _bossCreater = item;
    }
    
    public void Spawn()
    {
        var creater = GetValidCreater();
        if(creater != null)
            creater.Spawn();
    }

    public IEnemyCreater GetValidCreater()
    {
        return _bossCreater;
    }

    public bool JudgeEnd()
    {
        return false;
    }

    public int GetSpawnNum()
    {
        return 1;
    }
}
