using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCreaterMgr : ISubEnemyCreaterMgr, IGameProcessTriggerEvent
{
    private Dictionary<int, List<MissileEnemyCreater>> _createrDatas;
    private int _curPatch;
    
    public void Init()
    {
        _createrDatas = new Dictionary<int, List<MissileEnemyCreater>>();
        _curPatch = -1;
    }

    public void InitCreater(Transform parent, AllEnemyData enemyData, EnemyTrajectoryDataMgr trajectoryData, LevelData levelData)
    {
        foreach (MissileCreaterData createrData in levelData.MissileCreaterDatas)
        {
            int batch = createrData.Batch;
            if(!_createrDatas.ContainsKey(batch))
                _createrDatas[batch] = new List<MissileEnemyCreater>();
            
            GameObject createrGo = new GameObject("MissileCreaterMgr");
            var creater = createrGo.AddComponent<MissileEnemyCreater>();
            creater.Init(createrData,enemyData,trajectoryData);
            creater.transform.SetParent(parent);
            _createrDatas[batch].Add(creater);
        }
    }

    public void Spawn()
    {
        if (_curPatch < 0 || JudgeCurrentPatchEnd())
        {
            _curPatch++;
            if (_createrDatas.ContainsKey(_curPatch))
            {
                foreach (MissileEnemyCreater creater in _createrDatas[_curPatch])
                {
                    creater.Spawn();
                }
            }
        }
    }

    private bool JudgeCurrentPatchEnd()
    {
        if (_createrDatas.ContainsKey(_curPatch))
        {
            foreach (MissileEnemyCreater creater in _createrDatas[_curPatch])
            {
                if (!creater.IsEnd())
                    return false;
            }

            return true;
        }
        else
        {
            return true;
        }
    }

    public List<GameProcessTriggerEvent> GetTriggerEvents()
    {
        List<GameProcessTriggerEvent> list = new List<GameProcessTriggerEvent>();
        list.Add(GameUtil.GetTriggerEvent(0.5f,Spawn,true,JudgeCurrentPatchEnd));
        return list;
    }
}
