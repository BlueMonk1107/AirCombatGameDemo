public class SwitchPlayer : ViewBase
{
    protected override void InitChild()
    {
    }


    public override void Show()
    {
        base.Show();
        UpdateSprite();
    }

    public override void UpdateFun()
    {
        UpdateSprite();
    }


    private void UpdateSprite()
    {
        var id = GameStateModel.Single.SelectedPlaneId;
        var level = GameStateModel.Single.PlaneLevel;
        Util.Get("Icon").SetSprite(PlaneSpritesModel.Single[id, level]);
    }
}