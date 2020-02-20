public class EnemyCreaterConfigData
{
    public LevelData[] LevelDatas;
}

public class LevelData
{
    public PlaneCreaterData[] PlaneCreaterDatas;
    public MissileCreaterData[] MissileCreaterDatas;
    public int EnemyNumMax;
    public int EnemyNumMin;
    /// <summary>
    /// 每死亡多少个普通怪生成一波精英怪
    /// </summary>
    public int NormalDeadNumForSpawnElites;
}

public interface ICreaterData
{
    
}

public class PlaneCreaterData : ICreaterData
{
    public int IdMax;
    public int IdMin;
    /// <summary>
    /// 每个飞机队列的飞机数量
    /// </summary>
    public int QueuePlaneNum;
    /// <summary>
    /// 生成队列的数量
    /// </summary>
    public int QueueNum;
    public EnemyType Type;
    public double X;
}

public class MissileCreaterData : ICreaterData
{
    /// <summary>
    /// 当前导弹的生成批次
    /// </summary>
    public int Batch;
    public double X;
    public int NumOfWarning;
    public double EachWarningTime;
    public int SpwanCount;
    public double Speed;
}