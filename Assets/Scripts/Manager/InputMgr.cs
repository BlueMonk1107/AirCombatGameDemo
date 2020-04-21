using UnityEngine;

public class InputMgr : NormalSingleton<InputMgr>, IInputModule, IUpdate
{
    private readonly InputModule _module;
    private readonly bool _updating;

    public InputMgr()
    {
        _updating = false;
        _module = new InputModule();
        _module.AddSendEvent(SendKey);
        _module.AddSendEvent(SendMouse);
    }


    public void AddListener(KeyCode code)
    {
        _module.AddListener(code);
        AddUpdate();
    }

    public void AddMouseListener(int code)
    {
        _module.AddMouseListener(code);
        AddUpdate();
    }

    public void RemoveListener(KeyCode code)
    {
        _module.RemoveListener(code);
        RemoveUpdate();
    }

    public void RemoveMouseListener(int code)
    {
        _module.RemoveMouseListener(code);
        RemoveUpdate();
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

    public void UpdateFun()
    {
        _module.Execute();
    }

    private void SendKey(KeyCode code, InputState state)
    {
        MessageMgr.Single.DispatchMsg(GetKey(code, state), state);
    }

    private void SendMouse(int code, InputState state)
    {
        MessageMgr.Single.DispatchMsg(GetKey(code, state), state);
    }

    public string GetKey(KeyCode code, InputState state)
    {
        return code + state.ToString();
    }

    public string GetKey(int code, InputState state)
    {
        return code + state.ToString();
    }

    private void AddUpdate()
    {
        if (!_updating) LifeCycleMgr.Single.Add(LifeName.UPDATE, this);
    }

    private void RemoveUpdate()
    {
        if (_module.ListenerCount == 0) LifeCycleMgr.Single.Remove(LifeName.UPDATE, this);
    }
}