public class GameModel : NormalSingleton<GameModel>
{
    public void InitData()
    {
        CurrentLevel = SelectedLevel;
        Life = LifeMax;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_HP);
    }

    public int Life { get; set; }
    public int Score { get; set; }
    public int Stars { get; set; }
    public int ShieldCount { get; set; }
    public int PowerCount { get; set; }

    /// <summary>
    /// 选中的关卡
    /// </summary>
    public int SelectedLevel { get; set; }
    
    /// <summary>
    /// 当前的关卡
    /// </summary>
    public int CurrentLevel { get; set; }

    private int _tempLevel;

    /// <summary>
    /// 在游戏中的临时等级
    /// </summary>
    public int TempLevel
    {
        get { return _tempLevel; }
        set
        {
            if (value < GameStateModel.Single.PlaneLevelMax)
                _tempLevel = value;
        }
    }

    /// <summary>
    /// 是否完成了当前所在关卡
    /// </summary>
    public bool IsFinishOneLevel { get; set; }

    public int LifeMax
    {
        get
        {
            var key = KeysUtil.GetNewKey(PropertyItem.ItemKey.value, PlaneProperty.Property.life.ToString());
            return DataMgr.Single.Get<int>(key);
        }
    }

    public void Clear()
    {
        _single = new GameModel();
    }
}