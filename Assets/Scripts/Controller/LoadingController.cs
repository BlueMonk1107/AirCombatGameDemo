using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[BindPrefab(Paths.PREFAB_LOADING_VIEW,Const.BIND_PREFAB_PRIORITY_CONTROLLER)]
public class LoadingController : ControllerBase {
    protected override void InitChild()
    {
        
    }

    public override void Show()
    {
        base.Show();
        if (GameStateModel.Single.TargetScene != GameStateModel.Single.CurrentScene
            && GameStateModel.Single.TargetScene != SceneName.NULL)
        {
            SceneMgr.Single.AsyncLoadScene(GameStateModel.Single.TargetScene);
            LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
        }
    }

    public override void UpdateFun()
    {
        base.UpdateFun();
        if (SceneMgr.Single.Process() == 1)
        {
            SceneMgr.Single.SceneActivation();
        }
    }

    public override void Hide()
    {
        base.Hide();
        if (GameStateModel.Single.TargetScene != SceneName.NULL)
        {
            GameStateModel.Single.CurrentScene = GameStateModel.Single.TargetScene;
            GameStateModel.Single.TargetScene = SceneName.NULL;
        }
        
        LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
    }

    private void OnDestroy()
    {
        Hide();
    }
}
