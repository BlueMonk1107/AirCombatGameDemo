using System.Collections.Generic;
using UnityEngine;

public class PlayerView : PlaneView
{
    private readonly float _startY = -2.0f;

    private HashSet<string> _target = new HashSet<string>
    {
        Tags.ENEMY
    };

    public override void Init()
    {
        gameObject.tag = Tags.PLAYER;
        InitPos();
        InitSprite();
        InitComponent();
        AddListener();
    }

    private void AddListener()
    {
        MessageMgr.Single.AddListener(MsgEvent.EVENT_USE_SHIELD, UseShield);
        MessageMgr.Single.AddListener(MsgEvent.EVENT_USE_BOMB, UseBomb);
        MessageMgr.Single.AddListener(MsgEvent.EVENT_GAME_UPDATE_LEVEL,LevelUp);
        MessageMgr.Single.AddListener(MsgEvent.EVENT_GAME_EXP_LEVEL_UP,LevelUp);
    }

    private void OnDestroy()
    {
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_USE_SHIELD, UseShield);
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_USE_BOMB, UseBomb);
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_GAME_UPDATE_LEVEL,LevelUp);
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_GAME_EXP_LEVEL_UP,LevelUp);
    }

    private void UseShield(object[] paras)
    {
        var shield = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.EFFECT_SHIELD);
        shield.transform.position = transform.position;
        shield.transform.SetParent(transform);
        shield.AddComponent<ShieldView>();
        
    }

    private void UseBomb(object[] paras)
    {
        var shield = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.EFFECT_POWER);
        shield.transform.position = transform.position;
        shield.transform.SetParent(transform);
        shield.AddComponent<PowerView>();
    }

    private void LevelUp(object[] paras)
    {
        if (InitSprite())
        {
            var effect = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.EFFECT_LEVEL_UP);
            effect.transform.position = transform.position;
            effect.transform.SetParent(transform);
            effect.AddComponent<AutoDestroyComponent>().Init(0.4f);
        }
    }

    protected override void InitComponent()
    {
        gameObject.AddComponent<PlayerEnterAni>();
        gameObject.AddComponent<CameraMove>();
        gameObject.AddComponent<PlayerBehaviour>();

        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_INIT_PLANE_CONFIG);
        reader["planeSpeed"].Get<float>(speed =>
        {
            var move = gameObject.AddComponent<MoveComponent>();
            move.Init(speed);
            gameObject.AddComponent<PlayerController>();
        });

        var bulletMgr = transform.Find("BulletRoot").AddComponent<EmitBulletMgr>();
        bulletMgr.Init(BulletType.Player);

        gameObject.AddComponent<ColliderComponent>();
        gameObject.AddComponent<PlaneCollideMsgComponent>();
        gameObject.AddComponent<PlayerBuffMgr>();
    }

    private void InitPos()
    {
        var pos = transform.position;
        pos.y = _startY;
        transform.position = pos;
    }

    private bool InitSprite()
    {
        var id = GameStateModel.Single.SelectedPlaneId;
        var level = GameModel.Single.TempLevel;
        var sprite = PlaneSpritesModel.Single[id, level];
        var render = gameObject.AddOrGet<RenderComponent>();
        render.Init();
        return render.SetSprite(sprite);
    }
}