using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReader
{
    IReader this[string key] { get; }
    IReader this[int key] { get; }
    void Get<T>(Action<T> callBack);
    void SetData(object data);
    ICollection<string> Keys();
}
