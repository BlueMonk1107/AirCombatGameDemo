using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour 
{

    public int Life { get; set; }

    public void Init(int life)
    {
        Life = life;
    }
}
