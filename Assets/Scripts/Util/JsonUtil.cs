using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class JsonUtil  
{
    public static string DicConvertToJson(Dictionary<TrajectoryType,ITrajectoryData[]> dic)
    {
        JsonData jsonData = new JsonData();
        foreach (var datase in dic)
        {
            jsonData[datase.Key.ToString()] = new JsonData();
            foreach (ITrajectoryData data in datase.Value)
            {
                jsonData[datase.Key.ToString()].Add(JsonMapper.ToObject(JsonMapper.ToJson(data)));
            }
        }

        return jsonData.ToJson();
    }

    public static Dictionary<TrajectoryType, ITrajectoryData[]> JsonConvertToDic(string json)
    {
        Dictionary<TrajectoryType, ITrajectoryData[]> dic = new Dictionary<TrajectoryType, ITrajectoryData[]>();
        JsonData data = JsonMapper.ToObject(json);
        for (TrajectoryType type = TrajectoryType.Straight ; type < TrajectoryType.COUNT; type++)
        {
            if (data.Keys.Contains(type.ToString()))
            {
                dic[type] = GetArray(type,data);
            }
        }

        return dic;
    }

    private static ITrajectoryData[] GetArray(TrajectoryType type,JsonData data)
    {
        switch (type)
        {
            case TrajectoryType.Straight:
                return JsonMapper.ToObject<StraightTrajectoryData[]>(data[type.ToString()].ToJson());
            case TrajectoryType.W:
                return JsonMapper.ToObject<VTrajectoryData[]>(data[type.ToString()].ToJson());
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
