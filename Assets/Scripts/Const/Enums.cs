public enum Hero
{
    player_0,
    player_1,
    player_2
}

public enum SceneName
{
    NULL,
    Main,
    Game,
    COUNT
}

public enum BGAudio
{
    Game_BGM,
    Battle_BGM,
    Boss_BGM
}

public enum UIAduio
{
    UI_ClickButton,
    UI_Loading,
    UI_StartGame
}

public enum GameAudio
{
    Fire,
    Power,
    Effcet_Great,
    Effect_Gameover,
    Explode_Bullet,
    Explode_Plane,
    Get_Gold,
    Get_Item,
    Get_Shield,
    Lost_Item
}

public enum HandMode
{
    RIGHT,
    LEFT
}


public enum UILayer
{
    BASE_UI,
    MIDDLE_UI,
    TOP_UI,
    COUNT
}

public enum GameLayer
{
    BACKGROUND = 0,
    PLANE = -1,
    EFFECT = -2
}

public enum BulletOwner
{
    PLAYER,
    ENEMY
}

public enum EnemyType
{
    Normal,
    Elites,
    Boss,
    Item
}

/// <summary>
/// 轨迹类型
/// </summary>
public enum TrajectoryType
{
    /// <summary>
    /// 直线轨迹
    /// </summary>
    Straight,
    COUNT
}