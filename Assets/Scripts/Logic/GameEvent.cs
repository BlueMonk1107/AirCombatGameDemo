using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MessageMgr.Single.AddListener(MsgEvent.EVENT_END_GAME,GameEnd);
	}

	private void OnDestroy()
	{
		MessageMgr.Single.RemoveListener(MsgEvent.EVENT_END_GAME,GameEnd);
	}


	private void GameEnd(object[] paras)
	{
		UIManager.Single.Show(Paths.PREFAB_GAME_RESULT_VIEW);
	}
}
