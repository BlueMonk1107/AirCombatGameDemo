using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuideMgrBase<T> where T : new()
{
    protected static T _single;

    public static T Single
    {
        get
        {
            if (_single == null)
            {
                var t = new T();

                _single = t;
            }

            return _single;
        }
    }

    private Dictionary<string, IGuideRoot> _viewGuide;
    private IGuideRoot _currentGuide;
    
    public virtual void InitGuide()
    {
        _viewGuide = GetViweGuides();
    }

    protected void ShowUI(string name)
    {
        if (_viewGuide.ContainsKey(name))
        {
            _currentGuide = _viewGuide[name];
            _currentGuide.OnEnter();
        }
    }
    
    protected void HideUI(string name)
    {
        if (_currentGuide.GetViewName() == name)
            _currentGuide = null;
    }

    public void Update()
    {
        if(_currentGuide != null)
            _currentGuide.Update();
    }

    protected abstract Dictionary<string, IGuideRoot> GetViweGuides();
}
