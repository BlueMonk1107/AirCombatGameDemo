using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ControllerBase : MonoBehaviour, IController
{
    private List<IControllerHide> _hides;
    private List<IControllerInit> _inits;
    private Action _onUpdate;
    private List<IControllerShow> _shows;
    private List<IControllerUpdate> _updates;

    public virtual void Init()
    {
        InitChild();
        InitAllComponents();
        InitComponents();
        AddUpdateAction();
    }

    public void Reacquire()
    {
        InitInterface();
        InitComponents();
    }

    public virtual void Show()
    {
        foreach (var component in _shows) component.Show();
    }

    public virtual void Hide()
    {
        foreach (var component in _hides) component.Hide();
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

    public virtual void UpdateFun()
    {
        foreach (var component in _updates) 
            component.UpdateFun();
    }

    public void AddUpdateListener(Action update)
    {
        _onUpdate += update;
    }

    protected abstract void InitChild();

    private void AddUpdateAction()
    {
        foreach (var button in GetComponentsInChildren<Button>())
            button.onClick.AddListener(() =>
            {
                if (_onUpdate != null)
                    _onUpdate();

                UpdateFun();
            });
    }

    private void InitInterface()
    {
        InitComponent(_inits, this);
        InitComponent(_shows, this);
        InitComponent(_hides, this);
        InitComponent(_updates, this);
    }

    private void InitAllComponents()
    {
        _inits = new List<IControllerInit>();
        _shows = new List<IControllerShow>();
        _hides = new List<IControllerHide>();
        _updates = new List<IControllerUpdate>();

        InitInterface();
    }

    private void InitComponent<T>(List<T> components, T removeObject)
    {
        var temp = transform.GetComponentsInChildren<T>();

        components.AddRange(temp);

        components.Remove(removeObject);
    }

    private void InitComponents()
    {
        foreach (var component in _inits) component.Init();
    }
}