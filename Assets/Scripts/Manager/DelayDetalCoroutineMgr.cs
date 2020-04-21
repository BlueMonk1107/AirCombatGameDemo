using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDetalCoroutineMgr : NormalSingleton<DelayDetalCoroutineMgr>
{

	private ObjectPool<DelayDetalCoroutineItem> _itemPool;
	private Dictionary<int, DelayDetalCoroutineItem> _items;

	public DelayDetalCoroutineMgr()
	{
		_items = new Dictionary<int, DelayDetalCoroutineItem>();
		_itemPool = new ObjectPool<DelayDetalCoroutineItem>();
		_itemPool.Init(3,true);
	}

	public int Start(float time,Action begin,Action complete,int id)
	{
		var item = GetItem(id);
		complete += ()=>End(item);
		item.Start(time,begin,complete);
		return item.ID;
	}

	private void End(DelayDetalCoroutineItem item)
	{
		_itemPool.Despawn(item);
	}

	private DelayDetalCoroutineItem GetItem(int id)
	{
		if (_items.ContainsKey(id))
		{
			if (_items[id] == null)
			{
				return SpawnNew();
			}
			else
			{
				return _items[id];
			}
		}
		else
		{
			return SpawnNew();
		}
	}

	private DelayDetalCoroutineItem SpawnNew()
	{
		var item = _itemPool.Spawn();
		_items[item.ID] = item;
		return item;
	}

	public bool IsRunning(int id)
	{
		if (_items.ContainsKey(id))
		{
			if (_items[id] == null)
			{
				_items.Remove(id);
				return false;
			}
			else
			{
				return _items[id].IsRunning;
			}
		}
		else
		{
			return false;
		}
	}
}
