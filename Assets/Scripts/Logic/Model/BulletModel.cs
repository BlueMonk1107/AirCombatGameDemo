using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine;


public interface IBullet
{
    HashSet<string> GetTargetTags();
    BulletOwner Owner { get; }
    int GetAttack();
    BulletOwner[] Tagets { get; }
}

public interface IBulletModel : IBullet
{
    GameAudio AudioName { get; }
    float FireTime { get; }
    Sprite Sprite();
    void GetBulletSpeed(Action<float> callBack);
    void Trajectory(Action<ITrajectory[]> callBack);
}


public class PlayerBulletModel :NormalSingleton<PlayerBulletModel>,IBulletModel
{
    public HashSet<string> GetTargetTags()
    {
        return _targetTags;
    }

    public BulletOwner Owner
    {
        get { return BulletOwner.PLAYER; }
    }
    
    private HashSet<string> _targetTags = new HashSet<string>()
    {
        Tags.ENEMY
    };

    public int GetAttack()
    {
        string key = KeysUtil.GetNewKey(PropertyItem.ItemKey.value, PlaneProperty.Property.attack.ToString());
        var attack = DataMgr.Single.Get<int>(key);
        return attack;
    }

    private BulletOwner[] _tagets = new[]
    {
        BulletOwner.ENEMY
    };
    
    public BulletOwner[] Tagets
    {
        get { return _tagets; }
    }

    public GameAudio AudioName
    {
        get
        {
           return GameAudio.Fire;
        }
    }

    public float FireTime
    {
        get
        {
            string key = KeysUtil.GetNewKey(PropertyItem.ItemKey.value, PlaneProperty.Property.fireRate.ToString());
            var rate = DataMgr.Single.Get<int>(key);
            return Const.FIRE_BASE_TIME / (float) rate;
        }
    }

    private Sprite _sprite;

    public Sprite Sprite()
    {
        if (_sprite == null)
        {
            string path = Paths.PICTURE_PLAYER_BULLET_FOLDER + GameStateModel.Single.SelectedPlaneId;
            _sprite = LoadMgr.Single.Load<Sprite>(path);
        }

        return _sprite;
    }

    public void GetBulletSpeed(Action<float> callBack)
    {
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_Game_CONFIG);
        reader["bulletSpeed"].Get<float>(callBack);
    }

    private ITrajectory[] _trajectory;

    public void Trajectory(Action<ITrajectory[]> callBack)
    {
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_BULLET_CONFIG);
        int level = GameStateModel.Single.PlaneLevel;
        reader["Player"]["trajectory"][level.ToString()].Get<JsonData>((data) =>
        {
            if (_trajectory == null)
            {
                if (!data.IsArray)
                {
                    Debug.LogError("当前数据不是一个数组，data:"+data);
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
        for (int i = 0; i < data.Count; i++)
        {
            var trajectory = new StraightTrajectory();
            float value = 0;
            if(!float.TryParse(data[i].ToJson(), out value))
            {
                Debug.LogError("当前数据类型转换错误，数据为："+data[i]);
            }
                    
            trajectory.Init(value);
            _trajectory[i] = trajectory;
        }
    }
}

public class PowerBulletModel :NormalSingleton<PowerBulletModel>, IBulletModel
{
    
    public HashSet<string> GetTargetTags()
    {
        return PlayerBulletModel.Single.GetTargetTags();
    }

    public BulletOwner Owner
    {
        get { return PlayerBulletModel.Single.Owner; }
    }

    public int GetAttack()
    {
        return PlayerBulletModel.Single.GetAttack();
    }

    public BulletOwner[] Tagets
    {
        get
        {
            return PlayerBulletModel.Single.Tagets;
        }
    }
    
    public GameAudio AudioName
    {
        get { return GameAudio.Power; }
    }

    public float FireTime
    {
        get { return 0.3f; }
    }
    
    private Sprite _sprite;

    public Sprite Sprite()
    {
        if (_sprite == null)
        {
            string path = Paths.PICTURE_POWER;
            _sprite = LoadMgr.Single.Load<Sprite>(path);
        }

        return _sprite;
    }

    public void GetBulletSpeed(Action<float> callBack)
    {
        PlayerBulletModel.Single.GetBulletSpeed(callBack);
    }

    private ITrajectory[] _trajectories;
    public void Trajectory(Action<ITrajectory[]> callBack)
    {
        if (_trajectories == null)
        {
            List<ITrajectory> tempList =  new List<ITrajectory>(); 
            int angleOffset = 5;
            StraightTrajectory temp;
            for (int curAngle = 60; curAngle < 120; curAngle+=angleOffset)
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