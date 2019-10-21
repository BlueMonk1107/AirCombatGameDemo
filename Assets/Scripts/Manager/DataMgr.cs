using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : NormalSingleton<DataMgr> 
{
    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key,0);
    }

    public void SetInt(string key,int value)
    {
        PlayerPrefs.SetInt(key,value);
    }
}
