using System;
using UnityEngine;

public class EnemyView : PlaneView,IUpdate
{
    public SpriteRenderer Renderer { get;private set;}

    private ITrajectory _trajectory;
    private MoveComponent _moveComponent;


    protected override void OnEnable()
    {
        base.OnEnable();
        LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
    }

    private void OnDisable()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
    }

    public void Init(int id,EnemyData data, Sprite sprite, ITrajectoryData trajectoryData)
    {
        if (Renderer == null)
        {
            Renderer = GetComponent<SpriteRenderer>();
        }

        gameObject.tag = Tags.ENEMY;
        Renderer.sprite = sprite;

        _trajectory = TrajectoryFactory.GetTrajectory(data.trajectoryType);
        _trajectory.Init(trajectoryData);

        InitPos(id);
        InitComponent((float)data.speed,data.life);
    }

    private void InitComponent(float speed,int life)
    {
        gameObject.AddOrGet<LifeComponent>().Init(life);
        gameObject.AddOrGet<EnemyBehaviour>();
        _moveComponent = gameObject.AddOrGet<MoveComponent>();
        _moveComponent.Init(speed);
        gameObject.AddOrGet<ColliderComponent>();
        gameObject.AddOrGet<PlaneCollideMsgComponent>();
        var bulletMgr = transform.Find("BulletRoot").AddOrGet<BulletMgr>();
        bulletMgr.Init(EnemyNoBulluetModel.Single);
    }

    private void InitPos(int id)
    {
        float height = Renderer.bounds.max.y - Renderer.bounds.min.y;
        Vector3 startPos = transform.position;
        float yOffset = height * 0.5f + height * id;
        float y = startPos.y + yOffset;
        float x = _trajectory.GetX(y, startPos);
        SetPos(new Vector3(x,y,transform.position.z));
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

    private int _times;
    private int _updateTimes;
    public void UpdateFun()
    {
        _moveComponent.Move(_trajectory.GetDirection().Reversal());

        LimitUpdate();
    }

    private void LimitUpdate()
    {
        if (_times < _updateTimes)
        {
            _times++;
            return;
        }
        _times = 0;
        
        if (JudgeBeyondBorder())
        {
            PoolMgr.Single.Despawn(gameObject);
        }
    }

    private bool JudgeBeyondBorder()
    {
        if (Renderer.bounds.max.y < GameUtil.GetCameraMin().y)
        {
            return true;
        }
        else
        {
            if (Renderer.bounds.min.x < GameUtil.GetCameraMin().x)
            {
                return true;
            } 
            else if (Renderer.bounds.max.x > GameUtil.GetCameraMax().x)
            {
                return true;
            }
        }

        return false;
    }
}