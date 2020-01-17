using System.Collections.Generic;

public class PoolConfig
{
    public List<PoolData> Data = new List<PoolData>
    {
        new PoolData
        {
            Path = Paths.PREFAB_BULLET,
            PreloadCount = 15,
            AutoDestroy = false
        },
        new PoolData
        {
            Path = Paths.PREFAB_PLANE,
            PreloadCount = 15,
            AutoDestroy = false
        },
        new PoolData
        {
            Path = Paths.PREFAB_STAR,
            PreloadCount = 10,
            AutoDestroy = false
        },
        new PoolData
        {
            Path = Paths.EFFECT_FRAME_ANI,
            PreloadCount = 15,
            AutoDestroy = false
        }
    };
}

public class PoolData
{
    public string Path { get; set; }
    public int PreloadCount { get; set; }
    public bool AutoDestroy { get; set; }
}