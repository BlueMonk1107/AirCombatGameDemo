using System;
using System.Reflection;

public class InitCustomAttributes : IInit
{
    public void Init()
    {
        InitData<BindPrefab>(BindUtil.Bind);
        InitData<BulletAttribute>(BulletUtil.Add);
    }

    private void InitData<T>(Action<T,Type> callBack) where T: class
    {
        var assembly = Assembly.GetAssembly(typeof(T));
        var types = assembly.GetExportedTypes();

        foreach (var type in types)
        {
            foreach (var attribute in Attribute.GetCustomAttributes(type, true))
            {
                if (attribute is T)
                {
                    T data = attribute as T;
                    callBack(data, type);
                }
            } 
        }
    }
}