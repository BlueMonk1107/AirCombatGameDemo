using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUtil {

    public static Transform GetHighLightTrans(RectTransform rect,Action exit)
    {
        return GetHighLightTrans(new[] {rect}, exit);
    }
    
    public static Transform GetHighLightTrans(RectTransform[] rects,Action exit)
    {
        var highLight = GuideUiMgr.Single.Show(Paths.PREFAB_HIGH_LIGHT_GUIDE);
        var highLightModule = highLight.AddOrGet<HighLightModule>();
        highLightModule.Init(rects,Camera.main, exit);
        return highLight;
    }

    public static Transform GetHandTrans(RectTransform targetRect,Vector2 posRatio)
    {
        var hand = GuideUiMgr.Single.Show(Paths.PREFAB_HAND_GUIDE);
        var handModule = hand.AddOrGet<HandModule>();
        handModule.SetHand(targetRect,posRatio);
        return hand;
    }
}
