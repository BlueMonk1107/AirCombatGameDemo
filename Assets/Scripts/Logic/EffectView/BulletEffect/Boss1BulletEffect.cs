using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BulletEffect : EffectViewBase {
    
    protected override IEffect[] GetEffects(Transform transform)
    {
        IEffect[] effects = new IEffect[1];
        var rotate = new RotateEffect();
        rotate.Init(transform,-60);
        effects[0] = rotate;

        return effects;
    }
}
