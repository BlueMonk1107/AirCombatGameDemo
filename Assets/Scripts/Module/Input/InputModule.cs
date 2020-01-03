using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInputModule
{
    void AddListener(KeyCode code);
    void AddMouseListener(int code);
    void RemoveListener(KeyCode code);
    void RemoveMouseListener(int code);
}

public class InputModule : IInputModule
{
    private readonly Dictionary<KeyCode, int> _keyCodeDic;
    private Action<KeyCode, InputState> _keyEvent;
    private readonly Dictionary<int, int> _mouseDic;
    private Action<int, InputState> _mouseEvent;

    public InputModule()
    {
        _keyCodeDic = new Dictionary<KeyCode, int>();
        _mouseDic = new Dictionary<int, int>();
    }

    public int ListenerCount
    {
        get
        {
            if (_keyCodeDic == null || _mouseDic == null)
                return 0;

            return _keyCodeDic.Count + _mouseDic.Count;
        }
    }

    public void AddListener(KeyCode code)
    {
        if (_keyCodeDic.ContainsKey(code))
            _keyCodeDic[code] += 1;
        else
            _keyCodeDic.Add(code, 1);
    }

    public void AddMouseListener(int code)
    {
        if (_mouseDic.ContainsKey(code))
            _mouseDic[code] += 1;
        else
            _mouseDic.Add(code, 1);
    }

    public void RemoveListener(KeyCode code)
    {
        if (_keyCodeDic.ContainsKey(code))
        {
            _keyCodeDic[code] -= 1;
            if (_keyCodeDic[code] <= 0) _keyCodeDic.Remove(code);
        }
        else
        {
            Debug.LogError("当前移除指令并没有被监听，Keycode：" + code);
        }
    }

    public void RemoveMouseListener(int code)
    {
        if (_mouseDic.ContainsKey(code))
        {
            _mouseDic[code] -= 1;
            if (_mouseDic[code] <= 0) _mouseDic.Remove(code);
        }
        else
        {
            Debug.LogError("当前移除指令并没有被监听，Keycode：" + code);
        }
    }

    public void AddSendEvent(Action<KeyCode, InputState> keyEvent)
    {
        _keyEvent = keyEvent;
    }

    public void AddSendEvent(Action<int, InputState> keyEvent)
    {
        _mouseEvent = keyEvent;
    }


    public void Execute()
    {
        if (_keyEvent == null || _mouseEvent == null)
        {
            Debug.LogError("输入监听模块发送消息事件不能为空");
            return;
        }

        foreach (var pair in _keyCodeDic)
        {
            if (Input.GetKeyDown(pair.Key)) _keyEvent(pair.Key, InputState.DOWN);
            if (Input.GetKey(pair.Key)) _keyEvent(pair.Key, InputState.PREE);
            if (Input.GetKeyUp(pair.Key)) _keyEvent(pair.Key, InputState.UP);
        }

        foreach (var pair in _mouseDic)
        {
            if (Input.GetMouseButtonDown(pair.Key)) _mouseEvent(pair.Key, InputState.DOWN);
            if (Input.GetMouseButton(pair.Key)) _mouseEvent(pair.Key, InputState.PREE);
            if (Input.GetMouseButtonUp(pair.Key)) _mouseEvent(pair.Key, InputState.UP);
        }
    }
}

public enum InputState
{
    DOWN,
    PREE,
    UP
}