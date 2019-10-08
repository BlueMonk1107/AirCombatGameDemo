using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewBase : MonoBehaviour, IView
{
    private UiUtil _util;

    protected UiUtil Util
    {
        get
        {
            if (_util == null)
            {
                _util = gameObject.AddComponent<UiUtil>();
                _util.Init();
            }

            return _util;
        }
    }

    public virtual void Init()
    {

    }

    public virtual void Show()
    {

    }

    public virtual void Hide()
    {

    }
}