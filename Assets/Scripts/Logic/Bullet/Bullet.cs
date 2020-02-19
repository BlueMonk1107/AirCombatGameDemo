using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IUpdate, IBullet
{
    private IBulletModel _model;
    private MoveComponent _move;
    private SpriteRenderer _renderer;
    private ITrajectory _trajectory;
    private BulletEffectMgr _effectMgr;

    public BulletOwner Owner => _model.Owner;

    public int GetAttack()
    {
        return _model.GetAttack();
    }

    public BulletOwner[] Tagets => _model.Tagets;

    public HashSet<string> GetTargetTags()
    {
        return _model.GetTargetTags();
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

    public void UpdateFun()
    {
        if (_move != null)
        {
            _move.Move(_trajectory.GetDirection());
        }
    }

    // Use this for initialization
    private void Start()
    {
        gameObject.AddComponent<BulletCollideMsgComponent>();
        gameObject.AddComponent<BulletBehaviour>();
    }

    public void Init(IBulletModel model, ITrajectory trajectory, Vector3 pos)
    {
        if (_renderer == null) 
            _renderer = GetComponent<SpriteRenderer>();
        _model = model;
        _renderer.sprite = model.Sprite();
        _trajectory = trajectory;
        SetPos(pos);
        
        if(_effectMgr == null)
            _effectMgr = gameObject.AddComponent<BulletEffectMgr>();
        _effectMgr.Init(model.Type);
        
        _model.GetBulletSpeed(value =>
        {
            _move = gameObject.AddOrGet<MoveComponent>();
            _move.Init(value);
        });
    }

    private void OnEnable()
    {
        LifeCycleMgr.Single.Add(LifeName.UPDATE, this);
    }

    private void OnDisable()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE, this);
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnBecameInvisible()
    {
        if(PoolMgr.Single == null)
            return;
        
        PoolMgr.Single.Despawn(gameObject);
    }
}