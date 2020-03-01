using System;
using UnityEngine;

public class PlaneEnemyView : PlaneView,IUpdate
{
    public RenderComponent Renderer { get;private set;}
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
        gameObject.tag = Tags.ENEMY;
        InitComponent(data,type,sprite,trajectoryData);
        InitPos(id);
    }

    private void InitComponent(EnemyData data,EnemyType type, Sprite sprite, ITrajectoryData trajectoryData)
    {
        //更新飞机图片
        Renderer = gameObject.AddOrGet<RenderComponent>();
        Renderer.Init();
        Renderer.SetSprite(sprite);
        //路径初始化
        _path = new PathMgr(); 
        _path.Init(transform,data,trajectoryData);
        
        gameObject.AddOrGet<CameraMove>().enabled = _path.NeedMoveWithCamera();
        gameObject.AddOrGet<AutoDespawnComponent>();
        gameObject.AddOrGet<EnemyTypeComponent>().Init(type);
        var lifeC = gameObject.AddOrGet<LifeComponent>();
        lifeC.Init(data.life);
        gameObject.AddOrGet<EnemyBehaviour>().Init(data);
        _moveComponent = gameObject.AddOrGet<MoveComponent>();
        _moveComponent.Init((float)data.speed);
        gameObject.AddOrGet<ColliderComponent>();
        gameObject.AddOrGet<PlaneCollideMsgComponent>();
        var bulletMgr = transform.Find("BulletRoot").AddOrGet<EnemyBulletMgr>();
        bulletMgr.Init(data);

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