using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView : IViewInit,IViewShow,IViewHide,IViewUpdate
{
    Transform GetTrans();
    void Reacquire();
}
