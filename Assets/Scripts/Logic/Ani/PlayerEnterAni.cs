using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerEnterAni : MonoBehaviour 
{
	// Use this for initialization
	void Start ()
	{
		Vector3 targetPos = transform.position;
		float yMin = GameUtil.GetCameraMin().y;
		float y = yMin - GetComponent<SpriteRenderer>().bounds.size.y * 0.5f;

		var pos = transform.position;
		pos.y = y;
		transform.position = pos;

		
		var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_Game_CONFIG);
		reader["cameraSpeed"].Get<float>(value =>
		{
			float time = 1;
			transform.DOMove(targetPos +Vector3.up*value*time, time).OnComplete(
				()=>GameStateModel.Single.IsGaming = true);
		});
	}
}
