using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBulletData
{
    public NormalBulletData Player;
    public NormalBulletData Enemy_Normal_0;
    public Boss0BulletData Enemy_Boss_0;
    public Boss1BulletData Enemy_Boss_1;
}

public class BulletData
{
    public double fireRate;
    public double bulletSpeed;
    public PathType trajectoryType;
    
}

public class NormalBulletData : BulletData
{
    public int[][] trajectory;
}

public class BossBulletData : BulletData
{
    public BulletEvent[] Events;
}


public class Boss0BulletData : BossBulletData
{
    public int[][] trajectory;
}

public class Boss1BulletData : BossBulletData
{
    public RotateData[] trajectory;
}

public class BulletEvent
{
    /// <summary>
    /// 触发当前事件的血量比例
    /// </summary>
    public double LifeRatio;
    public BulletEventType Type;
    public BulletEventData Data;
}

public class BulletEventData
{
    
}

public class ChangeSpeedData :BulletEventData
{
    public double bulletSpeed;
}

public class ChangeTrajectoryData:BulletEventData
{
    public PathType trajectoryType;
    public int[][] trajectory;
}