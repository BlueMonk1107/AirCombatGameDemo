using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectView : IEffect
{
    void Init(Transform transform);
}

public abstract class EffectViewBase : IEffectView
{
    private IEffect[] _effects;
    private int _times;
    private Action _stopAction;
    protected Transform _transform;
    
    public virtual void Init(Transform transform)
    {
        _transform = transform;
        _effects = GetEffects(transform);
        _times = 0;
    }

    protected abstract IEffect[] GetEffects(Transform transform);
    
    public void Begin()
    {
        if(_effects == null)
            return;
        
        foreach (IEffect effect in _effects)
        {
            effect.Begin();
        }
    }

    public void Stop(Action callBack)
    {
        if(_effects == null)
            return;
        _stopAction = callBack;
        foreach (IEffect effect in _effects)
        {
            effect.Stop(End);
        }
    }

    public void Hide()
    {
        if(_effects == null)
            return;
        foreach (IEffect effect in _effects)
        {
            effect.Hide();
        }
    }

    private void End()
    {
        _times++;
        if (_times == _effects.Length)
        {
            StopCallBack();
        }
    }

    protected virtual void StopCallBack()
    {
        if (_stopAction != null)
            _stopAction();
    }

    public void Clear()
    {
        if(_effects == null)
            return;
        
        foreach (IEffect effect in _effects)
        {
            effect.Clear();
        }
    }

}
