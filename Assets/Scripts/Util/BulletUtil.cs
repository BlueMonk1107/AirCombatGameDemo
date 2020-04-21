using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUtil
{
    private static Dictionary<BulletType, IBulletModel> _types  = new Dictionary<BulletType, IBulletModel>();

    public static void Add(BulletAttribute attribute,Type type)
    {
        if (!_types.ContainsKey(attribute.Type))
        {
            _types.Add(attribute.Type,GetInstance(type));
        }
        else
        {
            Debug.LogError("当前子弹数据绑定类型存在重复，重复的类名称为："+_types[attribute.Type]+"和"+type);
        }
    }

    private static IBulletModel GetInstance(Type type)
    {
        object instance = Activator.CreateInstance(type);
        if (instance is IBulletModel)
        {
            return instance as IBulletModel;
        }
        else
        {
            Debug.LogError("当前绑定类未继承IBulletModel接口，类名为："+type);
            return null;
        }
    }

    public static IBulletModel GetBulletModel(BulletType type)
    {
        if (_types.ContainsKey(type))
        {
            return _types[type];
        }
        else
        {
            Debug.LogError("当前未绑定对应类型的数据，类型为："+type);
            return null;
        }
    }
    
    public static IEnemyBulletModel GetEnemyBulletModel(BulletType type)
    {
        if (_types.ContainsKey(type))
        {
            return (IEnemyBulletModel)_types[type];
        }
        else
        {
            Debug.LogError("当前未绑定对应类型的数据，类型为："+type);
            return null;
        }
    }
}
