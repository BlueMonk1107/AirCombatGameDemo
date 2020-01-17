using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IUpdate, IBullet
{
    private IBulletModel _model;
    private MoveComponent _move;

    private SpriteRenderer _renderer;
    private float _speed;
    private Vector3 _startPos;
    private ITrajectory _trajectory;

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
        gameObject.AddComponent<PlaneCollideMsgComponent>();
        gameObject.AddComponent<BulletBehaviour>();
    }

    public void Init(IBulletModel model, ITrajectory trajectory, Vector3 pos)
    {
        if (_renderer == null) 
            _renderer = GetComponent<SpriteRenderer>();
        _model = model;
        _renderer.sprite = model.Sprite();
        _startPos = pos;
        _trajectory = trajectory;
        SetPos(pos);
        
        _model.GetBulletSpeed(value =>
        {
            _speed = value;
            _move = gameObject.AddComponent<MoveComponent>();
            _move.Init(_speed);
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
        PoolMgr.Single.Despawn(gameObject);
    }
}