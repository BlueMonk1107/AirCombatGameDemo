using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BindPrefab(Paths.PREFAB_GAME_RESULT_VIEW, Const.BIND_PREFAB_PRIORITY_VIEW)]
public class GameResultView : ViewBase
{

    private string _lose = "You Lose";
    private string _win = "You Win";
    protected override void InitChild()
    {
        
    }

    public override void Show()
    {
        base.Show();

        string result = GameModel.Single.IsFinishOneLevel ? _win : _lose;
        Util.Get("Result").SetText(result);
    }
}
