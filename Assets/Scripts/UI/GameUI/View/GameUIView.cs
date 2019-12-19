using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[BindPrefab(Paths.PREFAB_GAME_UI_VIEW,Const.BIND_PREFAB_PRIORITY_VIEW)]
public class GameUIView : ViewBase ,IReceiver
{
    
    protected override void InitChild()
    {
        Util.Get("Life").Add<Life>();
        Util.Get("Shield").Add<Shield>();
        Util.Get("Bomb").Add<Bomb>();
        ReceiveMessage();
    }

    public override void Show()
    {
        base.Show();
        MessageMgr.Single.AddListener(MsgEvent.EVENT_SCORE,this);
    }
    
    public override void Hide()
    {
        base.Hide();
        MessageMgr.Single.RemoveListener(MsgEvent.EVENT_SCORE,this);
    }

    public void ReceiveMessage(params object[] args)
    {
        UpdateScore();
        UpdateStars();
    }

    private void UpdateScore()
    {
        Util.Get("Score").SetText(GameModel.Single.Score);
    }

    private void UpdateStars()
    {
        Util.Get("Star/Value").SetText(GameModel.Single.Stars);
    }
}
