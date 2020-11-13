using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighLightModule : Graphic, ICanvasRaycastFilter
{
    public bool CanClick;
    private Vector2[] _outer = new Vector2[4];
    private Vector2[] _inner = new Vector2[4];
    [SerializeField]
    public RectTransform[] TargetUI { get; private set; }
    private Vector2 _targetLocalPos;
    private Vector3 _lastTargetPos;
    private Vector2 _lastTargetSize;
    private Vector3 _targetPos;
    private Vector2 _targetSize;
    [SerializeField]
    public Camera CurrentCamera { get; private set; }
    public bool IsValid { get; set; }
    public bool IsInValidRange { get; private set; }
    private Button _selfButton;

    private Button SelfButton
    {
        get
        {
            if (_selfButton == null)
            {
                _selfButton = GetComponent<Button>();

                if (_selfButton == null)
                {
                    _selfButton = gameObject.AddComponent<Button>();
                }
            }

            return _selfButton;
        }
    }

    private float _leftX, _rightX, _bottomY, _upY;

    public void Init(RectTransform targetUI, Camera currentCamera, Action clickAction)
    {
        Init(new[] {targetUI}, currentCamera, clickAction);
    }

    public void Init(RectTransform[] targetsUI, Camera currentCamera, Action clickAction)
    {
        TargetUI = targetsUI;
        CurrentCamera = currentCamera;
        color = new Color32(0, 0, 0, 190);
        SelfButton.onClick.RemoveAllListeners();
        AddButtonListener(clickAction);
    }

    public void AddButtonListener(Action clickAction)
    {
        SelfButton.onClick.AddListener(() => clickAction());
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentCamera == null || !JudgeTargetValid())
        {
            if (IsValid)
            {
                SetAllDirty();
            }

            IsValid = false;
            return;
        }

        if (!IsValid)
        {
            IsValid = true;
            SetAllDirty();
        }

        GetValidRectData();
        JudgeChange();
    }

    private bool JudgeTargetValid()
    {
        if (TargetUI == null || TargetUI.Length == 0)
            return false;

        bool valid = false;
        foreach (RectTransform trans in TargetUI)
        {
            if (trans.gameObject.activeSelf)
            {
                valid = true;
                break;
            }
        }

        return valid;
    }

    private void JudgeChange()
    {
        if (_lastTargetPos != _targetPos || _lastTargetSize != _targetSize)
        {
            _lastTargetPos = _targetPos;
            _lastTargetSize = _targetSize;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                RectTransformUtility.WorldToScreenPoint(CurrentCamera, _lastTargetPos),
                CurrentCamera,
                out _targetLocalPos
            );
            SetAllDirty();
        }
    }

    private void GetValidRectData()
    {
        List<float> pointsX = new List<float>();
        List<float> pointsY = new List<float>();
        Tuple<float[], float[]> tuple = null;
        foreach (RectTransform rect in TargetUI)
        {
            tuple = GetPoints(rect);
            pointsX.AddRange(tuple.Item1);
            pointsY.AddRange(tuple.Item2);
        }

        _leftX = Mathf.Min(pointsX.ToArray());
        _rightX = Mathf.Max(pointsX.ToArray());
        _bottomY = Mathf.Min(pointsY.ToArray());
        _upY = Mathf.Max(pointsY.ToArray());

        _targetSize = new Vector2(_rightX - _leftX, _upY - _bottomY);
        _targetPos = new Vector3(_leftX + _targetSize.x / 2, _bottomY + _targetSize.y / 2);
    }

    private Tuple<float[], float[]> GetPoints(RectTransform rect)
    {
        float[] tempX = new float[2];
        float[] tempY = new float[2];
        float width = rect.rect.width * rect.lossyScale.x;
        float heigh = rect.rect.height * rect.lossyScale.y;
        tempX[0] = rect.position.x - width / 2;
        tempX[1] = rect.position.x + width / 2;
        tempY[0] = rect.position.y - heigh / 2;
        tempY[1] = rect.position.y + heigh / 2;
        return new Tuple<float[], float[]>(tempX, tempY);
        ;
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (!CanClick || !JudgeTargetValid())
            return true;

        IsInValidRange = false;
        IsInValidRange = JudgePointValid(sp);
        Debug.Log(IsInValidRange);
        return !IsInValidRange;
    }

    private bool JudgePointValid(Vector3 point)
    {
        return point.x >= _leftX
               && point.x <= _rightX
               && point.y >= _bottomY
               && point.y <= _upY;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (!IsValid)
            return;

        UpdateVertexData();

        AddVert(vh, _outer);
        AddVert(vh, _inner);

        int count = 4;

        for (int i = 0; i < count; i++)
        {
            AddTriangles(vh, i, count);
        }
    }

    private void AddVert(VertexHelper vh, Vector2[] vers)
    {
        for (int i = 0; i < vers.Length; i++)
        {
            UIVertex vertex = new UIVertex();
            vertex.position = vers[i];
            vertex.color = color;
            vh.AddVert(vertex);
        }
    }

    private void AddTriangles(VertexHelper vh, int startIndex, int count)
    {
        int outerEnd = GetVaildIndex(startIndex + 1, count);
        int innerEnd = GetVaildIndex(startIndex + count + 1, count);
        vh.AddTriangle(startIndex, outerEnd, startIndex + count);
        vh.AddTriangle(innerEnd, startIndex + count, outerEnd);
    }

    private int GetVaildIndex(int current, int count)
    {
        if (current % count != 0)
            return current;

        return (current / count - 1) * count + current % count;
    }

    private void UpdateVertexData()
    {
        float outerLeftX = -rectTransform.pivot.x * rectTransform.rect.width;
        float outerRightX = (1 - rectTransform.pivot.x) * rectTransform.rect.width;
        float outerBottomY = -rectTransform.pivot.y * rectTransform.rect.height;
        float outerUpY = (1 - rectTransform.pivot.y) * rectTransform.rect.height;

        _outer[0] = new Vector2(outerLeftX, outerBottomY);
        _outer[1] = new Vector2(outerLeftX, outerUpY);
        _outer[2] = new Vector2(outerRightX, outerUpY);
        _outer[3] = new Vector2(outerRightX, outerBottomY);

        float width = _targetSize.x;
        float heigh = _targetSize.y;

        float innerLeftX = _targetLocalPos.x - width / 2;
        float innerRightX = _targetLocalPos.x + width / 2;
        float innerBottomY = _targetLocalPos.y - heigh / 2;
        float innerUpY = _targetLocalPos.y + heigh / 2;

        _inner[0] = new Vector2(innerLeftX, innerBottomY);
        _inner[1] = new Vector2(innerLeftX, innerUpY);
        _inner[2] = new Vector2(innerRightX, innerUpY);
        _inner[3] = new Vector2(innerRightX, innerBottomY);
    }
}