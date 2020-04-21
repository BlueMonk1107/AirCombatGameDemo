using System;
using System.Collections.Generic;

public interface IMessageSystem
{
    void AddListener(int key, Action<object[]> callback);
    void RemoveListener(int key, Action<object[]> callback);
    void DispatchMsg(int key, params object[] args);

    void AddListener(string key, Action<object[]> callback);
    void RemoveListener(string key, Action<object[]> callback);
    void DispatchMsg(string key, params object[] args);
}

public class MessageSystem : IMessageSystem
{
    private readonly Dictionary<int, ActionMgr<object[]>> _intReceivers = new Dictionary<int, ActionMgr<object[]>>();
    private readonly Dictionary<string, ActionMgr<object[]>> _stringReceivers = new Dictionary<string, ActionMgr<object[]>>();

    public void AddListener(int key, Action<object[]> callback)
    {
        if (!_intReceivers.ContainsKey(key)) 
            _intReceivers[key] = new ActionMgr<object[]>();

        _intReceivers[key].Add(callback);
    }

    public void RemoveListener(int key, Action<object[]> callback)
    {
        if (_intReceivers.ContainsKey(key))
            _intReceivers[key].Remove(callback);
    }

    public void DispatchMsg(int key, params object[] args)
    {
        if (_intReceivers.ContainsKey(key))
            _intReceivers[key].Execute(args);
    }

    public void AddListener(string key, Action<object[]> callback)
    {
        if (!_stringReceivers.ContainsKey(key)) 
            _stringReceivers[key] = new ActionMgr<object[]>();

        _stringReceivers[key].Add(callback);
    }

    public void RemoveListener(string key, Action<object[]> callback)
    {
        if (_stringReceivers.ContainsKey(key)) 
            _stringReceivers[key].Remove(callback);
    }

    public void DispatchMsg(string key, params object[] args)
    {
        if (_stringReceivers.ContainsKey(key))
            _stringReceivers[key].Execute(args);
    }
}