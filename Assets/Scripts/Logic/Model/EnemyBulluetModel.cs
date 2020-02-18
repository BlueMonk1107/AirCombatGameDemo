using System;
using System.Collections.Generic;
using UnityEngine;

[Bullet(BulletType.Enemy_Normal_0)]
public class EnemyBulluetModel : IEnemyBulletModel
{
    private EnemyData _data;

    public void Init(EnemyData data)
    {
        _data = data;
    }

    public BulletOwner Owner
    {
        get { return BulletOwner.ENEMY; }
    }

    private BulletOwner[] _targets = new[]
    {
        BulletOwner.PLAYER
    };

    public BulletOwner[] Tagets
    {
        get { return _targets; }
    }

    private HashSet<string> _tags = new HashSet<string>()
    {
        Tags.PLAYER,
        Tags.SHIELD
    };

    public HashSet<string> GetTargetTags()
    {
        return _tags;
    }

    public int GetAttack()
    {
        return _data.attack;
    }

    public BulletType Type
    {
        get { return BulletType.Enemy_Normal_0; }
    } 

    public GameAudio AudioName
    {
        get { return GameAudio.Null; }
    }

    public float FireTime
    {
        get { return (float) _data.fireRate; }
    }

    private Sprite _sprite;

    public Sprite Sprite()
    {
        if (_sprite == null)
            _sprite = LoadMgr.Single.Load<Sprite>(Paths.PICTURE_ENEMY_BULLET_FOLDER + BulletName.Enemy_Normal_0);

        return _sprite;
    }

    public void GetBulletSpeed(Action<float> callBack)
    {
        float value = (float) GameDataMgr.Single.Get<AllBulletData>().Enemy_Normal_0.bulletSpeed;
        if (callBack != null)
            callBack(value);
    }

    public void Trajectory(Action<ITrajectory[]> callBack)
    {
        var data = GameDataMgr.Single.Get<AllBulletData>().Enemy_Normal_0;

        ITrajectory[] temp = BulletTrajectoryDataUtil.GetStraightArray(data.trajectory[0]);

        if (callBack != null)
            callBack(temp);
    }
}