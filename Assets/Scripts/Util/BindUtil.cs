using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindUtil  {
    
    private static Dictionary<string,Type> _prefabAndScriptMap = new Dictionary<string, Type>();

    public static void Bind(string path, Type type)
    {
        if (!_prefabAndScriptMap.ContainsKey(path))
        {
            _prefabAndScriptMap.Add(path,type);
        }
        else
        {
            Debug.LogError("already contain path:"+path);
        }
    }

    public static Type GetScriptType(string path)
    {
        if (_prefabAndScriptMap.ContainsKey(path))
        {
           return _prefabAndScriptMap[path];
        }
        else
        {
            Debug.LogError("not contain path:"+path);
            return null;
        }
    }
}
