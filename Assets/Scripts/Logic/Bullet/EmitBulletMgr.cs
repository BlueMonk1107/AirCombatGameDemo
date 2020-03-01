using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitBulletMgr : MonoBehaviour, IBullet
{
    private IBulletModel _model;
    private SpawnBulletPointMgr _pointMgr;

    public HashSet<string> GetTargetTags()
    {
        return _model.GetTargetTags();
    }

    public BulletOwner Owner => _model.Owner;

    public int GetAttack()
    {
        return _model.GetAttack();
    }

    public BulletOwner[] Tagets => _model.Tagets;

    private int _id;

    public void Init(BulletType type)
    {
        IBulletModel model = BulletUtil.GetBulletModel(type);
        Init(model);
    }

    public void Init(BulletType type,EnemyData data)
    {
        IEnemyBulletModel model = BulletUtil.GetEnemyBulletModel(type);
        if(model == null)
            return;
        model.Init(data);
        Init(model);
    }

    private void Init(IBulletModel model)
    {
        if(model == null)
            return;
        _model = model;
        if (GetAttack() > 0)
        {
            _id = CoroutineMgr.Single.ExecuteOnce(Fire(model));
        }

        InitComponent();

        InitPos(model);
    }

    private void InitComponent()
    {
        var component = gameObject.AddOrGet<BossBulletEventComponent>();
        component.Init(_model);

        _pointMgr = gameObject.AddOrGet<SpawnBulletPointMgr>();
        _pointMgr.Init();
    }

    private void InitPos(IBulletModel model)
    {
        model.Trajectory((trajectorys) =>
        {
            var bulletDirection = trajectorys[0].GetDirection();
            Vector2 selfDirection = transform.position - transform.parent.position;
            var angle = Vector2.Angle(selfDirection, bulletDirection);

            var pos = transform.localPosition;
            if (angle > 90)
            {
                pos.y = -pos.y;
            }

            transform.localPosition = pos;
        });
    }

    private IEnumerator Fire(IBulletModel model)
    {
        while (GameStateModel.Single.GameState == GameState.NULL)
        {
            yield return new WaitForSeconds(0.2f);
        }

        var audioName = model.AudioName.ToString();
        AudioMgr.Single.Play(audioName, true);
        var start = DateTime.Now;
        while (true)
        {
            yield return new WaitForSeconds(model.FireTime);
            model.Trajectory(data =>
            {
                for (int i = 0; i < data.Length; i++)
                {
                    Spawn(i,data.Length,model, data[i]);
                }
                    
            });

            //开火暂停时间
            if ((DateTime.Now - start).TotalSeconds >= Const.FIRE_DURATION)
            {
                AudioMgr.Single.PauseDelay(audioName);
                yield return new WaitForSeconds(Const.FIRE_CD_TIME);
                start = DateTime.Now;
                AudioMgr.Single.Replay(audioName);
            }
        }
    }

    private void OnDisable()
    {
        CoroutineMgr.Single.Stop(_id);
        AudioMgr.Single.Stop(_model.AudioName.ToString());
        _id = -1;
    }

    private void Spawn(int index,int count,IBulletModel model, ITrajectory trajectory)
    {
        if (PoolMgr.Single == null)
            return;
        var bulletGo = PoolMgr.Single.Spawn(Paths.PREFAB_BULLET);
        var bullet = bulletGo.AddOrGet<Bullet>();
        bullet.Init(model, trajectory, _pointMgr.GetPoints(count,transform.position)[index]);
    }
}