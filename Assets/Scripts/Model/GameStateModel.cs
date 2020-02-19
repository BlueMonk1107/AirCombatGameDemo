using System;
using UnityEngine;

public class GameStateModel : NormalSingleton<GameStateModel>
{
    public GameStateModel()
    {
        HandMode = (HandMode) DataMgr.Single.Get<int>(DataKeys.HAND_MODE);
        GameState = GameState.NULL;
    }

    public HandMode HandMode { get; set; }

    private GameState _gameState;

    /// <summary>
    /// 判断是否在游戏状态中
    /// </summary>
    public GameState GameState
    {
        get { return _gameState; }
        set
        {
            if(_gameState == value)
                return;
            
            _gameState = value;
            switch (value)
            {
                case GameState.START:
                    MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_START_GAME);
                    break;
                case GameState.PAUSE:
                    MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_GAME_PAUSE);
                    break;
                case GameState.CONTINUE:
                    MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_GAME_CONTINUE);
                    break;
                case GameState.END:
                    MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_END_GAME);
                    break;
            }
        }
    }

    public SceneName CurrentScene { get; set; }
    public SceneName TargetScene { get; set; }
    public Hero SelectedHero { get; set; }
    public int SelectedPlaneId { get; set; }

    /// <summary>
    /// 飞机等级
    /// </summary>
    public int PlaneLevel
    {
        get
        {
            var key = KeysUtil.GetPropertyKeys(DataKeys.LEVEL);
            return DataMgr.Single.Get<int>(key);
        }
    }

    public int PlaneLevelMax
    {
        get
        {
            var key = KeysUtil.GetPropertyKeys(DataKeys.LEVEL_MAX);
            return DataMgr.Single.Get<int>(key);
        }
    }

    public int GetMoney(string key)
    {
        var money = 0;
        switch (key)
        {
            case DataKeys.STAR:
                money = DataMgr.Single.Get<int>(DataKeys.STAR);
                break;
            case DataKeys.DIAMOND:
                money = DataMgr.Single.Get<int>(DataKeys.DIAMOND);
                break;
            default:
                Debug.LogError("当前传入的key无法识别，key：" + key);
                break;
        }

        return money;
    }

    public void SetMoney(string key, int money)
    {
        switch (key)
        {
            case DataKeys.STAR:
                DataMgr.Single.Set(DataKeys.STAR, money);
                break;
            case DataKeys.DIAMOND:
                DataMgr.Single.Set(DataKeys.DIAMOND, money);
                break;
            default:
                Debug.LogError("当前传入的key无法识别，key：" + key);
                break;
        }
    }
}