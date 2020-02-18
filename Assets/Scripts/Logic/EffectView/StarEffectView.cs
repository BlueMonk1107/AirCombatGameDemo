using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StarEffectView : EffectViewBase {

    protected override IEffect[] GetEffects(Transform transform)
    {
        IEffect[] effects = new IEffect[2];
        
        ScaleEffect scaleEffect = new ScaleEffect();
        Vector3 max = transform.localScale * 1.2f;
        scaleEffect.Init(transform,max,Vector3.zero, 0.5f,-1,LoopType.Yoyo);
        effects[0] = scaleEffect;
        
        FallDownEffect fallDownEffect = new FallDownEffect();
        fallDownEffect.Init(transform,3,3,-10);
        effects[1] = fallDownEffect;
        
        return effects;
    }

    protected override void StopCallBack()
    {
        FlyIntoUI flyIntoUi = new FlyIntoUI();
        flyIntoUi.Init(_transform,0.8f,Paths.PREFAB_GAME_UI_VIEW,"Star/Value",()=>base.StopCallBack());
        flyIntoUi.Begin();
    }
}
