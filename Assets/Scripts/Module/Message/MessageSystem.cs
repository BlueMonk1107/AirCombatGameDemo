using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessageSystem
{
    void AddListener(int key,Action<object[]> callback);
    void RemoveListener(int key,Action<object[]> callback);
    void DispatchMsg(int key, params object[] args);
    
    void AddListener(string key,Action<object[]> callback);
    void RemoveListener(string key,Action<object[]> callback);
    void DispatchMsg(string key, params object[] args);
}

public class MessageSystem :IMessageSystem {
    
    private Dictionary<int,Action<object[]>> _intReceivers = new Dictionary<int,Action<object[]>>();
    private Dictionary<string,Action<object[]>> _stringReceivers = new Dictionary<string,Action<object[]>>();

    public void AddListener(int key,Action<object[]> callback)
    {
        if (!_intReceivers.ContainsKey(key))
        {
            _intReceivers[key] = callback;
        }

        _intReceivers[key] += callback;
    }

    public void RemoveListener(int key,Action<object[]> callback)
    {
        if (_intReceivers.ContainsKey(key))
        {
            _intReceivers[key] -= callback;
        }
    }

    public void DispatchMsg(int key,params object[] args)
    {
        if(_intReceivers.ContainsKey(key))
            _intReceivers[key](args);
    }

    public void AddListener(string key,Action<object[]> callback)
    {
        if (!_stringReceivers.ContainsKey(key))
        {
            _stringReceivers[key] = callback;
        }
        
        _stringReceivers[key] += callback;
    }

    public void RemoveListener(string key,Action<object[]> callback)
    {
        if (_stringReceivers.ContainsKey(key))
        {
            _stringReceivers[key] -= callback;
        }
    }

    public void DispatchMsg(string key, params object[] args)
    {
        if(_stringReceivers.ContainsKey(key))
            _stringReceivers[key](args);
    }
}
