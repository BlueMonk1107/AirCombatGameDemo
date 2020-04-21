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
    BulletType Type { get; }
    GameAudio AudioName { get; }
    float FireTime { get; }
    Sprite Sprite();
    void GetBulletSpeed(Action<float> callBack);
    void Trajectory(Action<ITrajectory[]> callBack);
}

public interface IEnemyBulletModel : IBulletModel
{
    void Init(EnemyData data);
}

public interface IEnemyBossBulletModel : IEnemyBulletModel
{
    void UpdateEvent(float lifeRatio);
}
