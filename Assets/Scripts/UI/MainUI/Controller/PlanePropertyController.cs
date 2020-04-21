public class PlanePropertyController : ControllerBase
{
    protected override void InitChild()
    {
        AddComponent();
    }

    private void AddComponent()
    {
        for (PlaneProperty.Property i = 0; i < PlaneProperty.Property.COUNT; i++)
        {
            var item = transform.GetChild((int) i).gameObject.AddComponent<PropertyItemController>();
            item.Init(i.ToString());
        }
    }
}