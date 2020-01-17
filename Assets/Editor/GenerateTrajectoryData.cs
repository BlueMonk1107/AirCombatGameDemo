using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
using UnityEditor;
using UnityEngine;

public class GenerateTrajectoryData : MonoBehaviour {

	[MenuItem("Tools/GenerateTrajectoryData")]
	private static void Execute()
	{
		EnemyTrajectoryDataMgr data =  new EnemyTrajectoryDataMgr();
		data.TrajectoryDatas = new Dictionary<TrajectoryType, ITrajectoryData[]>();
		data.TrajectoryDatas[TrajectoryType.Straight] = InitStraightData(data);
		string json = JsonUtil.DicConvertToJson(data.TrajectoryDatas);
		File.WriteAllText(Paths.CONFIG_ENEMY_TRAJECTORY,json);
		AssetDatabase.Refresh();
	}

	private static ITrajectoryData[] InitStraightData(EnemyTrajectoryDataMgr data)
	{
		List<ITrajectoryData> list = new List<ITrajectoryData>();
		list.Add(SetStraightData(-30f));
		list.Add(SetStraightData(0f));
		list.Add(SetStraightData(30f));
		return list.ToArray();
	}

	private static StraightTrajectoryData SetStraightData(float angle)
	{
		StraightTrajectoryData data = new StraightTrajectoryData();
		data.Angle = angle;
		return data;
	}
}
