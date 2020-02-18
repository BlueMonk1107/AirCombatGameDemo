using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void Begin();
    void Stop(Action callBack);
    void Hide();
    void Clear();
}
