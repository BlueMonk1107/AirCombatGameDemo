using System;

public class TaskQueueMgr<T> : NormalSingleton<TaskQueueMgr<T>>
{
    private TaskQueue<T> _queue;

    public TaskQueueMgr()
    {
        _queue = new TaskQueue<T>();
    }
    
    public void AddQueue(Func<IReader> getReader)
    {
        _queue.Add((self,id)=>getReader().Get<T>((data)=>self.AddValue(id,data)));
    }

    public void Execute(Action<T[]> complete)
    {
        _queue.Execute(complete);
    }
}

public class TaskQueueMgr : NormalSingleton<TaskQueueMgr>
{
    private TaskQueue _queue;
    
    public TaskQueueMgr()
    {
        _queue = new TaskQueue();
    }
    
    public void AddQueue<T>(Func<IReader> getReader)
    {
        _queue.Add((self,id)=>getReader().Get<T>((data)=>self.AddValue(id,data)));
    }

    public void Execute(Action<object[]> complete)
    {
        _queue.Execute(complete);
    }
}