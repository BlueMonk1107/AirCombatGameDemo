using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessNormalEvent {
    
    public Action SpawnAction { get; private set; }
    public Func<int> SpawnedNum { get; private set; }
    public int SpawnTotalNum { get; private set; }

    public void AddEvent(Action spawnAction,Func<int> spawnedNum,int spawnTotalNum)
    {
        SpawnAction = spawnAction;
        SpawnedNum = spawnedNum;
        SpawnTotalNum = spawnTotalNum;
    }
}
