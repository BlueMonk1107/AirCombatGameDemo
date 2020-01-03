using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
using UnityEditor;

public class GenerateLevelConfig
{
    [MenuItem("Tools/GenerateLevelConfig")]
    private static void Execute()
    {
        var data = new EnemyCreaterConfigData();
        data.LevelDatas = new LevelData[2];
        LevelOne(data);
        LevelTwo(data);
        var json = JsonMapper.ToJson(data);
        File.WriteAllText(Paths.CONFIG_LEVEL_ENEMY_DATA, json);
        AssetDatabase.Refresh();
    }

    private static void LevelOne(EnemyCreaterConfigData data)
    {
        var level = new LevelData();
        data.LevelDatas[0] = level;
        level.EnemyNumMax = 50;
        level.EnemyNumMin = 40;
        level.CreaterDatas = GetCreaterListOne();
    }

    private static CreaterData[] GetCreaterListOne()
    {
        List<CreaterData> list = new List<CreaterData>();
        list.Add(GetCreaterData(0,1,4,5,EnemyType.Normal,0));
        return list.ToArray();
    }

    private static void LevelTwo(EnemyCreaterConfigData data)
    {
        var level = new LevelData();
        data.LevelDatas[1] = level;
        level.EnemyNumMax = 50;
        level.EnemyNumMin = 40;
        level.CreaterDatas = GetCreaterListOne();
    }
    
    private static CreaterData GetCreaterData(int idMin,int idMax,int queueNum,int queuePlaneNum,EnemyType type,double x)
    {
        var createrData = new CreaterData();
        createrData.IdMin = idMin;
        createrData.IdMax = idMax;
        createrData.QueueNum = queueNum;
        createrData.QueuePlaneNum = queuePlaneNum;
        createrData.Type = type;
        createrData.X = x;
        return createrData;
    }
}