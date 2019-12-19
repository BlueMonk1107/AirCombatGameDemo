using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemController : ControllerBase
{
    private int _id;
    
    protected override void InitChild()
    {
        _id = transform.GetSiblingIndex();
        transform.ButtonAction("Enter", () =>
        {
            GameModel.Single.SelectedLevel = _id;
            GameStateModel.Single.TargetScene = SceneName.Game;
            UIManager.Single.Show(Paths.PREFAB_LOADING_VIEW);
        });
        transform.ButtonAction("Mask",()=>UIManager.Single.ShowDialog("当前关卡未开放"));
    }
}
