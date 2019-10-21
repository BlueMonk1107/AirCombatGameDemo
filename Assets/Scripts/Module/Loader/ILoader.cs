using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader
{
    GameObject LoadPrefab(string path, Transform parent = null);
    Sprite LoadSprite(string path);
    Sprite[] LoadAllSprites(string path);
}
