using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILayerMgr : NormalSingleton<UILayerMgr> ,IInit
{

    private Dictionary<UILayer, RectTransform> _uiParentDic;
    
    public void Init()
    {
        _uiParentDic = new Dictionary<UILayer, RectTransform>();
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("当前场景中未发现Canvas");
            return;
        }
        
        for (UILayer i = UILayer.BASE_UI; i < UILayer.COUNT; i++)
        {
            GameObject child = new GameObject(i.ToString());
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
        else
        {
            Debug.LogError("当前预制未初始化层级配置，预制路径："+path);
            return UILayer.BASE_UI;
        }
    }
}
