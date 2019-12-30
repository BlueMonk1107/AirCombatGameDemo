using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour,IBehaviour {

	public void Injure(int value)
	{
		Destroy();
	}

	public void Dead()
	{
		Destroy();
	}

	private void Destroy()
	{
		//todo：子弹爆炸动画
		Destroy(gameObject);
	}
}
