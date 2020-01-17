public class GameModel : NormalSingleton<GameModel>
{
    public GameModel()
    {
        Life = LifeMax;
    }

    public int Life { get; set; }
    public int Score { get; set; }
    public int Stars { get; set; }
    public int ShieldCount { get; set; }
    public int BombCount { get; set; }

    /// <summary>
    /// 选中的关卡
    /// </summary>
    public int SelectedLevel { get; set; }

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