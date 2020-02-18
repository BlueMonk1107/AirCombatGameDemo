using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarView : PlaneView
{
    private StarEffectView _effectView;

    protected override void InitComponent()
    {
        gameObject.AddOrGet<AutoDespawnComponent>();
        gameObject.AddOrGet<ItemCollideMsgComponent>().Init(CollideEvent);
        if(_effectView == null)
            _effectView = new StarEffectView();
        
        _effectView.Init(transform);
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        _effectView.Begin();
    }

    protected void OnDisable()
    {
        _effectView.Hide();
    }

    private void CollideEvent()
    {
        AudioMgr.Single.PlayOnce(GameAudio.Get_Gold.ToString());
        _effectView.Stop(() =>
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