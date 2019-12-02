using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths {
	//预制体路径
	private const string PREFAB_FOLDER = "Prefab/";
	public const string PREFAB_START_VIEW = PREFAB_FOLDER + "StartView";
	public const string PREFAB_SELECTED_HERO_VIEW = PREFAB_FOLDER + "SelectedHeroView";
	public const string PREFAB_STRENGTHEN_VIEW = PREFAB_FOLDER + "StrengthenView";
	public const string PREFAB_LEVELS_VIEW = PREFAB_FOLDER + "LevelsView";
	public const string PREFAB_LEVEL_ITEM = PREFAB_FOLDER + "LevelItem";
	public const string PREFAB_PROPERTY_ITEM = PREFAB_FOLDER + "PropertyItem";
	public const string PREFAB_DIALOG = PREFAB_FOLDER + "Dialog";
	public const string PREFAB_LOADING_VIEW = PREFAB_FOLDER + "LoadingView";
	//图片路径
	private const string PICTURE_FOLDER = "Picture/";
	public const string PICTURE_PLAYER_PICTURE_FOLDER = PICTURE_FOLDER+"Player/";
	//配置路径
	public static readonly string CONFIG_FOLDER = Application.streamingAssetsPath + "/Config";
	public static readonly string CONFIG_INIT_PLANE_CONFIG = CONFIG_FOLDER + "/InitPlane.json";
	public static readonly string CONFIG_LEVEL_CONFIG = CONFIG_FOLDER + "/LevelConfig.json";
	public static readonly string CONFIG_AUDIO_VOLUME_CONFIG = CONFIG_FOLDER + "/AudioVolume.json";
	//音频路径
	public static readonly string AUDIO_FOLDER = "Audio";
	public static readonly string AUDIO_UI_FOLDER = AUDIO_FOLDER+"/UI/";
	public static readonly string AUDIO_Player_FOLDER = AUDIO_FOLDER+"/Player/";
	public static readonly string AUDIO_GAME_BG = AUDIO_FOLDER+"/Game_BG";
	public static readonly string AUDIO_CLICK_BUTTON = AUDIO_Player_FOLDER+"/UI_ClickButton";
	public static readonly string AUDIO_LOADING = AUDIO_Player_FOLDER+"/UI_Loading";
	public static readonly string AUDIO_START_GAME = AUDIO_Player_FOLDER+"/UI_StartGame";
}
