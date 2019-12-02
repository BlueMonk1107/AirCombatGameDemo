using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController : IControllerInit,IControllerShow,IControllerHide,IControllerUpdate
{
    void AddUpdateListener(Action update);
    void Reacquire();
}
