using System;
using System.Reflection;

public class InitCustomAttributes : IInit
{
    public void Init()
    {
        var assembly = Assembly.GetAssembly(typeof(BindPrefab));
        var types = assembly.GetExportedTypes();

        foreach (var type in types)
        foreach (var attribute in Attribute.GetCustomAttributes(type, true))
            if (attribute is BindPrefab)
            {
                var data = attribute as BindPrefab;
                BindUtil.Bind(data, type);
            }
    }
}