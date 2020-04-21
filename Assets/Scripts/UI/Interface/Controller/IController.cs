using System;

public interface IController : IControllerInit, IControllerShow, IControllerHide, IControllerUpdate
{
    void AddUpdateListener(Action update);
    void Reacquire();
}