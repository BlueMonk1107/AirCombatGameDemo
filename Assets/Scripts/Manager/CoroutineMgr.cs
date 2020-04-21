using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineMgr : MonoSingleton<CoroutineMgr>
{
    private readonly Dictionary<int, CoroutineController> _controllersDic;
    private readonly Dictionary<int, CoroutineController> _onceDic;
    public CoroutineMgr()
    {
        _controllersDic = new Dictionary<int, CoroutineController>();
        _onceDic = new Dictionary<int, CoroutineController>();
    }

    public int Execute(IEnumerator routine, bool autoStart = true)
    {
        var controller = new CoroutineController(this, routine);

        _controllersDic.Add(controller.ID, controller);
        if (autoStart)
            StartExecute(controller.ID);

        return controller.ID;
    }

    public int ExecuteOnce(IEnumerator routine)
    {
        var controller = new CoroutineController(this, routine);
        controller.Start();
        
        _onceDic.Add(controller.ID, controller);
        return controller.ID;
    }

    public void Restart(int id)
    {
        var controller = GetController(id);

        if (controller != null)
            controller.ReStart();
    }

    public void StartExecute(int id)
    {
        var controller = GetController(id);

        if (controller != null)
            controller.Start();
    }

    public void Pause(int id)
    {
        var controller = GetController(id);

        if (controller != null)
            controller.Pause();
    }

    public void Stop(int id)
    {
        var controller = GetController(id);

        if (controller != null)
            controller.Stop();
        if (_onceDic.ContainsKey(id))
            _onceDic.Remove(id);
    }

    public void Continue(int id)
    {
        var controller = GetController(id);

        if (controller != null)
            controller.Continue();
    }

    private CoroutineController GetController(int id)
    {
        if (_controllersDic.ContainsKey(id))
        {
            return _controllersDic[id];
        }
        else if(_onceDic.ContainsKey(id))
        {
            return _onceDic[id];
        }

        return null;
    }

    public void Delay(float time, Action callBack)
    {
        Execute(Wait(time, callBack));
    }

    private IEnumerator Wait(float time, Action callBack)
    {
        yield return new WaitForSeconds(time);
        if (callBack != null)
            callBack();
    }
}