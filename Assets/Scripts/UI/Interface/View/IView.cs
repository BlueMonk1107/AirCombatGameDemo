using UnityEngine;

public interface IView : IViewInit, IViewShow, IViewHide, IViewUpdate
{
    Transform GetTrans();
    void Reacquire();
}