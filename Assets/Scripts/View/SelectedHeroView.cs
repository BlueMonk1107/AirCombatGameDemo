using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedHeroView : ViewBase {

    protected override void InitChild()
    {
        Util.Get("Heros").Go.AddComponent<SelectHero>();
        Util.Get("Strengthen").AddListener(() =>
        {
            UIManager.Single.Show(Paths.STRENGTHEN_VIEW);
        });
        
        Util.Get("Exit").AddListener(() =>
        {
            Application.Quit();
        });
        
        Util.Get("OK/Start").AddListener(() =>
        {
            UIManager.Single.Show(Paths.LEVELS_VIEW);
        });
    }
}
