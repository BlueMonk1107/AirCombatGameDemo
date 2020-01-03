using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StarAni : MonoBehaviour
{

	private Vector3 _default;
	private Transform _starUi;
	private float _cameraSpeed;

	private void Awake()
	{
		_default = transform.localScale;
		_starUi = UIManager.Single.GetViwePrefab(Paths.PREFAB_GAME_UI_VIEW).Find("Star/Value");
		
		var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_Game_CONFIG);
		reader["cameraSpeed"].Get<float>(value => { _cameraSpeed = value; });
	}

	private void OnEnable()
	{
		Show();
	}

	private void Show()
	{
		transform.localScale = Vector3.zero;
		transform.DOKill();
		transform.DOScale(_default, 0.3f).OnComplete(Idle);
	}

	private void Idle()
	{
		transform.DOScale(_default*1.2f, 0.5f).SetLoops(-1,LoopType.Yoyo);
	}

	public void Hide(Action callBack)
	{
		float time = 0.8f;
		var pos = Camera.main.ScreenToWorldPoint(_starUi.position) + Vector3.up*_cameraSpeed*time;
		transform.DOKill();
		transform.DOMove(pos, time);
		transform.DOScale(Vector3.zero, time).OnComplete(()=>callBack());
	}
}
