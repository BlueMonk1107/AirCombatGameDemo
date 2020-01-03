using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class JsonUtil  
{
    public static string DicConvertToJson(Dictionary<TrajectoryType,StraightTrajectoryData[]> dic)
    {
        JsonData jsonData = new JsonData();
        foreach (var datase in dic)
        {
            jsonData[datase.Key.ToString()] = new JsonData();
            foreach (StraightTrajectoryData data in datase.Value)
            {
                jsonData[datase.Key.ToString()].Add(JsonMapper.ToObject(JsonMapper.ToJson(data)));
            }
        }

        return jsonData.ToJson();
    }

    public static Dictionary<TrajectoryType, StraightTrajectoryData[]> JsonConvertToDic(string json)
    {
        Dictionary<TrajectoryType, StraightTrajectoryData[]> dic = new Dictionary<TrajectoryType, StraightTrajectoryData[]>();
        JsonData data = JsonMapper.ToObject(json);
        for (TrajectoryType type = TrajectoryType.Straight ; type < TrajectoryType.COUNT; type++)
        {
            if (data.Keys.Contains(type.ToString()))
            {
                var array = JsonMapper.ToObject<StraightTrajectoryData[]>(data[type.ToString()].ToJson());
                dic[type] = array;
            }
        }

        return dic;
    }
}
