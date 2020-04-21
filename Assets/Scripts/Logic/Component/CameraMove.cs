using System;
using UnityEngine;

public class CameraMove : MonoBehaviour, IUpdate
{
    private MoveComponent _move;

    private float _speed;
    
    // Use this for initialization
    private void Start()
    {
        _speed = 0;
        var reader = ReaderMgr.Single.GetReader(Paths.CONFIG_Game_CONFIG);
        reader["cameraSpeed"].Get<float>(value =>
        {
            _speed = value;
            _move = gameObject.AddComponent<MoveComponent>();
            _move.Init(_speed);
            
        });
    }

    private void OnDestroy()
    {
        Destroy(_move);
    }

    private void OnEnable()
    {
        LifeCycleMgr.Single.Add(LifeName.UPDATE, this);
    }

    private void OnDisable()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE, this);
    }

    public int Times { get; set; }

    public int UpdateTimes { get; }

    public void UpdateFun()
    {
        if(_move == null)
            return;
        
        _move.Move(Vector2.up);
    }

   
}