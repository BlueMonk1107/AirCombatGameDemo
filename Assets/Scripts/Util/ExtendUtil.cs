using System;
using UnityEngine;
using UnityEngine.UI;

public static class ExtendUtil
{
    public static T AddOrGet<T>(this Transform trans) where T : Component
    {
        var t = trans.GetComponent<T>();
        if (t == null) t = trans.AddComponent<T>();

        return t;
    }

    public static T AddOrGet<T>(this GameObject go) where T : Component
    {
        var t = go.GetComponent<T>();
        if (t == null) 
            t = go.AddComponent<T>();

        return t;
    }

    public static RectTransform Rect(this Transform trans)
    {
        return trans.GetComponent<RectTransform>();
    }

    public static T AddComponent<T>(this Transform trans) where T : Component
    {
        return trans.gameObject.AddComponent<T>();
    }

    public static T AddComponent<T>(this Transform trans, string path) where T : Component
    {
        return trans.Find(path).gameObject.AddComponent<T>();
    }

    public static void ButtonAction(this Transform trans, string path, Action action, bool useDefaultAudio = true)
    {
        var target = trans.Find(path);
        if (target == null)
        {
            Debug.LogError("当前查找物体为空，路径为：" + path);
        }
        else
        {
            var button = target.GetComponent<Button>();
            if (button == null)
            {
                Debug.LogError("当前物体上没有button组件，物体名称：" + target.name);
            }
            else
            {
                button.onClick.AddListener(() => action());
                if (useDefaultAudio)
                    button.onClick.AddListener(AddButtonAudio);
            }
        }
    }

    private static void AddButtonAudio()
    {
        AudioMgr.Single.PlayOnce(UIAduio.UI_ClickButton.ToString());
    }

    public static Vector2 Reversal(this Vector2 direction)
    {
        return new Vector2(-direction.x,-direction.y);
    }
}