using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletEventComponent  : ComponentBase
{
    private IEnemyBossBulletModel _bulletModel;
    public void Init(IBulletModel model)
    {
        if (model is IEnemyBossBulletModel)
        {
            _bulletModel = model as IEnemyBossBulletModel;
            MsgMgr.AddListener(MsgEvent.EVENT_HP,ReceiveLifeMsg);
        }
    }

    private void ReceiveLifeMsg(object[] paras)
    {
        int life = GameUtil.GetInt(paras[0]);
        int lifeMax = GameUtil.GetInt(paras[1]);
        float ratio = 1;
        if (lifeMax != 0)
        {
            ratio = life / (float) lifeMax;
        }
        
        _bulletModel.UpdateEvent(ratio);
        if (life == 0)
        {
            End();
        }
    }

    private void End()
    {
        MsgMgr.RemoveListener(MsgEvent.EVENT_HP,ReceiveLifeMsg);
    }
}
