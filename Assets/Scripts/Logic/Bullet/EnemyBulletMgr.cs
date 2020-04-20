using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMgr : MonoBehaviour
{
    private List<EmitBulletMgr> _emitBulletMgrs = new List<EmitBulletMgr>();

    public void Init(EnemyData data)
    {
        if (_emitBulletMgrs.Capacity < data.bulletType.Length)
            _emitBulletMgrs.Capacity = data.bulletType.Length;
        JudgeCount(data.bulletType.Length);

        for (int i = 0; i < data.bulletType.Length; i++)
        {
            _emitBulletMgrs[i].Init(data.bulletType[i], data);
        }
    }

    private void JudgeCount(int count)
    {
        if (_emitBulletMgrs.Count < count)
        {
            for (int i = 0; i < count - _emitBulletMgrs.Count; i++)
            {
                _emitBulletMgrs.Add(gameObject.AddComponent<EmitBulletMgr>());
            }
        }
    }
}