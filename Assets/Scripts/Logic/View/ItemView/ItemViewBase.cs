using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemViewBase : PlaneView {
    private IEffectView _effectView;
    private SpriteRenderer _renderer;

    protected override void InitComponent()
    {
        if (_renderer == null)
            _renderer = GetComponent<SpriteRenderer>();
        gameObject.AddOrGet<AutoDespawnComponent>();
        gameObject.AddOrGet<ItemCollideMsgComponent>().Init(CollideEvent);
        if (_effectView == null)
            _effectView = GetEffectView();
        
        _effectView.Init(transform);
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        _effectView.Begin();
        InitSprite();
    }

    private void InitSprite()
    {
        _renderer.sprite = LoadMgr.Single.Load<Sprite>(SpritePath());
    }

    protected abstract IEffectView GetEffectView();
    protected abstract GameAudio GetGameAudio();

    protected virtual void ItemLogic()
    {
        Destroy(gameObject);
    }
    protected abstract string SpritePath();

    protected virtual void OnDisable()
    {
        _effectView.Hide();
    }

    private void CollideEvent()
    {
        AudioMgr.Single.PlayOnce(GetGameAudio().ToString());
        _effectView.Stop(ItemLogic);
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
