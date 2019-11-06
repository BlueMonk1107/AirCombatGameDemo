using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

public static class ExtendUtil 
{
    public static RectTransform Rect(this Transform trans)
    {
        return trans.GetComponent<RectTransform>();
    }

    public static void ButtonAction(this Transform trans, string path, Action action)
    {
        var target = trans.Find(path);
        if (target == null)
        {
            Debug.LogError("当前查找物体为空，路径为："+path);
        }
        else
        {
            var button = target.GetComponent<Button>();
            if (button == null)
            {
                Debug.LogError("当前物体上没有button组件，物体名称："+target.name);
            }
            else
            {
                button.onClick.AddListener(()=>action());
            }
        }
    }
}
