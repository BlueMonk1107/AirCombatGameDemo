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
		ConfigMgr.Single.Init();
		
		InitCustomAttributes initAtt = new InitCustomAttributes();
		initAtt.Init();

		UIManager.Single.Show(Paths.START_VIEW);
	}
}
