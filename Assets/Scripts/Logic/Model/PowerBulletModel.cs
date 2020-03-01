using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Bullet(BulletType.Power)]
public class PowerBulletModel : NormalSingleton<PowerBulletModel>, IBulletModel
{
	private Sprite _sprite;

	private ITrajectory[] _trajectories;

	public HashSet<string> GetTargetTags()
	{
		return BulletUtil.GetBulletModel(BulletType.Player).GetTargetTags();
	}

	public BulletOwner Owner => BulletUtil.GetBulletModel(BulletType.Player).Owner;

	public int GetAttack()
	{
		return BulletUtil.GetBulletModel(BulletType.Player).GetAttack();
	}

	public BulletOwner[] Tagets => BulletUtil.GetBulletModel(BulletType.Player).Tagets;

	public BulletType Type
	{
		get { return BulletType.Player; }
	}
	public GameAudio AudioName => GameAudio.Power;

	public float FireTime => 0.3f;

	public Sprite Sprite()
	{
		if (_sprite == null)
		{
			var path = Paths.PICTURE_BULLET_POWER;
			_sprite = LoadMgr.Single.Load<Sprite>(path);
		}

		return _sprite;
	}

	public void GetBulletSpeed(Action<float> callBack)
	{
		BulletUtil.GetBulletModel(BulletType.Player).GetBulletSpeed(callBack);
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