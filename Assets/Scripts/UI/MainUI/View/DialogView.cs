using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[BindPrefab(Paths.PREFAB_DIALOG, Const.BIND_PREFAB_PRIORITY_VIEW)]
public class DialogView : MonoBehaviour, IView
{
    private readonly float _leftAndRight = 60;
    private readonly float _maxWidth = 550;
    private readonly float _minWidth = 330;
    private readonly string _noBtn = "No";
    private readonly float _offset = 40;

    private readonly string _onePath = "Frame/Buttons/One";
    private readonly string _twoPath = "Frame/Buttons/Two";
    private readonly float _upAndDown = 40;

    private UiUtil _util;
    private readonly string _yesBtn = "Yes";

    public void Init()
    {
    }

    public void Show()
    {
    }

    public void Hide()
    {
        Destroy(gameObject);
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

    public void UpdateFun()
    {
    }

    public Transform GetTrans()
    {
        return transform;
    }

    public void Reacquire()
    {
    }

    public void InitDialog(string content, Action trueAction = null, Action falseAcion = null)
    {
        if (_util == null)
        {
            _util = gameObject.AddComponent<UiUtil>();
            _util.Init();
        }

        UpdateContent(content);
        AddAction(trueAction, falseAcion);
        CoroutineMgr.Single.ExecuteOnce(ChangeSize());
    }

    private IEnumerator ChangeSize()
    {
        yield return null;
        var content = _util.Get("Frame/Content").RectTrans;
        var buttons = _util.Get("Frame/Buttons").RectTrans;
        var frame = _util.Get("Frame").RectTrans;
        SetWeight(content, frame);
        yield return null;
        SetHeight(content, buttons, frame);
    }

    private void SetWeight(RectTransform content, RectTransform frame)
    {
        var weight = _leftAndRight * 2 + content.rect.width;

        float result = 0;
        if (weight <= _minWidth)
        {
            result = _minWidth;
        }
        else if (weight > _maxWidth)
        {
            result = _maxWidth + _leftAndRight * 2;
            content.GetComponent<LayoutElement>().preferredWidth = _maxWidth;
        }
        else
        {
            result = weight;
        }

        frame.sizeDelta = new Vector2(result, frame.sizeDelta.y);
    }

    private void SetHeight(RectTransform content, RectTransform buttons, RectTransform frame)
    {
        SetContentY(content);
        SetButtonsY(content, buttons);
        SetFrameHeight(content, buttons, frame);
    }

    private void SetContentY(RectTransform content)
    {
        var y = content.rect.height * 0.5f + _upAndDown;
        SetPosY(content, y);
    }

    private void SetButtonsY(RectTransform content, RectTransform buttons)
    {
        var offset = _upAndDown + content.rect.height + _offset;

        var y = offset + buttons.rect.height * 0.5f;

        SetPosY(buttons, y);
    }

    private void SetFrameHeight(RectTransform content, RectTransform buttons, RectTransform frame)
    {
        var height = _upAndDown * 2 + _offset + content.rect.height + buttons.rect.height;
        var size = frame.sizeDelta;
        size.y = height;
        frame.sizeDelta = size;
    }

    private void SetPosY(RectTransform rect, float y)
    {
        var pos = rect.anchoredPosition;
        pos.y = -y;
        rect.anchoredPosition = pos;
    }

    private void UpdateContent(string content)
    {
        transform.Find("Frame/Content").GetComponent<Text>().text = content;
    }

    private void AddAction(Action trueAction, Action falseAcion)
    {
        if (trueAction == null && falseAcion == null)
        {
            SetButtonState(true);
            AddOneListener(trueAction);
        }
        else if (trueAction == null && falseAcion != null)
        {
            Debug.LogError("在取消事件不为空时，确认事件不能为空");
            AddOneListener(null);
        }
        else if (trueAction != null && falseAcion == null)
        {
            SetButtonState(true);
            AddOneListener(trueAction);
        }
        else
        {
            SetButtonState(false);
            AddTwoListener(trueAction, falseAcion);
        }
    }

    private void SetButtonState(bool isOne)
    {
        transform.Find(_onePath).gameObject.SetActive(isOne);
        transform.Find(_twoPath).gameObject.SetActive(!isOne);
    }

    private void AddOneListener(Action trueAction)
    {
        if (trueAction == null)
            transform.ButtonAction(_onePath + "/" + _yesBtn, UIManager.Single.Back);
        else
            transform.ButtonAction(_onePath + "/" + _yesBtn, trueAction);
    }

    private void AddTwoListener(Action trueAction, Action falseAcion)
    {
        transform.ButtonAction(_twoPath + "/" + _yesBtn, trueAction);
        transform.ButtonAction(_twoPath + "/" + _noBtn, falseAcion);
    }
}