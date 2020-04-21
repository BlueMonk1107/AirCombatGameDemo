using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeItem : OtherViewBase
{
	public override GameLayer Layer
	{
		get
		{
			return GameLayer.EFFECT;
		}
	}

	public override void Init()
	{
		base.Init();
		MsgMgr.AddListener(MsgEvent.EVENT_HP,UpdateLife);
		gameObject.SetActive(true);
	}

	private void UpdateLife(object[] paras)
	{
		int life = 0;
		int lifeMax = 0;
		if (paras.Length == 2)
		{
			life = (int) paras[0];
			lifeMax = (int) paras[1];
		}
		else
		{
			Debug.LogError("更新生命消息部分，参数不能为空");
			return;
		}

		if (life <= GetLifeMin(lifeMax))
		{
			gameObject.SetActive(false);
		}
	}

	private int GetLifeMin(int lifeMax)
	{
		var eachLife =  lifeMax / transform.parent.childCount;
		return transform.GetSiblingIndex() * eachLife;
	}
}
