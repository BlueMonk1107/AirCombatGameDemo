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
		foreach (RectTransform trans in rect)
		{
			_datas.Add(trans.name,new UiUtilData(trans));
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
			Transform trans = transform.Find(name);
			if (trans == null)
			{
				Debug.LogError("can not find Gameobject name is "+name);
				return new UiUtilData();
			}
			else
			{
				_datas.Add(name,new UiUtilData(trans.GetComponent<RectTransform>()));
				return _datas[name];
			}
		}
	}
}

public struct UiUtilData
{
	public GameObject Go { get; private set; }
	public RectTransform RectTrans { get; set; }
	public Button Btn { get; private set; }
	public Image Img { get; private set; }
	public Text Text { get; private set; }

	public UiUtilData(RectTransform rect) : this()
	{
		RectTrans = rect;
		Go = rect.gameObject;
		Btn = rect.GetComponent<Button>();
		Img = rect.GetComponent<Image>();
		Text = rect.GetComponent<Text>();
	}

	public void AddListener(Action callBack)
	{
		if (Btn != null)
		{
			Btn.onClick.AddListener(()=>callBack());
		}
		else
		{
			Debug.LogError("can not find Button,gameobject name is "+Go.name);
		}
	}
}