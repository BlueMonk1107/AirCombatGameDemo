using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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

    public static void ShowWarnning()
    {
        var go = LoadMgr.Single.LoadPrefabAndInstantiate(Paths.PREFAB_WARNING,UIManager.Single.Canvas.transform);
        go.AddComponent<Warning>();
    }

    public static int GetInt(object value)
    {
        if (value.GetType() == typeof(int))
        {
            return (int) value;
        }
        else
        {
            Debug.LogError("当前值不是int类型，值为："+value);
            return 0;
        }
    }
    
    
    public static List<IEnemyCreater> InitCreater(
        EnemyType type,
        Transform parent,
        AllEnemyData enemyData, 
        EnemyTrajectoryDataMgr trajectoryData, 
        LevelData levelData)
    {
        List<IEnemyCreater> list = new List<IEnemyCreater>();
        foreach (var createrData in levelData.PlaneCreaterDatas)
        {
            if(createrData.Type == type)
                list.Add(SpawnCreater(parent,createrData,enemyData,trajectoryData));
        }

        return list;
    }

    private static IEnemyCreater SpawnCreater(
        Transform              parent,
        PlaneCreaterData       data,
        AllEnemyData           enemyData,
        EnemyTrajectoryDataMgr trajectoryData)
    {
        var go = new GameObject("PlaneEnemyCreater");
        var creater = go.AddComponent<PlaneEnemyCreater>();
        creater.Init(data, enemyData, trajectoryData);
        go.transform.SetParent(parent);
        return creater;
    }

    public static GameProcessNormalEvent GetNormalEvent(Action spawnAction,Func<int> spawnedNum,int spawnTotalNum)
    {
        GameProcessNormalEvent e = new GameProcessNormalEvent();
        e.AddEvent(spawnAction,spawnedNum,spawnTotalNum);
        return e;
    }

    public static GameProcessTriggerEvent GetTriggerEvent(float ratio,Action action,bool needPauseProcess,Func<bool> isEnd)
    {
        GameProcessTriggerEvent e = new GameProcessTriggerEvent();
        e.AddEvent(ratio,action,needPauseProcess,isEnd);
        return e;
    }
}