using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : NormalSingleton<SceneMgr>
{
    private AsyncOperation _async;
    private readonly Dictionary<SceneName, int> _initItemTotalNum = new Dictionary<SceneName, int>();
    private readonly Dictionary<SceneName, Action<Action>> _loadedDic = new Dictionary<SceneName, Action<Action>>();
    private readonly Dictionary<SceneName, Action> _unloadedDic = new Dictionary<SceneName, Action>();

    public SceneMgr()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnload;
        InitItemTotalNum();
    }

    public int CurInitNum { get; private set; }

    public int InitTotalNum => _initItemTotalNum[GameStateModel.Single.TargetScene];

    private void InitItemTotalNum()
    {
        for (var i = SceneName.Main; i < SceneName.COUNT; i++) 
            _initItemTotalNum[i] = 1;
    }

    public void AsyncLoadScene(SceneName name)
    {
        ResetData();
        CoroutineMgr.Single.ExecuteOnce(AsyncLoad(name.ToString()));
    }

    private IEnumerator AsyncLoad(string name)
    {
        _async = SceneManager.LoadSceneAsync(name);
        _async.allowSceneActivation = false;
        yield return _async;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var name = (SceneName) Enum.Parse(typeof(SceneName), scene.name);
        if (_loadedDic.ContainsKey(name) && _loadedDic[name] != null) _loadedDic[name](LoadCallBack);
    }

    private void LoadCallBack()
    {
        CurInitNum++;
    }

    private void OnSceneUnload(Scene scene)
    {
        var name = (SceneName) Enum.Parse(typeof(SceneName), scene.name);
        if (_unloadedDic.ContainsKey(name) && _unloadedDic[name] != null) _unloadedDic[name]();
    }

    public void AddSceneLoaded(SceneName name, Action<Action> action)
    {
        _initItemTotalNum[name] += 1;

        if (_loadedDic.ContainsKey(name))
            _loadedDic[name] += action;
        else
            _loadedDic[name] = action;
    }

    public void AddSceneUnloaded(SceneName name, Action action)
    {
        if (_unloadedDic.ContainsKey(name))
            _unloadedDic[name] += action;
        else
            _unloadedDic[name] = action;
    }

    public float Process()
    {
        var ratio = CurInitNum / (float) InitTotalNum;
        if (_async != null && _async.progress >= 0.9f) 
            SceneActivation();

        return ratio;
    }

    public void SceneActivation()
    {
        CurInitNum++;
        _async.allowSceneActivation = true;
        GameStateModel.Single.CurrentScene = GameStateModel.Single.TargetScene;
        _async = null;
    }

    private void ResetData()
    {
        CurInitNum = 0;
        InitItemTotalNum();
    }

    public bool IsScene(SceneName name)
    {
        return GameStateModel.Single.CurrentScene == name;
    }
}