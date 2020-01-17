using System;
using System.Linq;
using UnityEngine;

public class LaunchGame : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        TestMgr.Single.Init();

        if (FindObjectsOfType<LaunchGame>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        //DataMgr.Single.ClearAll(); 

        GameStateModel.Single.CurrentScene = SceneName.Main;
        IInit lifeCycle = LifeCycleMgr.Single;
        lifeCycle.Init();

        UIManager.Single.Show(Paths.PREFAB_START_VIEW);

        DontDestroyOnLoad(gameObject);
    }
}