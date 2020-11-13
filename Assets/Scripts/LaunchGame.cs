using System;
using System.Collections;
using System.Linq;
using LitJson;
using UnityEngine;

public class LaunchGame : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        if (FindObjectsOfType<LaunchGame>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return TestMgr.Single.Init();
        DataMgr.Single.ClearAll(); 
        GameStateModel.Single.CurrentScene = SceneName.Main;
        IInit lifeCycle = LifeCycleMgr.Single;
        lifeCycle.Init();
        
        GuideUiMgr.Single.Init(transform);
        GuideMgr.Single.InitGuide();
        
        UIManager.Single.Init(GetComponent<Canvas>());
        UIManager.Single.Show(Paths.PREFAB_START_VIEW);

        DontDestroyOnLoad(gameObject);
    }
}
