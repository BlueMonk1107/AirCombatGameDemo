using System;

public class AllEnemyData
{
    public EnemyData[] Boss;
    public EnemyData[] Elites;
    public EnemyData[] Normal;

    public EnemyData[] GetData(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Normal:
                return Normal;
            case EnemyType.Elites:
                return Elites;
            case EnemyType.Boss:
                return Boss;
            default:
                return null;
        }
    }
}

public class EnemyData
{
    public int id;
    public int attack;
    public double attackTime;
    public double fireRate;
    public int life;
    public double speed;
    public TrajectoryType trajectoryType;
    //-1代表当前是随机轨迹，大于0的值，代表轨迹id
    public int trajectoryID;
}