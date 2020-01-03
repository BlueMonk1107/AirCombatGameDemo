using UnityEngine;

public interface IGameView : IInit
{
    GameLayer Layer { get; }
    Transform Self { get; }
}

public abstract class GameViewBase : MonoBehaviour, IGameView
{
    public virtual void Init()
    {
        InitComponent();
    }

    public abstract GameLayer Layer { get; }

    public Transform Self
    {
        get { return transform; }
    } 

    protected virtual void InitComponent()
    {
    }

    protected virtual void OnEnable()
    {
        if (GameLayerMgr.Single != null) 
            GameLayerMgr.Single.SetLayerParent(this);

        Init();
    }
}

public class BGView : GameViewBase
{
    public override GameLayer Layer => GameLayer.BACKGROUND;
}

public class PlaneView : GameViewBase
{
    public override GameLayer Layer => GameLayer.PLANE;
}

public class EffectView : GameViewBase
{
    public override GameLayer Layer => GameLayer.EFFECT;
}