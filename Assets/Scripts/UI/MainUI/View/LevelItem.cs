using UnityEngine;

public class LevelItem : ViewBase
{
    private int _id;
    private readonly int _leftOffet = 50;
    private readonly int _lineSpacing = 10;
    private readonly int _offset = 10;

    protected override void InitChild()
    {
        _id = transform.GetSiblingIndex();
        SetLevelId();
        var isOpen = JudgeOpenState();
        SetMaskState(isOpen);
        InitPos();
    }

    private void InitPos()
    {
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_LEVEL_CONFIG);
        reader["eachRow"].Get<int>(data =>
        {
            var grid = GetGrid(data);
            SetPos(grid);
        });
    }

    private bool JudgeOpenState()
    {
        var passed = -1;
        if (DataMgr.Single.Contains(DataKeys.LEVEL_PASSED)) passed = DataMgr.Single.Get<int>(DataKeys.LEVEL_PASSED);

        return _id <= passed + 1;
    }

    private void SetMaskState(bool isOpen)
    {
        Util.Get("Mask").Go.SetActive(!isOpen);
    }

    private void SetLevelId()
    {
        Util.Get("Enter/Text").SetText(_id + 1);
    }

    private Vector2 GetGrid(int eachRow)
    {
        var y = _id / eachRow;
        var x = _id % eachRow;
        return new Vector2(x, y);
    }

    private void SetPos(Vector2 gridId)
    {
        var width = transform.Rect().rect.width * transform.localScale.x;
        var height = transform.Rect().rect.height * transform.localScale.y;

        var indention = gridId.y % 2 == 0 ? _leftOffet : 0;

        var x = indention + width * 0.5f + (_offset + width) * gridId.x;
        var y = height * 0.5f + (_lineSpacing + height) * gridId.y;
        transform.Rect().anchoredPosition = new Vector2(x, -y);
    }
}