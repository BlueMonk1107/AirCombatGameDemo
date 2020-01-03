public class EnemyCreaterConfigData
{
    public LevelData[] LevelDatas;
}

public class LevelData
{
    public CreaterData[] CreaterDatas;
    public int EnemyNumMax;
    public int EnemyNumMin;
}

public class CreaterData
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