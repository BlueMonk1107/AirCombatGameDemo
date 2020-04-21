using System;

[AttributeUsage(AttributeTargets.Class)]
public class BindPrefab : Attribute
{
    public BindPrefab(string path, int priority = 100)
    {
        Path = path;
        Priority = priority;
    }

    public string Path { get; }
    public int Priority { get; }
}