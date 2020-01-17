using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarView : PlaneView
{
    private StarAni _ani;

    protected override void InitComponent()
    {
        gameObject.AddOrGet<AutoDespawnComponent>();
        gameObject.AddOrGet<ItemCollideMsgComponent>().Init(CollideEvent);
        _ani = gameObject.AddOrGet<StarAni>();
    }

    private void CollideEvent()
    {
        AudioMgr.Single.PlayOnce(GameAudio.Get_Gold.ToString());
        _ani.Hide(() =>
        {
            GameModel.Single.Stars++;
            PoolMgr.Single.Despawn(gameObject);
            MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_SCORE);
        });
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }
}