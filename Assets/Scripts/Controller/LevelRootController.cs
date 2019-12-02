using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRootController : ControllerBase {
    protected override void InitChild()
    {
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_LEVEL_CONFIG);
        reader["levelCount"].Get<int>((data) =>
        {
            CoroutineMgr.Single.ExecuteOnce(Wait(data));
        });
        
    }

    private IEnumerator Wait(int count)
    {
        yield return new WaitUntil(() => transform.childCount >= count);
        AddComponent();
        Reacquire();
    }

    private void AddComponent()
    {
        foreach (Transform trans in transform)
        {
            trans.gameObject.AddComponent<LevelItemController>();
        }
    }
}
