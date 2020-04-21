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
    Null,
    Fire,
    Power,
    Effcet_Great,
    Effect_Gameover,
    Effect_Boss_Warning,
    Effect_Missile_Warning,
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
/// 路径类型
/// </summary>
public enum PathType
{
    /// <summary>
    /// 直线路径
    /// </summary>
    Straight,
    /// <summary>
    /// W型路径
    /// </summary>
    W,
    /// <summary>
    /// 只有入场动画，入场后呆在上方不动
    /// </summary>
    StayOnTop,
    /// <summary>
    /// 椭圆路径
    /// </summary>
    Ellipse,
    /// <summary>
    /// 原地旋转
    /// </summary>
    Rotate,
    COUNT
}

public enum BulletType
{
    Player,
    Enemy_Normal_0,
    Enemy_Boss_0,
    Enemy_Boss_1,
    Power
}

public enum PathState
{
    ENTER,
    FORWARD_MOVING,
    BACK_MOVING
}

public enum GameState
{
    NULL,
    START,
    PAUSE,
    CONTINUE,
    END
}

public enum BulletName
{
    Enemy_Normal_0,
    Enemy_Boss_0,
    Enemy_Boss_1,
    COUNT
}

public enum BulletEventType
{
    ChangeSpeed,
    ChangeTrajectory
}

public enum BuffType
{
    NULL,
    LEVEL_UP
}

public enum DebuffType
{
    NULL
}

public enum ItemType
{
    ADD_BULLET,
    ADD_EXP,
    SHIELD,
    POWER
}