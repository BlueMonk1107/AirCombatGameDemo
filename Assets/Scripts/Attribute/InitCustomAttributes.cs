using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class InitCustomAttributes {

    public void Init()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(BindPrefab));
        Type[] types = assembly.GetExportedTypes();

        foreach (Type type in types)
        {
            foreach (Attribute attribute in Attribute.GetCustomAttributes(type,true))
            {
                if (attribute is BindPrefab)
                {
                    BindPrefab data = attribute as BindPrefab;
                    BindUtil.Bind(data,type);
                }
            }
        }
    }
}
