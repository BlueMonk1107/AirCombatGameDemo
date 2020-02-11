using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class JsonUtil  
{
    public static string DicConvertToJson(Dictionary<PathType,ITrajectoryData[]> dic)
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

    public static Dictionary<PathType, ITrajectoryData[]> JsonConvertToDic(string json)
    {
        Dictionary<PathType, ITrajectoryData[]> dic = new Dictionary<PathType, ITrajectoryData[]>();
        JsonData data = JsonMapper.ToObject(json);
        for (PathType type = PathType.Straight ; type < PathType.COUNT; type++)
        {
            if (data.Keys.Contains(type.ToString()))
            {
                dic[type] = GetArray(type,data);
            }
        }

        return dic;
    }

    private static ITrajectoryData[] GetArray(PathType type,JsonData data)
    {
        switch (type)
        {
            case PathType.Straight:
                return JsonMapper.ToObject<StraightTrajectoryData[]>(data[type.ToString()].ToJson());
            case PathType.W:
                return JsonMapper.ToObject<VTrajectoryData[]>(data[type.ToString()].ToJson());
            case PathType.Ellipse:
                return JsonMapper.ToObject<EllipseData[]>(data[type.ToString()].ToJson());
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
