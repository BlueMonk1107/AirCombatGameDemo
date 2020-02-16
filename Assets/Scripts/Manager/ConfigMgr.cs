using LitJson;

public class ConfigMgr : NormalSingleton<ConfigMgr>, IInit
{
    public void Init()
    {
        InitPlaneConfig();
    }

    private void InitPlaneConfig()
    {
        var config = ReaderMgr.Single.GetReader(Paths.CONFIG_INIT_PLANE_CONFIG);
        config["planes"].Get<JsonData>(data =>
        {
            foreach (JsonData item in data)
            {
                foreach (var key in item.Keys)
                {
                    if (key == "planeId")
                        continue;

                    var newKey = KeysUtil.GetPropertyKeys(int.Parse(item["planeId"].ToJson()), key);

                    if (!DataMgr.Single.Contains(newKey)) DataMgr.Single.SetJsonData(newKey, item[key]);
                }
            }
            
        });
    }
}