using System.Collections.Generic;
using UnityEngine;

public class UILayerMgr : NormalSingleton<UILayerMgr>, IInit
{
    private Dictionary<UILayer, RectTransform> _uiParentDic;

    public void Init()
    {
        _uiParentDic = new Dictionary<UILayer, RectTransform>();
        var canvas = Object.FindObjectOfType<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("当前场景中未发现Canvas");
            return;
        }

        for (var i = UILayer.BASE_UI; i < UILayer.COUNT; i++)
        {
            var child = new GameObject(i.ToString());
            child.transform.SetParent(canvas.transform);
            var rect = child.AddComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            _uiParentDic[i] = rect;
        }
    }

    public void SetParent(string path, Transform trans)
    {
        var parent = _uiParentDic[GetLayer(path)];
        trans.SetParent(parent);
    }

    public UILayer GetLayer(string path)
    {
        if (UILayerConfig.Layers.ContainsKey(path))
        {
            return UILayerConfig.Layers[path];
        }

        Debug.LogError("当前预制未初始化层级配置，预制路径：" + path);
        return UILayer.BASE_UI;
    }
}