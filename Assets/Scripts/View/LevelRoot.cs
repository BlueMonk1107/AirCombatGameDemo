using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class LevelRoot : ViewBase {

    private enum Name
    {
        levelCount = 0,
        COUNT
    }
    
    protected override void InitChild()
    {
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_LEVEL_CONFIG);
        for (Name i = 0; i < Name.COUNT; i++)
        {
            Name temp = i;
            TaskQueueMgr<int>.Single.AddQueue(()=>reader[temp.ToString()]);
        }
        
        TaskQueueMgr<int>.Single.Execute(SpawnLevelItem);
    }
    

    private void SpawnLevelItem(int[] values)
    {
        if (values.Length != (int) Name.COUNT)
        {
            Debug.LogError("返回数据数量不匹配，正确数量:"+(int) Name.COUNT+" 当前数量："+values.Length);
            return;
        }
        int levelCount = values[(int)Name.levelCount];

        SpawnItem(levelCount);
    }

    private void SpawnItem(int count)
    {
        GameObject go;
        LevelItem item;
        for (int i = 0; i < count; i++)
        {
            go = LoadMgr.Single.LoadPrefab(Paths.PREFAB_LEVEL_ITEM,transform);
            go.AddComponent<LevelItem>();
        }
        
        Reacquire();
    }
}
