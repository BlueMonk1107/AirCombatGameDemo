using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StarEffectView : EffectViewBase {

    protected override IEffect[] GetEffects(Transform transform)
    {
        IEffect[] effects = new IEffect[3];
        
        ScaleEffect scaleEffect = new ScaleEffect();
        Vector3 max = transform.localScale * 1.2f;
        scaleEffect.Init(transform,max,Vector3.zero, 0.5f,-1,LoopType.Yoyo);
        effects[0] = scaleEffect;
        
        SlowSpeedEffect ySlow = new SlowSpeedEffect();
        ySlow.Init(transform,Vector2.up, Random.Range(1,4),Random.Range(2,4),-10);
        effects[1] = ySlow;
        
        SlowSpeedEffect xSlow = new SlowSpeedEffect();
        float startSpeed = Random.Range(-0.5f, 0.5f);
        float slowSpeed = 0;
        if (startSpeed > 0)
        {
            slowSpeed = Random.Range(0.3f, 1);
        }
        else
        {
            slowSpeed = Random.Range(-0.3f, -1);
        }
        xSlow.Init(transform,Vector2.right,startSpeed ,slowSpeed,0);
        effects[2] = xSlow;
        
        return effects;
    }

    protected override void StopCallBack()
    {
        FlyIntoUI flyIntoUi = new FlyIntoUI();
        flyIntoUi.Init(_transform,0.8f,Paths.PREFAB_GAME_UI_VIEW,"Star/Value",()=>base.StopCallBack());
        flyIntoUi.Begin();
    }
}
