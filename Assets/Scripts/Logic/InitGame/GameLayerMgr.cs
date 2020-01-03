using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLayerMgr : MonoBehaviour
{
    private static GameLayerMgr _single;
    private Dictionary<GameLayer, Transform> _gameLayers;

    public static GameLayerMgr Single
    {
        get
        {
            if (SceneMgr.Single.IsScene(SceneName.Game))
            {
                return _single;
            }

            var scene = GameStateModel.Single.CurrentScene;
            Debug.LogError("GameLayerMgr只能在Game场景下运行，当前场景：" + scene);
            return null;
        }
    }

    public void Init()
    {
        _single = this;
        _gameLayers = new Dictionary<GameLayer, Transform>();

        foreach (GameLayer layer in Enum.GetValues(typeof(GameLayer)))
        {
            var layerGo = new GameObject(layer.ToString());
            layerGo.transform.SetParent(transform);
            layerGo.transform.position = Vector3.forward * (int) layer;
            _gameLayers.Add(layer, layerGo.transform);
        }
    }

    public void SetLayerParent(IGameView view)
    {
        if (_gameLayers.ContainsKey(view.Layer))
            view.Self.SetParent(_gameLayers[view.Layer]);
        else
            Debug.LogError("当前层级并不存在，layer:" + view.Layer);
    }
}