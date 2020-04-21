using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniMgr : NormalSingleton<AniMgr> {

	public void PlaneDestroyAni(Vector3 pos)
	{
		var go = PoolMgr.Single.Spawn(Paths.EFFECT_FRAME_ANI);
		var view = go.AddOrGet<PlaneDestroyAniView>();
		view.Init();
		view.SetScale(Vector3.one*0.5f);
		view.SetPos(pos);
	}
	
	public void BulletDestroyAni(Vector3 pos)
	{
		var go = PoolMgr.Single.Spawn(Paths.EFFECT_FRAME_ANI);
		var view = go.AddOrGet<BulletDestroyAniView>();
		view.Init();
		view.SetScale(Vector3.one*0.1f);
		view.SetPos(pos);
	}
}
