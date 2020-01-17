public class Const
{
    /// <summary>
    ///     view界面绑定脚本优先级
    /// </summary>
    public const int BIND_PREFAB_PRIORITY_VIEW = 0;

    /// <summary>
    ///     controller绑定脚本优先级
    /// </summary>
    public const int BIND_PREFAB_PRIORITY_CONTROLLER = 1;


    //游戏中部分常量数据
    public const int LIFE_ITEM_NUM = 10;
    public const float CD_EFFECt_TIME = 2;

    /// <summary>
    ///     开火的基础时间
    /// </summary>
    public const float FIRE_BASE_TIME = 1;

    /// <summary>
    ///     开火的CD时间
    /// </summary>
    public const float FIRE_CD_TIME = 0.15f;

    /// <summary>
    ///     开火的持续时间
    /// </summary>
    public const float FIRE_DURATION = 0.3f;

    /// <summary>
    ///     护盾持续时间
    /// </summary>
    public const float SHIELD_TIME = 6;


    public const string MAP_PREFIX = "map_level_";

    public const string ENEMY_PREFIX = "Enemy_{0}_{1}";

    /// <summary>
    /// 敌方血条自适应宽度比例
    /// </summary>
    public const float ENEMY_LIFE_BAR_WIDTH = 0.8f;
}