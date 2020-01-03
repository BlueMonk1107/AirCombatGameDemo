using System.Collections.Generic;
using UnityEngine;

public class PlaneSpritesModel : NormalSingleton<PlaneSpritesModel>, IInit
{
    private Dictionary<int, List<Sprite>> _planeSprites;

    public int Count
    {
        get
        {
            if (_planeSprites == null)
                return 0;
            return _planeSprites.Count;
        }
    }

    public Sprite this[int id, int level] => GetPlaneSprite(id, level);

    public void Init()
    {
        LoadSprite();
    }

    private void LoadSprite()
    {
        _planeSprites = new Dictionary<int, List<Sprite>>();
        var sprites = LoadMgr.Single.LoadAll<Sprite>(Paths.PICTURE_PLAYER_PICTURE_FOLDER);

        foreach (var sprite in sprites)
        {
            var idData = sprite.name.Split('_');
            var playerId = int.Parse(idData[0]);
            if (!_planeSprites.ContainsKey(playerId)) _planeSprites[playerId] = new List<Sprite>();

            _planeSprites[playerId].Add(sprite);
        }
    }

    private Sprite GetPlaneSprite(int id, int level)
    {
        if (!_planeSprites.ContainsKey(id) || level >= _planeSprites[id].Count)
        {
            Debug.LogError("当前id或等级错误");
            return null;
        }

        return _planeSprites[id][level];
    }
}