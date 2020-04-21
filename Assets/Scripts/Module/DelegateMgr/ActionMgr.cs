using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMgr {
    
}

public class ActionMgr<T>
{
    private HashSet<Action<T>> _actions;
    private Action<T> _action;

    public ActionMgr()
    {
        _actions = new HashSet<Action<T>>();
        _action = null;
    }

    public void Add(Action<T> action)
    {
        if (_actions.Add(action))
        {
            _action += action;
        }
    }

    public void Remove(Action<T> action)
    {
        if (_actions.Remove(action))
        {
            _action -= action;
        }
    }

    public void Execute(T t)
    {
        if (_action != null)
            _action(t);
    }

    public bool Contains(Action<T> action)
    {
        return _actions.Contains(action);
    }
}
