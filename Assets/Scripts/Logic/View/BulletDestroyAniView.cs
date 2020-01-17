using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyAniView : EffectView {
    protected override void InitComponent()
    {
        var ani = gameObject.AddOrGet<FrameAni>();
        var sprites = LoadMgr.Single.LoadAll<Sprite>(Paths.PICTURE_PLANE_DESTROY_FOLDER);
        sprites = GetSprites(sprites);
        ani.Init(sprites,AniEnd);
    }

    private Sprite[] GetSprites(Sprite[] sprites)
    {
        Sprite[] temp = new Sprite[sprites.Length/4];
        for (int i = 0; i < sprites.Length; i++)
        {
            if (i % 4 == 0)
            {
                temp[i / 4] = sprites[i];
            }
        }
        
        return temp;
    }

    private void AniEnd()
    {
        PoolMgr.Single.Despawn(gameObject);
    }
}
