using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiUtil : MonoBehaviour
{
    private Dictionary<string, UiUtilData> _datas;

    public void Init()
    {
        _datas = new Dictionary<string, UiUtilData>();
        var rect = transform.GetComponent<RectTransform>();
        foreach (RectTransform rectTransform in rect) _datas.Add(rectTransform.name, new UiUtilData(rectTransform));
    }

    public UiUtilData Get(string name)
    {
        if (_datas.ContainsKey(name)) return _datas[name];

        var temp = transform.Find(name);
        if (temp == null)
        {
            Debug.LogError("无法按照路径查找到物体，路径为：" + name);
            return null;
        }

        _datas.Add(name, new UiUtilData(temp.GetComponent<RectTransform>()));
        return _datas[name];
    }
}

public class UiUtilData
{
    public UiUtilData(RectTransform rectTrans)
    {
        RectTrans = rectTrans;
        Go = rectTrans.gameObject;
        Img = RectTrans.GetComponent<Image>();
        Text = rectTrans.GetComponent<Text>();
    }

    public GameObject Go { get; }
    public RectTransform RectTrans { get; }
    public Image Img { get; }
    public Text Text { get; }

    public void SetSprite(Sprite sprite)
    {
        if (Img != null)
            Img.sprite = sprite;
        else
            Debug.LogError("当前物体上没有image组件，物体名称为" + Go.name);
    }

    public void SetText(int content)
    {
        SetText(content.ToString());
    }

    public void SetText(float content)
    {
        SetText(content.ToString());
    }

    public void SetText(string content)
    {
        if (Text != null)
            Text.text = content;
        else
            Debug.LogError("当前物体上没有Text组件，物体名称为" + Go.name);
    }

    public T Get<T>() where T : Component
    {
        if (Go != null) return Go.GetComponent<T>();

        Debug.LogError("当前gameobject为空");
        return null;
    }

    public T Add<T>() where T : Component
    {
        if (Go != null)
        {
            return Go.AddComponent<T>();
        }

        Debug.LogError("当前gameobject为空");
        return null;
    }
}