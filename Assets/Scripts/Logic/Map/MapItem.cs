using UnityEngine;

public class MapItem : MonoBehaviour, IUpdate
{
    private Transform _camera;
    private float _offset;
    private SpriteRenderer _renderer;
    private MapCloud _cloud;
    private static int _currentLevel;

    public int Times { get; set; }

    public int UpdateTimes { get; }

    public void UpdateFun()
    {
        if (JugdeUpdate(_offset, _camera))
        {
            UpdatePos(_offset);
            UpdateSprite();
            UpdateLevel();
        }
    }

    public void Init(float offset, Transform camera)
    {
        _offset = offset;
        _camera = camera;
        _renderer = GetComponent<SpriteRenderer>();
        LifeCycleMgr.Single.Add(LifeName.UPDATE, this);
        _currentLevel = GameModel.Single.SelectedLevel;
        SetSprite(_currentLevel);
        _cloud = transform.GetChild(0).AddComponent<MapCloud>();
    }

    private bool JugdeUpdate(float offset, Transform camera)
    {
        return camera.position.y - transform.position.y >= offset;
    }

    private void UpdateLevel()
    {
        bool isActive = _currentLevel != GameModel.Single.CurrentLevel;
        _cloud.SetActive(isActive);
        if (isActive)
        {
            _currentLevel = GameModel.Single.CurrentLevel;
        }
    }

    private void UpdatePos(float offset)
    {
        var pos = transform.position;
        pos.y += offset * 2;
        transform.position = pos;
    }

    private void UpdateSprite()
    {
        SetSprite(GameModel.Single.CurrentLevel);
    }
    
    private void SetSprite(int level)
    {
        var name = Const.MAP_PREFIX + level;
        var sprite = LoadMgr.Single.Load<Sprite>(Paths.PICTURE_MAP_FOLDER + name);
        if (sprite == null) 
            sprite = LoadMgr.Single.Load<Sprite>(Paths.PICTURE_MAP_FOLDER + Const.MAP_PREFIX + 0);

        _renderer.sprite = sprite;
    }

    private void OnDestroy()
    {
        LifeCycleMgr.Single.Remove(LifeName.UPDATE, this);
    }
}