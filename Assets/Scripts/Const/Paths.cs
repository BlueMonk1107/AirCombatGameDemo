﻿using UnityEngine;

public class Paths
{
    //预制体路径
    private const string PREFAB_FOLDER = "Prefab/";
    private const string UI_FOLDER = PREFAB_FOLDER + "UI/";
    public const string PREFAB_START_VIEW = UI_FOLDER + "StartView";
    public const string PREFAB_SELECTED_HERO_VIEW = UI_FOLDER + "SelectedHeroView";
    public const string PREFAB_STRENGTHEN_VIEW = UI_FOLDER + "StrengthenView";
    public const string PREFAB_LEVELS_VIEW = UI_FOLDER + "LevelsView";
    public const string PREFAB_LEVEL_ITEM = UI_FOLDER + "LevelItem";
    public const string PREFAB_PROPERTY_ITEM = UI_FOLDER + "PropertyItem";
    public const string PREFAB_DIALOG = UI_FOLDER + "Dialog";
    public const string PREFAB_LOADING_VIEW = UI_FOLDER + "LoadingView";
    public const string PREFAB_GAME_UI_VIEW = UI_FOLDER + "GameUI";
    public const string PREFAB_LIFE_ITEM_VIEW = UI_FOLDER + "LifeItem";
    public const string PREFAB_SETTING_VIEW = UI_FOLDER + "SettingView";
    public const string PREFAB_GAME_RESULT_VIEW = UI_FOLDER + "GameResultView";

    public const string PREFAB_ITEM_EFFECT_VIEW = UI_FOLDER + "ItemEffect";
    public const string PREFAB_WARNING = UI_FOLDER + "Warning";
    
    private const string GUIDE_FOLDER = PREFAB_FOLDER + "Guide/";
    public const string PREFAB_HAND_GUIDE = GUIDE_FOLDER + "Hand";
    public const string PREFAB_WELCOME_GUIDE = GUIDE_FOLDER + "WelcomeView";
    public const string PREFAB_HIGH_LIGHT_GUIDE = GUIDE_FOLDER + "HighLightModule";
    public const string PREFAB_EXPAIN_DIALOG_GUIDE = GUIDE_FOLDER + "ExplainDialog";
    //game预制
    private const string Game_FOLDER = PREFAB_FOLDER + "Game/";
    public const string PREFAB_GAME_ROOT = Game_FOLDER + "GameRoot";
    public const string PREFAB_MAP_MGR = Game_FOLDER + "MapMgr";
    public const string PREFAB_PLANE = Game_FOLDER + "Plane";
    public const string PREFAB_BULLET = Game_FOLDER + "Bullet";
    public const string PREFAB_ENEMY_LIFE = Game_FOLDER + "EnemyLife";

    private const string EFFECT_FOLDER = Game_FOLDER + "Effect/";
    public const string EFFECT_SHIELD = EFFECT_FOLDER + "Shield";
    public const string EFFECT_POWER = EFFECT_FOLDER + "Power";
    public const string EFFECT_FRAME_ANI = EFFECT_FOLDER + "FrameAni";
    public const string EFFECT_LEVEL_UP = EFFECT_FOLDER + "LevelUpEffect";

    public const string ENEMY_FOLDER = Game_FOLDER + "Enemy/";
    public const string PREFAB_ENEMY_MISSILE = ENEMY_FOLDER + "Missile";
    
    private const string PREFAB_ITEM_FOLDER = Game_FOLDER + "Item/";
    public const string PREFAB_ITEM_ITEM = PREFAB_ITEM_FOLDER + "Item";
    public const string PREFAB_ITEM_LIGHT = PREFAB_ITEM_FOLDER + "Light";
  

    //图片路径
    private const string PICTURE_FOLDER = "Picture/";
    public const string PICTURE_PLAYER_PICTURE_FOLDER = PICTURE_FOLDER + "Player/";
    public const string PICTURE_MAP_FOLDER = PICTURE_FOLDER + "Map/";
    public const string PICTURE_EFFECT_FOLDER = PICTURE_FOLDER + "Effect/";
    public const string PICTURE_PLANE_DESTROY_FOLDER = PICTURE_EFFECT_FOLDER + "PlaneDestroy/";

    public const string PICTURE_BULLET_FOLDER = PICTURE_FOLDER + "Buttle/";
    public const string PICTURE_PLAYER_BULLET_FOLDER = PICTURE_BULLET_FOLDER + "Player/";
    public const string PICTURE_ENEMY_BULLET_FOLDER = PICTURE_BULLET_FOLDER + "Enemy/";
    public const string PICTURE_BULLET_EFFECT = PICTURE_BULLET_FOLDER + "Effect/";
    public const string PICTURE_BULLET_POWER = PICTURE_BULLET_EFFECT + "Power";

    public const string PICTURE_ENEMY_FOLDER = PICTURE_FOLDER + "Enemy/";
    
    public const string PICTURE_ITEM_FOLDER = PICTURE_FOLDER + "Item/";
    public const string PICTURE_ADD_BULLET = PICTURE_ITEM_FOLDER + "AddBullet";
    public const string PICTURE_ADD_EXP = PICTURE_ITEM_FOLDER + "AddExp";
    public const string PICTURE_SHIELD = PICTURE_ITEM_FOLDER + "Shield";
    public const string PICTURE_POWER = PICTURE_ITEM_FOLDER + "Power";
    public const string PICTURE_STAR = PICTURE_ITEM_FOLDER + "Star";

    //配置路径
    public static readonly string CONFIG_FOLDER = Application.streamingAssetsPath + "/Config";
    public static readonly string CONFIG_INIT_PLANE_CONFIG = CONFIG_FOLDER + "/InitPlane.json";
    public static readonly string CONFIG_LEVEL_CONFIG = CONFIG_FOLDER + "/LevelConfig.json";
    public static readonly string CONFIG_AUDIO_VOLUME_CONFIG = CONFIG_FOLDER + "/AudioVolume.json";
    public static readonly string CONFIG_Game_CONFIG = CONFIG_FOLDER + "/GameConfig.json";
    public static readonly string CONFIG_BULLET_CONFIG = CONFIG_FOLDER + "/BulletConfig.json";

    public static readonly string CONFIG_LEVEL_ENEMY_DATA = CONFIG_FOLDER + "/LevelEnemyDataConfig.json";
    public static readonly string CONFIG_ENEMY = CONFIG_FOLDER + "/EnemyConfig.json";
    public static readonly string CONFIG_ENEMY_TRAJECTORY = CONFIG_FOLDER + "/EnemyTrajectoryConfig.json";

    //音频路径
    public static readonly string AUDIO_FOLDER = "Audio";
    public static readonly string AUDIO_UI_FOLDER = AUDIO_FOLDER + "/UI/";
    public static readonly string AUDIO_Player_FOLDER = AUDIO_FOLDER + "/Player/";
    public static readonly string AUDIO_GAME_BG = AUDIO_FOLDER + "/Game_BG";
    public static readonly string AUDIO_CLICK_BUTTON = AUDIO_Player_FOLDER + "/UI_ClickButton";
    public static readonly string AUDIO_LOADING = AUDIO_Player_FOLDER + "/UI_Loading";
    public static readonly string AUDIO_START_GAME = AUDIO_Player_FOLDER + "/UI_StartGame";
}