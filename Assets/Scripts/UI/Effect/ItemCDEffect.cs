using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemCDEffect : MonoBehaviour, IUpdate
{
    private Action _callBack;
    private Image _image;

    private Image Image
    {
        get
        {
            if (_image == null)
                _image = GetComponent<Image>();

            return _image;
        }
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

    public void UpdateFun()
    {
        Image.fillAmount -= Time.deltaTime / Const.CD_EFFECt_TIME;
        if (Image.fillAmount <= 0)
        {
            LifeCycleMgr.Single.Remove(LifeName.UPDATE, this);
            if (_callBack != null)
                _callBack();
        }
    }

    public void SetShow()
    {
        Image.fillAmount = 0;
    }

    public void SetMask()
    {
        Image.fillAmount = 1;
    }

    public void StartCD(Action callBack)
    {
        _callBack = callBack;
        Image.fillAmount = 1;
        LifeCycleMgr.Single.Add(LifeName.UPDATE, this);
    }
}