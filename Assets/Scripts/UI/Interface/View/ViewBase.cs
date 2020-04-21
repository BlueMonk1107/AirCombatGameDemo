using System.Collections.Generic;
using UnityEngine;

public abstract class ViewBase : MonoBehaviour, IView
{
    private UiUtil _util;
    private List<IViewHide> _viewHides;
    private List<IViewInit> _viewInits;
    private List<IViewShow> _viewShows;
    private List<IViewUpdate> _viewUpdates;


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
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        foreach (var view in _viewShows) view.Show();

        UpdateFun();
    }

    public virtual void Hide()
    {
        foreach (var view in _viewHides) view.Hide();
        gameObject.SetActive(false);
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

    public virtual void UpdateFun()
    {
        foreach (var update in _viewUpdates) update.UpdateFun();
    }

    public Transform GetTrans()
    {
        return transform;
    }

    public void Reacquire()
    {
        InitInterface();
        InitAllSubView();
    }

    protected abstract void InitChild();

    private void InitSubView()
    {
        _viewInits = new List<IViewInit>();
        _viewShows = new List<IViewShow>();
        _viewHides = new List<IViewHide>();
        _viewUpdates = new List<IViewUpdate>();

        InitInterface();
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

    private void InitAllSubView()
    {
        foreach (var view in _viewInits) view.Init();
    }

    private void InitInterface()
    {
        InitViewInterface(_viewInits);
        InitViewInterface(_viewShows);
        InitViewInterface(_viewHides);
        InitViewInterface(_viewUpdates);
    }
}