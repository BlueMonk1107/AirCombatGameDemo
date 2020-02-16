using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyBossBulluetModelBase : IEnemyBossBulletModel
{
    private EnemyData _data;
    private BossBulletData _bossData;
    private Dictionary<float, KeyValuePair<BulletEventType, BulletEventData>> _eventsData;

    public void Init(EnemyData data)
    {
        _data = data;
        _getBulletSpeedAction = GetDefaultBulletSpeed;
        _getTrajectoryAction = GetDefaultTrajectory;
        _bossData = GetBulletData();
        InitEventsData(_bossData);
    }

    protected abstract BossBulletData GetBulletData();
    protected abstract BulletName GetBulletName();

    private void InitEventsData(BossBulletData bossData)
    {
        _eventsData = new Dictionary<float, KeyValuePair<BulletEventType, BulletEventData>>();
        foreach (BulletEvent bulletEvent in bossData.Events)
        {
            _eventsData[(float)bulletEvent.LifeRatio] = 
                new KeyValuePair<BulletEventType, BulletEventData>(bulletEvent.Type,bulletEvent.Data); 
        }
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
            _sprite = LoadMgr.Single.Load<Sprite>(Paths.PICTURE_ENEMY_BULLET_FOLDER + GetBulletName());

        return _sprite;
    }


    private Action<Action<float>> _getBulletSpeedAction;
    private Action<Action<ITrajectory[]>> _getTrajectoryAction;

    public void GetBulletSpeed(Action<float> callBack)
    {
        _getBulletSpeedAction(callBack);
    }

    public void Trajectory(Action<ITrajectory[]> callBack)
    {
        _getTrajectoryAction(callBack);
    }

    private void GetDefaultBulletSpeed(Action<float> callBack)
    {
        float value = (float) _bossData.bulletSpeed;
        if (callBack != null)
            callBack(value);
    }

    private void GetDefaultTrajectory(Action<ITrajectory[]> callBack)
    {
        ITrajectory[] temp = BulletTrajectoryDataUtil.GetTrajectory(_bossData.trajectoryType, 0, _bossData);

        if (callBack != null)
            callBack(temp);
    }

    public void UpdateEvent(float lifeRatio)
    {
        foreach (var pair in _eventsData)
        {
            if (lifeRatio < pair.Key)
            {
                switch (pair.Value.Key)
                {
                    case BulletEventType.ChangeSpeed:
                        _getBulletSpeedAction = (callBack) =>
                        {
                            ChangeSpeedData speedData = pair.Value.Value as ChangeSpeedData;
                            if (callBack != null)
                                callBack((float)speedData.bulletSpeed);
                        };
                        break;
                    case BulletEventType.ChangeTrajectory:
                        _getTrajectoryAction = (callBack) =>
                        {
                            ChangeTrajectoryData data = pair.Value.Value as ChangeTrajectoryData;
                            ITrajectory[] temp = BulletTrajectoryDataUtil.GetTrajectory(data.trajectoryType, 0, data);
                            if (callBack != null)
                                callBack(temp);
                        };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}

[Bullet(BulletType.Enemy_Boss_0)]
public class EnemyBoss0BulluetModel : EnemyBossBulluetModelBase
{
    protected override BossBulletData GetBulletData()
    {
        return GameDataMgr.Single.Get<AllBulletData>().Enemy_Boss_0;
    }

    protected override BulletName GetBulletName()
    {
        return BulletName.Enemy_Boss_0;
    }
}

[Bullet(BulletType.Enemy_Boss_1)]
public class EnemyBoss1BulluetModel : EnemyBossBulluetModelBase
{
    protected override BossBulletData GetBulletData()
    {
        return GameDataMgr.Single.Get<AllBulletData>().Enemy_Boss_1;
    }

    protected override BulletName GetBulletName()
    {
        return BulletName.Enemy_Boss_1;
    }
}