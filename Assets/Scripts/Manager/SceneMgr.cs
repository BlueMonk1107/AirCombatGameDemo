using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : NormalSingleton<SceneMgr>
{

	private AsyncOperation _async;

	public void AsyncLoadScene(SceneName name)
	{
		CoroutineMgr.Single.ExecuteOnce(AsyncLoad(name.ToString()));
	}

	private IEnumerator AsyncLoad(string name)
	{
		_async = SceneManager.LoadSceneAsync(name);
		_async.allowSceneActivation = false;
		yield return _async;
	}

	public float Process()
	{
		if (_async == null)
			return 0;
		
		if (_async.progress >= 0.9f)
		{
			return 1;
		}
		else
		{
			return _async.progress;
		}
	}

	public void SceneActivation()
	{
		if(_async == null)
			return;
		
		_async.allowSceneActivation = true;
		_async = null;
	}
}
