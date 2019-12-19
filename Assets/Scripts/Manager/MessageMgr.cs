using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageMgr : NormalSingleton<MessageMgr>,IMessageSystem {

    private MessageSystem _msgSystem;

    public MessageMgr()
    {
        _msgSystem = new MessageSystem();
    }
    
    public void AddListener(int key, IReceiver receiver)
    {
        _msgSystem.AddListener(key,receiver);
    }

    public void RemoveListener(int key, IReceiver receiver)
    {
        _msgSystem.RemoveListener(key,receiver);
    }

    public void DispatchMsg(int key, params object[] args)
    {
        _msgSystem.DispatchMsg(key,args);
    }
}
