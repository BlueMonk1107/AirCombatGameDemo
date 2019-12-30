using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameView: IInit
{
    GameLayer Layer { get; }
    Transform Self { get; }
}

public abstract class GameViewBase : MonoBehaviour,IGameView
{
    public virtual void Init()
    {
        InitComponent();
    }

    protected virtual void InitComponent()
    {
        
    }

    public abstract GameLayer Layer { get; }
    public Transform Self
    {
        get { return transform; }
    }

    private void OnEnable()
    {
        if (GameLayerMgr.Single != null)
        {
            GameLayerMgr.Single.SetLayerParent(this);
        }

        Init();
    }
}

public class BGView : GameViewBase
{
    public override GameLayer Layer
    {
        get { return GameLayer.BACKGROUND; }
    }
}

public class PlaneView : GameViewBase
{
    public override GameLayer Layer
    {
        get { return GameLayer.PLANE; }
    }
}

public class EffectView : GameViewBase
{
    public override GameLayer Layer
    {
        get { return GameLayer.EFFECT; }
    }
}