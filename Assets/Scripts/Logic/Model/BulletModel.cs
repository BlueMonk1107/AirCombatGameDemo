using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public interface IBullet
{
    BulletOwner Owner { get; }
    BulletOwner[] Tagets { get; }
    HashSet<string> GetTargetTags();
    int GetAttack();
}

public interface IBulletModel : IBullet
{
    GameAudio AudioName { get; }
    float FireTime { get; }
    Sprite Sprite();
    void GetBulletSpeed(Action<float> callBack);
    void Trajectory(Action<ITrajectory[]> callBack);
}


public class PlayerBulletModel : NormalSingleton<PlayerBulletModel>, IBulletModel
{
    private Sprite _sprite;

    private readonly HashSet<string> _targetTags = new HashSet<string>
    {
        Tags.ENEMY
    };

    private ITrajectory[] _trajectory;

    public HashSet<string> GetTargetTags()
    {
        return _targetTags;
    }

    public BulletOwner Owner => BulletOwner.PLAYER;

    public int GetAttack()
    {
        var key = KeysUtil.GetNewKey(PropertyItem.ItemKey.value, PlaneProperty.Property.attack.ToString());
        var attack = DataMgr.Single.Get<int>(key);
        return attack;
    }

    public BulletOwner[] Tagets { get; } =
    {
        BulletOwner.ENEMY
    };

    public GameAudio AudioName => GameAudio.Fire;

    public float FireTime
    {
        get
        {
            var key = KeysUtil.GetNewKey(PropertyItem.ItemKey.value, PlaneProperty.Property.fireRate.ToString());
            var rate = DataMgr.Single.Get<int>(key);
            return Const.FIRE_BASE_TIME / rate;
        }
    }

    public Sprite Sprite()
    {
        if (_sprite == null)
        {
            var path = Paths.PICTURE_PLAYER_BULLET_FOLDER + GameStateModel.Single.SelectedPlaneId;
            _sprite = LoadMgr.Single.Load<Sprite>(path);
        }

        return _sprite;
    }

    public void GetBulletSpeed(Action<float> callBack)
    {
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_Game_CONFIG);
        reader["bulletSpeed"].Get(callBack);
    }

    public void Trajectory(Action<ITrajectory[]> callBack)
    {
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_BULLET_CONFIG);
        var level = GameStateModel.Single.PlaneLevel;
        reader["Player"]["trajectory"][level.ToString()].Get<JsonData>(data =>
        {
            if (_trajectory == null)
            {
                if (!data.IsArray)
                {
                    Debug.LogError("当前数据不是一个数组，data:" + data);
                    return;
                }

                InitTrajectory(data);
            }

            callBack(_trajectory);
        });
    }

    private void InitTrajectory(JsonData data)
    {
        _trajectory = new ITrajectory[data.Count];
        for (var i = 0; i < data.Count; i++)
        {
            var trajectory = new StraightTrajectory();
            float value = 0;
            if (!float.TryParse(data[i].ToJson(), out value)) Debug.LogError("当前数据类型转换错误，数据为：" + data[i]);

            trajectory.Init(value);
            _trajectory[i] = trajectory;
        }
    }
}

public class PowerBulletModel : NormalSingleton<PowerBulletModel>, IBulletModel
{
    private Sprite _sprite;

    private ITrajectory[] _trajectories;

    public HashSet<string> GetTargetTags()
    {
        return PlayerBulletModel.Single.GetTargetTags();
    }

    public BulletOwner Owner => PlayerBulletModel.Single.Owner;

    public int GetAttack()
    {
        return PlayerBulletModel.Single.GetAttack();
    }

    public BulletOwner[] Tagets => PlayerBulletModel.Single.Tagets;

    public GameAudio AudioName => GameAudio.Power;

    public float FireTime => 0.3f;

    public Sprite Sprite()
    {
        if (_sprite == null)
        {
            var path = Paths.PICTURE_POWER;
            _sprite = LoadMgr.Single.Load<Sprite>(path);
        }

        return _sprite;
    }

    public void GetBulletSpeed(Action<float> callBack)
    {
        PlayerBulletModel.Single.GetBulletSpeed(callBack);
    }

    public void Trajectory(Action<ITrajectory[]> callBack)
    {
        if (_trajectories == null)
        {
            var tempList = new List<ITrajectory>();
            var angleOffset = 5;
            StraightTrajectory temp;
            for (var curAngle = 60; curAngle < 120; curAngle += angleOffset)
            {
                temp = new StraightTrajectory();
                temp.Init(curAngle);
                tempList.Add(temp);
            }

            _trajectories = tempList.ToArray();
        }

        callBack(_trajectories);
    }
}

public class EnemyBulluetModel : IBulletModel
{
    private EnemyData _data;
    public EnemyBulluetModel(EnemyData data)
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

    public GameAudio AudioName
    {
        get { return GameAudio.Null; }
    }
    public float FireTime
    {
        get { return (float)_data.fireRate; }
    }

    private Sprite _sprite;
    public Sprite Sprite()
    {
        if(_sprite == null)
            _sprite = LoadMgr.Single.Load<Sprite>(Paths.PICTURE_ENEMY_BULLET_FOLDER + "1");

        return _sprite;
    }

    public void GetBulletSpeed(Action<float> callBack)
    {
        if (callBack != null)
            callBack(3f);
    }

    public void Trajectory(Action<ITrajectory[]> callBack)
    {
        StraightTrajectoryData data = new StraightTrajectoryData();
        data.Angle = -90;
        ITrajectory[] temp = new ITrajectory[1];
        temp[0] = new StraightTrajectory();
        temp[0].Init(data);
        if (callBack != null)
            callBack(temp);
    }
}