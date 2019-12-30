using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : PlaneView
{
    private float _startY = -2.0f;

    private HashSet<string> _target = new HashSet<string>()
    {
        Tags.ENEMY
    };

    public override void Init()
    {
        InitPos();
        InitSprite();
        InitComponent();
        AddListener();
    }

    private void AddListener()
    {
        MessageMgr.Single.AddListener(MsgEvent.EVENT_USE_SHIELD,UseShield);
    }
    
    private void OnDestroy()
    {
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_USE_SHIELD,UseShield);
    }

    private void UseShield(object[] paras)
    {
        var shield = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.EFFECT_SHIELD);
        shield.transform.position = transform.position;
        shield.AddComponent<ShieldView>();
        
    }

    protected override void InitComponent()
    {
        gameObject.AddComponent<CameraMove>();
        
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_INIT_PLANE_CONFIG);
        reader["planeSpeed"].Get<float>((speed) =>
        {
            var move = gameObject.AddComponent<MoveComponent>();
            move.Init(speed);
            gameObject.AddComponent<PlayerController>();
        });

        var bulletMgr = transform.Find("BulletRoot").AddComponent<BulletMgr>();
        bulletMgr.Init(PlayerBulletModel.Single);

        gameObject.AddComponent<ColliderComponent>();
        gameObject.AddComponent<CollideMsgComponent>();
    }

    private void InitPos()
    {
        var pos = transform.position;
        pos.y = _startY;
        transform.position = pos;
    }

    private void InitSprite()
    {
        var render = GetComponent<SpriteRenderer>();
        int id = GameStateModel.Single.SelectedPlaneId;
        int level = GameStateModel.Single.PlaneLevel;
        var sprite = PlaneSpritesModel.Single[id, level];
        render.sprite = sprite;
    }
}
