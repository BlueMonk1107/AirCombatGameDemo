using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BindPrefab(Paths.PREFAB_GAME_RESULT_VIEW, Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class GameResultController : ControllerBase {
    protected override void InitChild()
    {
        transform.ButtonAction("BGButton",BackToMain);
    }

    private void BackToMain()
    {
        if (GameModel.Single.IsFinishOneLevel)
        {
            UIManager.Single.Back();
        }
        else
        {
            UIManager.Single.Back();
            GameStateModel.Single.TargetScene = SceneName.Main;
            UIManager.Single.Show(Paths.PREFAB_LOADING_VIEW);
        }
    }

    public override void Show()
    {
        base.Show();
        GameStateModel.Single.GameState = GameState.PAUSE;
    }

    public override void Hide()
    {
        base.Hide();
        GameStateModel.Single.GameState = GameState.NULL;
    }
}
