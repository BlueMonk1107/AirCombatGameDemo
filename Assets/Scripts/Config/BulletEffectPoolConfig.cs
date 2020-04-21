using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectPoolConfig : NormalSingleton<BulletEffectPoolConfig>,IInit {
	public ObjectPool<Boss1BulletEffect> Boss1Pool { get; private set; }

	public void Init()
	{
		Boss1Pool = new ObjectPool<Boss1BulletEffect>();
		Boss1Pool.Init(10,true);
	}
}
