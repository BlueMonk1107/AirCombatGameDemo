using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewBase : MonoBehaviour, IView
{
    private UiUtil _util;
    private HashSet<ViewBase> _views;

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
        InitChild();
        InitSubView();

        foreach (ViewBase view in _views)
        {
            view.Init();
        }
    }

    protected abstract void InitChild();

    private void InitSubView()
    {
        _views = new HashSet<ViewBase>();
        ViewBase view = null;
        foreach (Transform trans in transform)
        {
            view = trans.GetComponent<ViewBase>();
            if (view != null)
            {
                _views.Add(view);
            }
        }
    }
    
    public virtual void Show()
    {
        gameObject.SetActive(true);
        foreach (ViewBase view in _views)
        {
            view.Show();
        }
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        foreach (ViewBase view in _views)
        {
            view.Hide();
        }
    }
}