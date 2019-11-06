using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyItem : MonoBehaviour,IViewUpdate,IViewShow {

	public enum ItemKey
	{
		name,
		value,
		cost,
		grouth,
		maxVaue,
		COUNT
	}

	private static int _itemId = -1;
	private string _key;
	
	public void Init(string key)
	{
		_key = key;
		_itemId++;
		UpdatePos(_itemId);
		InitButtonAction();
	}

	private void InitButtonAction()
	{
		transform.ButtonAction("Add", AddAction);
	}

	private void AddAction()
	{
		string valueKey = GetNewKey(ItemKey.value);
		int value = GetValue(valueKey);
		string grouthKey = GetNewKey(ItemKey.grouth);
		int grouth = GetValue(grouthKey);
		value += grouth;
		
		DataMgr.Single.SetObject(valueKey,value);
	}

	private string GetNewKey(ItemKey key)
	{
		int planeId = GameStateModel.Single.SelectedPlaneId;
		return KeysUtil.GetPropertyKeys(planeId, _key + key);
	}

	private int GetValue(string key)
	{
		return DataMgr.Single.Get<int>(key);
	}
	

	private void UpdatePos(int itemId)
	{
		RectTransform rect = transform.Rect();
		float offset = rect.rect.height * itemId;
		rect.anchoredPosition -= offset * Vector2.up;
	}

	private void UpdatePlaneId(int planeId)
	{
		UpdateData(planeId);
		UpdateSlider();
	}

	private void UpdateData(int planeId)
	{
		for (ItemKey i = 0; i < ItemKey.grouth; i++)
		{
			Transform trans = transform.Find(ConvertName(i));
			if (trans != null)
			{
				string key = KeysUtil.GetPropertyKeys(planeId, _key + i);
				trans.GetComponent<Text>().text = DataMgr.Single.GetObject(key).ToString();
			}
			else
			{
				Debug.LogError("当前预制名称错误，正确名称："+ConvertName(i));
			}
		}
	}

	private void UpdateSlider()
	{
		Slider slider = transform.Find("Slider").GetComponent<Slider>();
		slider.minValue = 0;
		slider.maxValue = DataMgr.Single.Get<int>(GetNewKey(ItemKey.maxVaue));
		slider.value = DataMgr.Single.Get<int>(GetNewKey(ItemKey.value));
	}

	private string ConvertName(ItemKey key)
	{
		string first = key.ToString().Substring(0, 1).ToUpper();
		string others = key.ToString().Substring(1);
		return first + others;
	}

	public void UpdateFun()
	{
		UpdatePlaneId(GameStateModel.Single.SelectedPlaneId);
	}

	public void Show()
	{
		int id = DataMgr.Single.Get<int>(DataKeys.PLANE_ID);
		UpdatePlaneId(id);
	}
}
