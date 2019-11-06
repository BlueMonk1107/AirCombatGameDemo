using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class ViewBase : MonoBehaviour,IView
{
    private UiUtil _util;
    private List<IViewUpdate> _viewUpdates;
    private List<IViewInit> _viewInits;
    private List<IViewShow> _viewShows;
    private List<IViewHide> _viewHides;


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
        InitAllSubView();
        
        InitUpdateObjects();
    }

    protected abstract void InitChild();

    private void InitSubView()
    {
        _viewInits = new List<IViewInit>();
        _viewShows = new List<IViewShow>();
        _viewHides = new List<IViewHide>();

        InitViewInterface(_viewInits);
        InitViewInterface(_viewShows);
        InitViewInterface(_viewHides);
    }

    private void InitViewInterface<T>(List<T> list)
    {
        T view;
        foreach (Transform trans in transform)
        {
            view = trans.GetComponent<T>();
            if (view != null)
                list.Add(view);
        }
    }

    private void InitUpdateObjects()
    {
        _viewUpdates = transform.GetComponentsInChildren<IViewUpdate>().ToList();
    }

    private void InitAllSubView()
    {
        foreach (var view in _viewInits)
        {
            view.Init();
        }
    }

   

    public virtual void Show()
    {
        gameObject.SetActive(true);
        foreach (var view in _viewShows)
        {
            view.Show();
        }
    }

    public virtual void Hide()
    {
        foreach (var view in _viewHides)
        {
            view.Hide();
        }
        gameObject.SetActive(false);
    }

    private void UpdateAction()
    {
        foreach (IViewUpdate update in _viewUpdates)
        {
            update.UpdateFun();
        }
    }

    public virtual void UpdateFun()
    {
    }

    public Transform GetTrans()
    {
        return transform;
    }
}
