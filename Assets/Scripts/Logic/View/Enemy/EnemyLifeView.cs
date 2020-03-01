using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeView : MonoBehaviour
{
    private LifeComponent _life;
    private int _lifeItemCount;

    public void Init()
    {
        _lifeItemCount = transform.childCount;
        _life = transform.GetComponentInParent<LifeComponent>();
        var planeRender = transform.parent.GetComponent<SpriteRenderer>();
        if (planeRender == null)
            Debug.LogError("父物体上没有找到SpriteRenderer组件");
        transform.localScale = GetRatio(planeRender) * transform.localScale;
        InitPos(planeRender);
        InitItem();
    }

    private void InitItem()
    {
        foreach (Transform trans in transform)
        {
            trans.AddOrGet<EnemyLifeItem>().Init();
        }
    }

    private float GetRatio(SpriteRenderer render)
    {
        if (render == null)
            return 0;
        float planeWidth = render.bounds.size.y;
        float targetBarWidth = planeWidth * Const.ENEMY_LIFE_BAR_WIDTH;
        int count = transform.childCount;
        float xMin = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.min.x;
        float xMax = transform.GetChild(count - 1).GetComponent<SpriteRenderer>().bounds.max.x;
        float realWidth = xMax - xMin;
        float ratio = targetBarWidth / realWidth;
        return ratio;
    }

    private void InitPos(SpriteRenderer render)
    {
        if (render == null)
            return;
        float yMin = render.bounds.min.y;
        float xCenter = render.bounds.center.x;
        float itemHeigh = 0;
        if (transform.childCount > 0)
        {
            itemHeigh = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y;
        }

        transform.position = new Vector3(xCenter, yMin - itemHeigh / 2, transform.position.z);
    }
    
}