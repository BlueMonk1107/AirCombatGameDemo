using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHerosBehaviour : GuideBehaviourBase {
    
    private Transform _highLight;
    protected override void OnEnterLogic()
    {
        var go = UIManager.Single.GetCurrentViewPrefab();
        Transform parent = go.Find("Heros");
        RectTransform[] children = parent.GetComponentsInChildren<RectTransform>();
        _highLight = GuideUtil.GetHighLightTrans(children, OnExit);
    }

    protected override void OnExitLogic()
    {
        GuideUiMgr.Single.Hide(_highLight);
    }
}
