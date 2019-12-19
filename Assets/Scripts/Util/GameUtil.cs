

using UnityEngine;

public class GameUtil
{
    public static void ChangeHandPos(RectTransform rect)
    {
        float value = Mathf.Abs(rect.anchoredPosition.x);
        if (GameStateModel.Single.HandMode == HandMode.LEFT)
        {
            SetPosData(rect, Vector2.zero, value);
        }
        else
        {
            SetPosData(rect, Vector2.right, -value);
        }
    }

    private static void SetPosData(RectTransform rect,Vector2 anchorValue,float x)
    {
        rect.anchorMin = anchorValue;
        rect.anchorMax = anchorValue;
        var pos = rect.anchoredPosition;
        pos.x = x;
        rect.anchoredPosition = pos;
    }
}