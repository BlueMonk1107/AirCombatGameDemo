using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager : NormalSingleton<UIManager>
{
    private Canvas _canvas;
    private IView _dialog;

    private readonly HashSet<string> _skipViews = new HashSet<string>
    {
        Paths.PREFAB_LOADING_VIEW
    };

    private readonly Stack<string> _uiStack = new Stack<string>();
    private readonly Dictionary<string, IView> _views = new Dictionary<string, IView>();

    public Canvas Canvas
    {
        get
        {
            if (_canvas == null)
                _canvas = Object.FindObjectOfType<Canvas>();

            if (_canvas == null)
                Debug.LogError("场景中没有Canvas");

            return _canvas;
        }
    }

    public IView Show(string path)
    {
        if (_uiStack.Count > 0)
        {
            var name = _uiStack.Peek();
            if (GetLayer(name) >= GetLayer(path)) 
                HideAll(_views[name]);
        }

        var view = InitView(path);

        ShowAll(view);

        if (!_skipViews.Contains(path))
            _uiStack.Push(path);

        _views[path] = view;

        return view;
    }

    private UILayer GetLayer(string path)
    {
        return UILayerMgr.Single.GetLayer(path);
    }

    public DialogView ShowDialog(string content, Action trueAction = null, Action falseAcion = null)
    {
        var dialogGo = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_DIALOG, Canvas.transform);
        AddTypeComponent(dialogGo, Paths.PREFAB_DIALOG);

        var dialog = dialogGo.GetComponent<DialogView>();
        if (dialog != null) 
            dialog.InitDialog(content, trueAction, falseAcion);

        _dialog = dialog;
        return dialog;
    }

    private IView InitView(string path)
    {
        if (_views.ContainsKey(path))
        {
            return _views[path];
        }

        var viewGo = LoadMgr.Single.LoadPrefabAndInstantiate(path, Canvas.transform);

        InitLayer(path, viewGo.transform);

        AddTypeComponent(viewGo, path);

        AddUpdateListener(viewGo);

        InitComponent(viewGo);

        var view = viewGo.GetComponent<IView>();

        return view;
    }

    private void InitLayer(string path, Transform view)
    {
        UILayerMgr.Single.SetParent(path, view);
    }

    private void AddTypeComponent(GameObject viewGo, string path)
    {
        foreach (var type in BindUtil.GetType(path)) viewGo.AddComponent(type);
    }

    private void AddUpdateListener(GameObject viewGo)
    {
        var controller = viewGo.GetComponent<IController>();
        if (controller == null)
        {
            Debug.LogWarning("当前物体没有IController组件，物体名称:" + viewGo.name);
            return;
        }

        foreach (var update in viewGo.GetComponents<IUpdate>()) controller.AddUpdateListener(update.UpdateFun);
    }

    private void InitComponent(GameObject viewGo)
    {
        var inits = viewGo.GetComponents<IInit>();

        foreach (var init in inits) init.Init();
    }

    public void Back()
    {
        if (_uiStack.Count <= 1)
            return;

        if (_dialog == null)
        {
            
            var name = _uiStack.Pop();
            HideAll(_views[name]);
            name = _uiStack.Peek();
            ShowAll(_views[name]);
        }
        else
        {
            _dialog.Hide();
            _dialog = null;
            _views[_uiStack.Peek()].Show();
        }
    }

    public void Hide(string name)
    {
        HideAll(_views[name]);
    }

    private void ShowAll(IView view)
    {
        foreach (var show in view.GetTrans().GetComponents<IShow>()) show.Show();
    }

    private void HideAll(IView view)
    {
        foreach (var hide in view.GetTrans().GetComponents<IHide>()) hide.Hide();
    }

    public Transform GetViwePrefab(string path)
    {
        if (_views.ContainsKey(path))
        {
            return _views[path].GetTrans();
        }
        else
        {
            Debug.LogError("当前预制为在uimanager管理当中,path:"+path);
            return null;
        }
    }
}