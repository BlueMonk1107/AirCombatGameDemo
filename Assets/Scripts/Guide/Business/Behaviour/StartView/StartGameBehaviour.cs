using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameBehaviour : GuideBehaviourBase
{
    private Transform _highLight;
    private Transform _hand;
    private RectTransform _startButtonTrans;
    
    protected override void OnEnterLogic()
    {
        var startView = UIManager.Single.GetCurrentViewPrefab();
        _startButtonTrans = startView.Find("Start").GetComponent<RectTransform>();

        _highLight = GuideUtil.GetHighLightTrans(_startButtonTrans, OnExit);

        _hand = GuideUtil.GetHandTrans(_startButtonTrans, new Vector2(0.9f, 0.2f));
    }

    protected override void OnExitLogic()
    {
        GuideUiMgr.Single.Hide(_highLight);
        GuideUiMgr.Single.Hide(_hand);
        _startButtonTrans.GetComponent<Button>().onClick.Invoke();
    }
}
