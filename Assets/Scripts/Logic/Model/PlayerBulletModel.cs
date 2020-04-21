using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Bullet(BulletType.Player)]
public class PlayerBulletModel : IBulletModel
{
	private Sprite _sprite;

	private readonly HashSet<string> _targetTags = new HashSet<string>
	{
		Tags.ENEMY
	};

	private ITrajectory[] _trajectorys;

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

	public BulletType Type
	{
		get { return BulletType.Player; }
	}
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
		float value = (float) GameDataMgr.Single.Get<AllBulletData>().Player.bulletSpeed;
		if (callBack != null)
			callBack(value);
	}

	public void Trajectory(Action<ITrajectory[]> callBack)
	{
		var playerData = GameDataMgr.Single.Get<AllBulletData>().Player;
		int level = GameModel.Single.TempLevel;
		if(_trajectorys == null || _trajectorys.Length != playerData.trajectory[level].Length)
			_trajectorys = BulletTrajectoryDataUtil.GetStraightArray(playerData.trajectory[level]);
		if (callBack != null)
			callBack(_trajectorys);
	}
}
