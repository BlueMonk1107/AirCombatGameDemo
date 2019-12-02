using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class LaunchGame : MonoBehaviour {
	// Use this for initialization
	void Start ()
	{
		//DataMgr.Single.ClearAll();

		GameStateModel.Single.CurrentScene = SceneName.Main;
		IInit lifeCycle = LifeCycleMgr.Single;
		lifeCycle.Init();

		UIManager.Single.Show(Paths.PREFAB_START_VIEW);
	}
}
