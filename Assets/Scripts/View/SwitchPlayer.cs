using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : ViewBase {
	
	private Dictionary<int, List<Sprite>> _planeSprites;
	private Action<int> _updateData;
	private int _id;

	protected override void InitChild()
	{
		Util.Get("Left").AddListener(()=>Switch(ref _id,-1));
		Util.Get("Right").AddListener(()=>Switch(ref _id,1));
		LoadSprite();
	}
	
	private void LoadSprite()
	{
		_planeSprites = new Dictionary<int, List<Sprite>>();
		Sprite[] sprites = LoadMgr.Single.LoadAllSprites(Paths.PLAYER_PICTURE_FOLDER);
		foreach (Sprite sprite in sprites)
		{
			string[] idData = sprite.name.Split('_');
			int playerId = int.Parse(idData[0]);
			if (!_planeSprites.ContainsKey(playerId))
			{
				_planeSprites[playerId] = new List<Sprite>();
			}
			_planeSprites[playerId].Add(sprite);
		}

		foreach (KeyValuePair<int,List<Sprite>> pair in _planeSprites)
		{
			int count = pair.Value.Count;
			pair.Value.Capacity = count;
		}
	}

	public override void Show()
	{
		_id = DataMgr.Single.GetInt(DataKeys.PLANE_ID);
	}

	private void Switch(ref int id,int value)
	{
		
		int level = DataMgr.Single.GetInt(DataKeys.LEVEL);

		int max = _planeSprites.Count;
		id += value;
		id = id < 0 ? 0 : id >= max ? id = max - 1 : id;
		
		UpdateData(id);
		UpdateImage(id, level);
	}

	private void UpdateData(int id)
	{
		if (_updateData != null)
			_updateData(id);
	}

	private void UpdateImage(int id,int level)
	{
		Util.Get("Icon").Img.sprite = _planeSprites[id][level];
	}

	public void AddUpdateDataListener(Action<int> updateData)
	{
		_updateData += updateData;
	}
}
