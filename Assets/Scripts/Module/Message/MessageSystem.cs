using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessageSystem
{
    void AddListener(int key, IReceiver receiver);
    void RemoveListener(int key, IReceiver receiver);
    void DispatchMsg(int key, params object[] args);
}

public class MessageSystem :IMessageSystem {
    
    private Dictionary<int,HashSet<IReceiver>> _receivers = new Dictionary<int, HashSet<IReceiver>>();

    public void AddListener(int key,IReceiver receiver)
    {
        if (!_receivers.ContainsKey(key))
        {
            _receivers[key] = new HashSet<IReceiver>();
        }
        
        _receivers[key].Add(receiver);
    }

    public void RemoveListener(int key,IReceiver receiver)
    {
        if (_receivers.ContainsKey(key))
        {
            _receivers[key].Remove(receiver);
        }
    }

    public void DispatchMsg(int key,params object[] args)
    {
        foreach (IReceiver receiver in _receivers[key])
        {
            receiver.ReceiveMessage(args);
        }
    }
}
