using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AddBulletEffectView : EffectViewBase {
    protected override IEffect[] GetEffects(Transform transform)
    {
        IEffect[] effects = new IEffect[1];

        SlowSpeedEffect ySlow = new SlowSpeedEffect();
        ySlow.Init(transform,Vector2.up, 0,Random.Range(2,4),-5);
        effects[0] = ySlow;

        return effects;
    }
}
