using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlyIntoUI : IEffect {
    
    private Transform _starUi;
    private float _cameraSpeed;
    private Transform _transform;
    private float _duration;
    private Action _callBack;

    public void Init(Transform transform,float duration,string uiPath,string childPath,Action callBack)
    {
        _transform = transform;
        _duration = duration;
        _callBack = callBack;
        _starUi = UIManager.Single.GetViwePrefab(uiPath).Find(childPath);
		
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_Game_CONFIG);
        reader["cameraSpeed"].Get<float>(value => { _cameraSpeed = value; });
    }
    public void Begin()
    {
        var pos = Camera.main.ScreenToWorldPoint(_starUi.position) + Vector3.up*_cameraSpeed*_duration;
        _transform.DOKill();
        _transform.DOMove(pos, _duration);
        _transform.DOScale(Vector3.zero, _duration).OnComplete(() =>
        {
            if (_callBack != null)
                _callBack();
        });
    }

    public void Stop(Action callBack)
    {
        if (callBack != null)
            callBack();
    }

    public void Hide()
    {
    }

    public void Clear()
    {
        _transform = null;
        _duration = 0;
        _starUi = null;
    }
}
