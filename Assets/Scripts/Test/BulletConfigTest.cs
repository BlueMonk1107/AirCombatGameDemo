using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class BulletConfigTest : ITest
{
    private string _trajectoryKey = "trajectory"; 
    public IEnumerator Execute()
    {
        bool complete = false;
        LoadMgr.Single.LoadConfig(Paths.CONFIG_BULLET_CONFIG, (value) =>
        {
            bool change = false;
            string json = (string) value;
            JsonData data = JsonMapper.ToObject(json);

            foreach (string key in data.Keys)
            {
                if(key == "Player")
                    continue;
                change = ChangeValue(data[key]);

                foreach (string sKey in data[key].Keys)
                {
                    if (sKey == "Events")
                    {
                        foreach (JsonData jsonData in data[key][sKey])
                        {
                            if (jsonData["Data"].Keys.Contains("trajectory"))
                            {
                                if (ChangeValue(jsonData["Data"]))
                                {
                                    change = true;
                                }
                            }
                        }
                    }
                }
            }

            if (change)
            {
                File.WriteAllText(Paths.CONFIG_BULLET_CONFIG,data.ToJson());
            }
            complete = true;
        });

        while (!complete)
        {
            yield return null;
        }
    }

    private bool ChangeValue(JsonData data)
    {
        if (!data[_trajectoryKey][0].IsArray)
            return false;
        bool change = false;
        string json = data.ToJson();
        TempTrajectoryData temp = JsonMapper.ToObject<TempTrajectoryData>(json);
        foreach (int[] array in temp.trajectory)
        {
            for (int j = 0; j < array.Length; j++)
            {
                if (array[j] > 0)
                {
                    array[j] = -array[j];
                    change = true;
                }
            }
        }

        if (change)
        {
            data[_trajectoryKey].Clear();
            for (int i = 0; i < temp.trajectory.Length; i++)
            {
                data[_trajectoryKey].Add(i);
                data[_trajectoryKey][i] = new JsonData();
                for (int j = 0; j < temp.trajectory[i].Length; j++)
                {
                    data[_trajectoryKey][i].Add(j);
                    data[_trajectoryKey][i][j] = temp.trajectory[i][j];
                }
            }
        }

        return change;
    }
}

public class TempTrajectoryData
{
    public int[][] trajectory;
}
