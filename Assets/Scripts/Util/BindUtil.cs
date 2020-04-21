using System;
using System.Collections.Generic;
using UnityEngine;

public class BindUtil
{
    private static readonly Dictionary<string, List<Type>> _prefabAndScriptMap = new Dictionary<string, List<Type>>();
    private static readonly Dictionary<Type, int> _prioritysMap = new Dictionary<Type, int>();

    public static void Bind(BindPrefab data, Type type)
    {
        var path = data.Path;
        if (!_prefabAndScriptMap.ContainsKey(path)) _prefabAndScriptMap.Add(path, new List<Type>());

        if (!_prefabAndScriptMap[path].Contains(type))
        {
            _prefabAndScriptMap[path].Add(type);
            _prioritysMap.Add(type, data.Priority);
            _prefabAndScriptMap[path].Sort(new BindPriorityComparer());
        }
    }

    public static List<Type> GetType(string path)
    {
        if (_prefabAndScriptMap.ContainsKey(path))
        {
            return _prefabAndScriptMap[path];
        }

        Debug.LogError("当前数据中未包含路径：" + path);
        return null;
    }

    public class BindPriorityComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            if (x == null)
                return 1;
            if (y == null)
                return -1;
            return _prioritysMap[x] - _prioritysMap[y];
        }
    }
}