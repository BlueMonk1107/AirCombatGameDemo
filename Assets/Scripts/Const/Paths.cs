using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths
{
    private const string PREFAB_FOLDER = "Prefab/";
    public const string START_VIEW = PREFAB_FOLDER + "StartView";
    public const string SELECTED_HERO_VIEW = PREFAB_FOLDER + "SelectedHeroView";
    public const string STRENGTHEN_VIEW = PREFAB_FOLDER + "StrengthenView";
    public const string LEVELS_VIEW = PREFAB_FOLDER + "LevelsView";
    public const string PROPERTY_ITEM = PREFAB_FOLDER + "PropertyItem";
    
    private const string PICTURE_FOLDER = "Picture/";
    public const string PLAYER_PICTURE_FOLDER = PICTURE_FOLDER+"Player/";

    private static readonly string CONFIG_FOLDER = Application.streamingAssetsPath + "/Config";
    public static readonly string INIT_PLANE_CONFIG = CONFIG_FOLDER + "/InitPlane.json";
}
