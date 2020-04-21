using UnityEngine;
using UnityEngine.UI;

public class PropertyItem : MonoBehaviour, IViewUpdate, IViewShow
{
    public enum ItemKey
    {
        name,
        value,
        cost,
        grouth,
        maxVaue
    }

    private static int _itemId = -1;
    private string _key;

    public void Show()
    {
        UpdatePlaneId(GameStateModel.Single.SelectedPlaneId);
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

    public void UpdateFun()
    {
        UpdatePlaneId(GameStateModel.Single.SelectedPlaneId);
    }

    public void Init(string key)
    {
        _key = key;
        _itemId++;
        UpdatePos(_itemId);
    }


    private void UpdatePos(int itemId)
    {
        var rect = transform.Rect();
        var offset = rect.rect.height * itemId;
        rect.anchoredPosition -= offset * Vector2.up;
    }

    private void UpdatePlaneId(int planeId)
    {
        UpdateData(planeId);
        UpdateSlider();
    }

    private void UpdateData(int planeId)
    {
        for (ItemKey i = 0; i < ItemKey.grouth; i++)
        {
            var trans = transform.Find(ConvertName(i));
            if (trans != null)
            {
                var key = KeysUtil.GetPropertyKeys(_key + i);
                trans.GetComponent<Text>().text = DataMgr.Single.GetObject(key).ToString();
            }
            else
            {
                Debug.LogError("当前预制名称错误，正确名称：" + ConvertName(i));
            }
        }
    }

    private void UpdateSlider()
    {
        var slider = transform.Find("Slider").GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = DataMgr.Single.Get<int>(KeysUtil.GetNewKey(ItemKey.maxVaue, _key));
        slider.value = DataMgr.Single.Get<int>(KeysUtil.GetNewKey(ItemKey.value, _key));
    }

    private string ConvertName(ItemKey key)
    {
        var first = key.ToString().Substring(0, 1).ToUpper();
        var others = key.ToString().Substring(1);
        return first + others;
    }
}