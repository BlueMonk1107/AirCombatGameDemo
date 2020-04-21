using System.Collections;

public class LifeCycleAddConfig : IInit
{
    public ArrayList Objects { get; private set; }

    public void Init()
    {
        Objects = new ArrayList();
        Add();
    }

    private void Add()
    {
        Objects.Add(UILayerMgr.Single);
        Objects.Add(ConfigMgr.Single);
        Objects.Add(new InitCustomAttributes());
        Objects.Add(PlaneSpritesModel.Single);
        Objects.Add(AudioMgr.Single);
        Objects.Add(SceneConfig.Single);
        Objects.Add(GameDataMgr.Single);
        Objects.Add(BulletEffectPoolConfig.Single);
    }
}