using System;
using DG.Tweening;
using UnityEngine;

public class ScaleEffect : IEffect
{
    private Vector3 _default;
    private Transform _transform;
    private Vector3 _scaleMax, _scaleMin;
    private float _duration;
    private int _loopTimes;
    private LoopType _loopType;
	
    public void Init(Transform transform,Vector3 scaleMax,Vector3 scaleMin,float duration,int loopTimes,LoopType loopType)
    {
        _transform = transform;
        _default = transform.localScale;
        _scaleMax = scaleMax;
        _scaleMin = scaleMin;
        _duration = duration;
        _loopTimes = loopTimes;
        _loopType = loopType;
    }

    private void Idle()
    {
        _transform.DOScale(_scaleMax, _duration).SetLoops(_loopTimes,_loopType);
    }

    public void Begin()
    {
        _transform.localScale = _scaleMin;
        _transform.DOScale(_default, _duration*0.6f).OnComplete(Idle);
    }

    public void Stop(Action callBack)
    {
        if (callBack != null)
            callBack();
        _transform.localScale = _default;
    }

    public void Hide()
    {
        _transform.localScale = _default;
    }

    public void Clear()
    {
        _transform = null;
    }
}