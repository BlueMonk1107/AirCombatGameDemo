using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpState : IBuff
{
    private const float TIME_ONCE = 20;
    private int _level;
    private SubMsgMgr _msgMgr;
    private int _id = -1;
    
    public void Start(SubMsgMgr msgMgr)
    {
        _msgMgr = msgMgr;
        MessageMgr.Single.AddListener(MsgEvent.EVENT_GAME_EXP_LEVEL_UP,LevelUpByExp);
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_GAME_UPDATE_LEVEL);

        _id = DelayDetalCoroutineMgr.Single.Start(TIME_ONCE, BeginCallBack, EndCallBack, _id);
        GameModel.Single.TempLevel++;
    }

    private void BeginCallBack()
    {
        _level = GameModel.Single.TempLevel;
    }
    
    private void EndCallBack()
    {
        _id = -1;
        GameModel.Single.TempLevel = _level;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_GAME_UPDATE_LEVEL);
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_GAME_EXP_LEVEL_UP,LevelUpByExp);
    }
    
    private void LevelUpByExp(object[] paras)
    {
        _level++;
    }
}
