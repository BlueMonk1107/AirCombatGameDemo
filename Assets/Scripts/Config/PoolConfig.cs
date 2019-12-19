

using System.Collections.Generic;

public class PoolConfig
{
        public List<PoolData> Data = new List<PoolData>()
        {
              
        };
}

public class PoolData
{
        public string Path { get; set; }
        public int PreloadCount { get; set; }
        public bool AutoDestroy { get; set; }
}