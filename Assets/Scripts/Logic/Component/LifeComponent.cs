using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : ComponentBase
{
    private int _life;

    public int Life
    {
        get { return _life; }
        set
        {
            _life = value;
            MsgMgr.DispatchMsg(MsgEvent.EVENT_HP,_life,LifeMax);
        }
    }

    public int LifeMax { get; private set; }

    public void Init(int life)
    {
        Life = life;
        LifeMax = life;
    }
}
