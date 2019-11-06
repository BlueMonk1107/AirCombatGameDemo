using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBase : MonoBehaviour,IController {
    public virtual void Init()
    {
        AddUpdateAction();
    }
    
    private void AddUpdateAction()
    {
//        foreach (Button button in GetComponentsInChildren<Button>())
//        {
//            button.onClick.AddListener(UpdateAction);
//        }
    }

    public virtual void Show()
    {
    }

    public virtual void Hide()
    {
    }

    public virtual void UpdateFun()
    {
    }
}
