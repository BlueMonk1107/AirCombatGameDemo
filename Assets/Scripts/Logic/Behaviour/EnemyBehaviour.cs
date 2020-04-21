using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IBehaviour
{
    private LifeComponent _life;
    private EnemyData _data;

    public void Init(EnemyData data)
    {
        _data = data;
        _life = GetComponent<LifeComponent>();
        if (_life == null)
        {
            Debug.LogError("当前飞机没有添加生命值组件，物体名称:"+gameObject.name);
        }
    }

    public void Injure(int value)
    {
        if (_life.Life <= 0)
            return;

        var life = _life.Life - value;
        if (life <= 0)
        {
            _life.Life = 0;
            Dead();
        }
        else
        {
            _life.Life = life;
        }
    }

    public void Dead()
    {
        AniMgr.Single.PlaneDestroyAni(transform.position);
        GameModel.Single.Score += _data.score;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_SCORE);
        PoolMgr.Single.Despawn(gameObject);
        AudioMgr.Single.PlayOnce(GameAudio.Explode_Plane.ToString());

        SpawnStar();

        var typeC = GetComponent<EnemyTypeComponent>();
        if (typeC.Type == EnemyType.Boss)
        {
            MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_END_ONCE);
        }

        SpawnItem();
    }

    private void SpawnItem()
    {
        for (int i = 0; i < _data.itemCount; i++)
        {
            if (CanSpawn(_data.itemProbability))
            {
                var type = GetItemType(_data.itemRange);
                var view = ItemFactory.Single.GetItem(type);
                view.SetPos(transform.position);
            }
        }
    }

    private bool CanSpawn(int ratio)
    {
        int index = Random.Range(0, 101);
        return index < ratio;
    }

    private ItemType GetItemType(ItemType[] itemRange)
    {
        if (itemRange.Length == 2)
        {
            
            int index = Random.Range((int)itemRange[0], (int)itemRange[1]+1);
            return (ItemType) index;
        }
        else
        {
            return ItemType.ADD_BULLET;
        }
    }

    private void SpawnStar()
    {
        for (int i = 0; i < _data.starNum; i++)
        {
            var star = StarView.GetObject();
            star.SetPos(transform.position);
        }
    }
}