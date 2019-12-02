using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskQueue
{
    private Queue<Action<TaskQueue,int>> _tasks;
    private int _id;
    private object[] _values;
    private int _addValueTimes;
    private Action<object[]> _onComplete;

    public TaskQueue()
    {
        _tasks = new Queue<Action<TaskQueue,int>>();
        ResetData();
    }

    private void ResetData()
    {
        _id = -1;
        _addValueTimes = 0;
    }

    public void Add(Action<TaskQueue,int> task)
    {
        _tasks.Enqueue(task);
    }

    public void Execute(Action<object[]> complete)
    {
        _onComplete = complete;
        _values = new object[_tasks.Count];
        
        while (_tasks.Count >0)
        {
            _id++;
            var task = _tasks.Dequeue();
            if (task != null)
                task(this,_id);
        }

        ResetData();
    }

    public void AddValue(int id,object value)
    {
        _addValueTimes++;
        _values[id] = value;
        JudgeComplete();
    }

    private void JudgeComplete()
    {
        if (_addValueTimes == _values.Length)
        {
            if (_onComplete != null)
                _onComplete(_values);
        }
        else if (_addValueTimes > _values.Length)
        {
            Debug.LogError("AddValue执行次数过多");
        }
    }
}

public class TaskQueue<T>
{
    private Queue<Action<TaskQueue<T>,int>> _tasks;
    private int _id;
    private T[] _values;
    private int _addValueTimes;
    private Action<T[]> _onComplete;

    public TaskQueue()
    {
        _tasks = new Queue<Action<TaskQueue<T>,int>>();
        _id = -1;
        _addValueTimes = 0;
    }

    public void Add(Action<TaskQueue<T>,int> task)
    {
        _tasks.Enqueue(task);
    }

    public void Execute(Action<T[]> complete)
    {
        _onComplete = complete;
        _values = new T[_tasks.Count];
        
        while (_tasks.Count >0)
        {
            _id++;
            var task = _tasks.Dequeue();
            if (task != null)
                task(this,_id);
        }
    }

    public void AddValue(int id,T value)
    {
        _addValueTimes++;
        _values[id] = value;
        JudgeComplete();
    }

    private void JudgeComplete()
    {
        if (_addValueTimes == _values.Length)
        {
            if (_onComplete != null)
                _onComplete(_values);
        }
        else if (_addValueTimes > _values.Length)
        {
            Debug.LogError("AddValue执行次数过多");
        }
    }
}