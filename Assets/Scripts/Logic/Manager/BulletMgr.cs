using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMgr : MonoBehaviour,IBullet
{
    private IBulletModel _model;
    public HashSet<string> GetTargetTags()
    { 
        return _model.GetTargetTags();
    }

    public BulletOwner Owner
    {
        get { return _model.Owner; }
    }

    public int GetAttack()
    {
        return _model.GetAttack();
    }

    public BulletOwner[] Tagets
    {
        get { return _model.Tagets; }
    }
    
    public void Init(IBulletModel model)
    {
        _model = model;
        CoroutineMgr.Single.ExecuteOnce(Fire( model));
    }

    private IEnumerator Fire(IBulletModel model)
    {
        string audioName = model.AudioName.ToString();
        AudioMgr.Single.Play(audioName,true);
        DateTime start = DateTime.Now;
        while (true)
        {
            yield return new WaitForSeconds(model.FireTime);
            model.Trajectory((data) =>
            {
                foreach (var trajectory in data)
                {
                    Spawn(model, trajectory);
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
    private void Spawn(IBulletModel model,ITrajectory trajectory)
    {
        var bulletGo = PoolMgr.Single.Spawn(Paths.PREFAB_BULLET);
        var bullet = bulletGo.AddOrGet<Bullet>();
        bullet.Init(model,trajectory,transform.position);
    }
}