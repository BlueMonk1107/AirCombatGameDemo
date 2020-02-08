using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDestroyAniView : EffectView {
    protected override void InitComponent()
    {
        var ani = gameObject.AddOrGet<FrameAni>();
        var sprites = LoadMgr.Single.LoadAll<Sprite>(Paths.PICTURE_PLANE_DESTROY_FOLDER);
        ani.Init(sprites,AniEnd);
    }

    private void AniEnd()
    {
        if(PoolMgr.Single != null)
            PoolMgr.Single.Despawn(gameObject);
    }
}
