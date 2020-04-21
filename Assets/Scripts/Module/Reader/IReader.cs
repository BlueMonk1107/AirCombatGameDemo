using System;
using System.Collections.Generic;

public interface IReader
{
    IReader this[string key] { get; }
    IReader this[int key] { get; }
    void Count(Action<int> callBack);
    void Get<T>(Action<T> callBack);
    void SetData(object data);
    ICollection<string> Keys();
}