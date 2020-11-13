using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeGuideBehaviour : GuideBehaviourBase
{
    private Transform _view;
    protected override void OnEnterLogic()
    {
        _view = GuideUiMgr.Single.Show(Paths.PREFAB_WELCOME_GUIDE);
        var button = _view.AddOrGet<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnExit);
    }

    protected override void OnExitLogic()
    {
        GuideUiMgr.Single.Hide(_view);
    }
}
