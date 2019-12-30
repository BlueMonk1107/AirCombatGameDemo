using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour,IBehaviour {

	public void Injure(int value)
	{
		int life = GameModel.Single.Life - value;
		if (life <= 0)
		{
			life = 0;
			Dead();
		}
		else
		{
			GameModel.Single.Life = life;
		}
		
		MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_HP);
	}

	public void Dead()
	{
		//todo:播放爆炸特效
	}
}
