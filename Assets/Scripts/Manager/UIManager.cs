using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager : NormalSingleton<UIManager>
{
    private Stack<string> _uiStack = new Stack<string>();
    private Dictionary<string,IView> _views = new Dictionary<string, IView>();
    private Canvas _canvas;
    public Canvas Canvas
    {
        get
        {
            if (_canvas == null)
                _canvas = Object.FindObjectOfType<Canvas>();
            
            if(_canvas == null)
                Debug.LogError("场景中没有Canvas");

            return _canvas;
        }
    }

    public IView Show(string path)
    {
        if (_uiStack.Count > 0)
        {
            string name = _uiStack.Peek();
            _views[name].Hide();
        }

        IView view = InitView(path);

        ShowAll(view);
        
        _uiStack.Push(path);
        _views[path] = view;

        return view;
    }

    private IView InitView(string path)
    {
        if (_views.ContainsKey(path))
        {
            return _views[path];
        }
        else
        {
            GameObject viewGo = LoadMgr.Single.LoadPrefab(path, Canvas.transform);

            foreach (var type in BindUtil.GetType(path))
            {
                viewGo.AddComponent(type);
                
                IInit[] inits = viewGo.GetComponents<IInit>();

                foreach (var init in inits)
                {
                    init.Init();
                }
            }

            IView view = viewGo.GetComponent<IView>();
            
            return view;
        }
    }

    public void Back()
    {
        if(_uiStack.Count <= 1)
            return;

        string name = _uiStack.Pop();
        HideAll(_views[name]);

        name = _uiStack.Peek();
        ShowAll(_views[name]);
    }

    private void ShowAll(IView view)
    {
        foreach (IShow show in view.GetTrans().GetComponents<IShow>())
        {
            show.Show();
        }
    }
    
    private void HideAll(IView view)
    {
        foreach (IHide hide in view.GetTrans().GetComponents<IHide>())
        {
            hide.Hide();
        }
    }
}
