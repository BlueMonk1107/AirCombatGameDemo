using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWarningEffectView : EffectViewBase
{
    private int _numOfWarning;
    private float _eachWarningTime;
    private Action _endAction;

    public void Init(Transform transform,int numOfWarning,float eachWarningTime,Action endAction)
    {
        _numOfWarning = numOfWarning;
        _eachWarningTime = eachWarningTime;
        _endAction = endAction;
        base.Init(transform);
    }
    
    protected override IEffect[] GetEffects(Transform transform)
    {
        if (_eachWarningTime == 0 || _numOfWarning == 0)
        {
            Debug.LogError("当前组件未初始化");
            return null;
        }
        
        IEffect[] effects = new IEffect[1];
        ShowAndHideEffect effect = new ShowAndHideEffect();
        effect.Init(transform,_eachWarningTime,_numOfWarning,_endAction);
        
        effects[0] = effect;

        return effects;
    }
}
