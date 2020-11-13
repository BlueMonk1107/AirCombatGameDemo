using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuide
{
    void OnEnter(Action callBack = null);
    void Update();
}

public interface IGuideRoot : IGuide
{
    string GetViewName();
}

public abstract class GuideBase : IGuideRoot
{
    private Queue<IGuideGroup> _groups;
    protected abstract int GuideId { get; }

    public virtual void OnEnter(Action callBack = null)
    {
        _groups = GetGroups();
        ExecuteChildEnter();
    }

    public abstract string GetViewName();

    public void Update()
    {
        if(_groups == null)
            return;
        
        foreach (IGuideGroup guideGroup in _groups)
        {
            guideGroup.Update();
        }
    }

    protected abstract Queue<IGuideGroup> GetGroups();

    private void ExecuteChildEnter()
    {
        foreach (IGuideGroup guideGroup in _groups)
        {
            guideGroup.OnEnter();
        }
    }
}

