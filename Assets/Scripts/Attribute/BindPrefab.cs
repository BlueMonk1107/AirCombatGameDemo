using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class BindPrefab : Attribute {

    public string Path { get; private set; }
    public int Priority { get; private set; }

    public BindPrefab(string path,int priority = 100)
    {
        Path = path;
        Priority = priority;
    }
}
