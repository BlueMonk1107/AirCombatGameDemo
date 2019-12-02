using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpritesModel : NormalSingleton<PlaneSpritesModel>,IInit {

	public int Count
	{
		get
		{
			if (_planeSprites == null)
				return 0;
			return _planeSprites.Count;
		}
	}

	public Sprite this[int id,int level]
	{
		get { return GetPlaneSprite(id, level); }
	}

	private Dictionary<int, List<Sprite>> _planeSprites;

	public void Init()
	{
		LoadSprite();
	}
	
	private void LoadSprite()
	{
		_planeSprites = new Dictionary<int, List<Sprite>>();
		Sprite[] sprites = LoadMgr.Single.LoadAll<Sprite>(Paths.PICTURE_PLAYER_PICTURE_FOLDER);

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
	}

	private Sprite GetPlaneSprite(int id,int level)
	{
		if (!_planeSprites.ContainsKey(id) || level >= _planeSprites[id].Count)
		{
			Debug.LogError("当前id或等级错误");
			return null;
		}
		return _planeSprites[id][level];
	}
}
