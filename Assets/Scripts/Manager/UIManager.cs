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
    private IView _dialog;
    
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

    public DialogView ShowDialog(string content,Action trueAction = null,Action falseAcion = null)
    {
        var dialogGo = LoadMgr.Single.LoadPrefab(Paths.PREFAB_DIALOG, Canvas.transform);
        AddTypeComponent(dialogGo,Paths.PREFAB_DIALOG);

        DialogView dialog = dialogGo.GetComponent<DialogView>();
        if (dialog != null)
        {
            dialog.InitDialog(content,trueAction,falseAcion);
        }

        _dialog = dialog;
        return dialog;
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

            AddTypeComponent(viewGo, path);

            AddUpdateListener(viewGo);

            InitComponent(viewGo);

            IView view = viewGo.GetComponent<IView>();
            
            return view;
        }
    }

    private void AddTypeComponent(GameObject viewGo,string path)
    {
        foreach (var type in BindUtil.GetType(path))
        {
            viewGo.AddComponent(type);
        }
    }

    private void AddUpdateListener(GameObject viewGo)
    {
        var controller = viewGo.GetComponent<IController>();
        if (controller == null)
        {
            Debug.LogWarning("当前物体没有IController组件，物体名称:"+viewGo.name);
            return;
        }

        foreach (IUpdate update in viewGo.GetComponents<IUpdate>())
        {
            controller.AddUpdateListener(update.UpdateFun);
        }
    }

    private void InitComponent(GameObject viewGo)
    {
        IInit[] inits = viewGo.GetComponents<IInit>();

        foreach (var init in inits)
        {
            init.Init();
        }
    }

    public void Back()
    {
        if(_uiStack.Count <= 1)
            return;

        if (_dialog == null)
        {
            string name = _uiStack.Pop();
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
