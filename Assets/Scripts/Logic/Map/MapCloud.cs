using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCloud : MonoBehaviour {
    
    private void Start()
    {
        SetActive(false);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
