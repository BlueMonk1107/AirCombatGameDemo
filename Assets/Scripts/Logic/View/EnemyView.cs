using System;
using UnityEngine;

public class EnemyView : PlaneView,IUpdate
{
    public SpriteRenderer Renderer { get;private set;}
    private MoveComponent _moveComponent;
    private EnemyLifeView _lifeView;
    private PathMgr _path;


    protected override void OnEnable()
    {
        base.OnEnable();
        LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
    }

    private void OnDisable()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
    }

    public void Init(int id,EnemyType type,EnemyData data, Sprite sprite, ITrajectoryData trajectoryData)
    {
        if (Renderer == null)
        {
            Renderer = GetComponent<SpriteRenderer>();
        }

        gameObject.tag = Tags.ENEMY;
        Renderer.sprite = sprite;

        _path = new PathMgr(); 
        _path.Init(transform,data,trajectoryData);

        InitPos(id);
        InitComponent(data,type);
    }

    private void InitComponent(EnemyData data,EnemyType type)
    {
        gameObject.AddOrGet<CameraMove>().enabled = _path.NeedMoveWithCamera();
        gameObject.AddOrGet<AutoDespawnComponent>();
        gameObject.AddOrGet<EnemyTypeComponent>().Init(type);
        var lifeC = gameObject.AddOrGet<LifeComponent>();
        lifeC.Init(data.life);
        gameObject.AddOrGet<EnemyBehaviour>();
        _moveComponent = gameObject.AddOrGet<MoveComponent>();
        _moveComponent.Init((float)data.speed);
        gameObject.AddOrGet<ColliderComponent>();
        gameObject.AddOrGet<PlaneCollideMsgComponent>();
        var bulletMgr = transform.Find("BulletRoot").AddOrGet<BulletMgr>();
        bulletMgr.Init(new EnemyBulluetModel(data));

        if (_lifeView == null)
        {
            var lifeGo = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_ENEMY_LIFE, transform);
            _lifeView = lifeGo.AddComponent<EnemyLifeView>();
        }
        
        _lifeView.Init();
    }

    private void InitPos(int id)
    {
        SetPos(_path.GetInitPos(id));
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

   
    public void UpdateFun()
    {
        _moveComponent.Move(_path.GetDirection());
    }

   
}