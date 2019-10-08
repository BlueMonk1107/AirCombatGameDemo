using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSingleton<T> where T : class,new()
{
    private static T _single;

    public static T Single
    {
        get
        {
            if (_single == null)
            {
                T t = new T();
                if (t is MonoBehaviour)
                {
                    Debug.LogError("Mono类请使用MonoSingleton");
                    return null;
                }

                _single = t;
               
            }
            return _single;
        }
    }
}
