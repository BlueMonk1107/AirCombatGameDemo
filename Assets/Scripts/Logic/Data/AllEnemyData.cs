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
    public PathType trajectoryType;
    //-1代表当前是随机轨迹，大于0的值，代表轨迹id
    public int trajectoryID;
    public BulletType[] bulletType;
    public int starNum;
    public int score;
    /// <summary>
    /// 掉落道具的可能性，例如值为10，就代表百分之十的概率
    /// </summary>
    public int itemProbability;
    /// <summary>
    /// 掉落道具的范围，应该是长度为2的数组
    /// </summary>
    public ItemType[] itemRange;
    /// <summary>
    /// 掉落道具的数量，每个道具都在范围内随机
    /// 例如：数量是2，范围是[0,1]，那么可能会出一个0，一1.或者是两个1，或者是两个0
    /// </summary>
    public int itemCount;
}