using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiUtil : MonoBehaviour
{

	private Dictionary<string, UiUtilData> _datas;

	public void Init()
	{
		_datas = new Dictionary<string, UiUtilData>();
		RectTransform rect = transform.GetComponent<RectTransform>();
		foreach (RectTransform rectTransform in rect)
		{
			_datas.Add(rectTransform.name,new UiUtilData(rectTransform));
		}
	}

	public UiUtilData Get(string name)
	{
		if (_datas.ContainsKey(name))
		{
			return _datas[name];
		}
		else
		{
			Transform temp = transform.Find(name);
			if (temp == null)
			{
				Debug.LogError("无法按照路径查找到物体，路径为："+name);
				return null;
			}
			else
			{
				_datas.Add(name,new UiUtilData(temp.GetComponent<RectTransform>()));
				return _datas[name];
			}
		}
	}
}

public class UiUtilData
{
	public GameObject Go { get; private set; }
	public RectTransform RectTrans { get; private set; }
	public Button Btn { get; private set; }
	public Image Img { get; private set; }
	public Text Text { get; private set; }

	public UiUtilData(RectTransform rectTrans)
	{
		RectTrans = rectTrans;
		Go = rectTrans.gameObject;
		Btn = rectTrans.GetComponent<Button>();
		Img = RectTrans.GetComponent<Image>();
		Text = rectTrans.GetComponent<Text>();
	}

	public void AddListener(Action action)
	{
		if (Btn != null)
		{
			Btn.onClick.AddListener(()=>action());
		}
		else
		{
			Debug.LogError("当前物体上没有button组件，物体名称为"+Go.name);
		}
	}

	public void SetSprite(Sprite sprite)
	{
		if (Img != null)
		{
			Img.sprite = sprite;
		}
		else
		{
			Debug.LogError("当前物体上没有image组件，物体名称为"+Go.name);
		}
	}
}
