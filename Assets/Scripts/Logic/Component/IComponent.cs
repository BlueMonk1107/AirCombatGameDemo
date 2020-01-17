using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComponent  {
    
}

public abstract class ComponentBase : MonoBehaviour, IComponent
{
    private SubMsgMgr _msgMgr;

    protected SubMsgMgr MsgMgr
    {
        get
        {
            if (_msgMgr == null)
                _msgMgr = GameUtil.GetSubMsgMgr(transform);

            return _msgMgr;
        }
    }
}