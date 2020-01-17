using UnityEngine;

public class GameUtil
{
    private static Vector2 _cameraSize = Vector2.zero;

    private static Camera _camera;

    public static void ChangeHandPos(RectTransform rect)
    {
        var value = Mathf.Abs(rect.anchoredPosition.x);
        if (GameStateModel.Single.HandMode == HandMode.LEFT)
            SetPosData(rect, Vector2.zero, value);
        else
            SetPosData(rect, Vector2.right, -value);
    }

    private static void SetPosData(RectTransform rect, Vector2 anchorValue, float x)
    {
        rect.anchorMin = anchorValue;
        rect.anchorMax = anchorValue;
        var pos = rect.anchoredPosition;
        pos.x = x;
        rect.anchoredPosition = pos;
    }

    public static Vector2 GetCameraSize()
    {
        if (_cameraSize == Vector2.zero)
        {
            var camera = Object.FindObjectOfType<Camera>();
            var heigth = camera.orthographicSize * 2;
            var width = heigth * camera.aspect;
            _cameraSize = new Vector2(width, heigth);
        }

        return _cameraSize;
    }

    public static Vector2 GetCameraMin()
    {
        if (GetCamera() != null)
        {
            var pos = GetCamera().transform.position;
            var size = GetCameraSize();
            return new Vector3(pos.x - size.x * 0.5f, pos.y - size.y * 0.5f, pos.z);
        }

        return Vector2.zero;
    }

    public static Vector2 GetCameraMax()
    {
        if (GetCamera() != null)
        {
            var pos = GetCamera().transform.position;
            var size = GetCameraSize();
            return new Vector3(pos.x + size.x * 0.5f, pos.y + size.y * 0.5f, pos.z);
        }

        return Vector2.zero;
    }

    public static Camera GetCamera()
    {
        if (_camera == null)
        {
            _camera = Object.FindObjectOfType<Camera>();
            if (_camera == null)
                Debug.LogError("当前场景中没有相机");

            return _camera;
        }
        else
        {
            return _camera;
        }
        
       
    }

    public static SubMsgMgr GetSubMsgMgr(Transform trans)
    {
        var msg = trans.GetComponentInParent<SubMsgMgr>();
        if (msg == null)
        {
            var root = trans.GetComponentInParent<IGameRoot>();
            msg = root.Self.AddComponent<SubMsgMgr>();
        }
        return msg;
    }
}