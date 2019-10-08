using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : NormalSingleton<SceneMgr> {

	public void SwitchScene(string name)
	{
		SceneManager.LoadScene(name);
	}
}
