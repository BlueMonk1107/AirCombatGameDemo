using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffMgr : SpecialStateMgrBase {
    
    protected override HashSet<BuffType> GetCanBuffs()
    {
        HashSet<BuffType> types = new HashSet<BuffType>();
        types.Add(BuffType.LEVEL_UP);
        return types;
    }

    protected override HashSet<DebuffType> GetCanDebuffs()
    {
        return null;
    }
}
