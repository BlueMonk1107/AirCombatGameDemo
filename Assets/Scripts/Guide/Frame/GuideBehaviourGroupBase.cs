using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuideGroup : IGuide
{
}

public abstract class GuideGroupBase<T> : IGuideGroup where T : IGuide
{
	private Queue<T> _guideItems;
	private Action _complete;
	protected abstract bool IsTrigger { get; }
	protected abstract int GroupId { get; }
	/// <summary>
	/// 数据持久化的键值
	/// </summary>
	protected int _dataId;

	/// <summary>
	/// 是否是第一次执行引导逻辑
	/// </summary>
	private bool _firstExecute;

	public GuideGroupBase(int parentId)
	{
		_dataId = GetDataId(parentId);
		_dataId = parentId < 0 ? parentId : _dataId;
	}

	private int GetDataId(int parentId)
	{
		return parentId << 8 + GroupId;
	}
	
	public void OnEnter(Action callBack)
	{
		if(GuideDataMgr.Single.GetData(_dataId))
			return;
		
		_firstExecute = true;
		_complete = callBack;
		_guideItems = GetGuideItems();
	}

	private void SaveData()
	{
		if(_dataId < 0)
			return;
		
		GuideDataMgr.Single.SaveData(_dataId);
	}
	
	public virtual void Update()
	{
		if (IsTrigger && _firstExecute)
		{
			_firstExecute = false;
			ExecuteGuideItem();
		}
	}

	protected abstract Queue<T> GetGuideItems();

	private bool ExecuteGuideItem()
	{
		if (_guideItems.Count > 0)
		{
			T behaviour = _guideItems.Dequeue();
			behaviour.OnEnter(End);
			return true;
		}
		else
		{
			return false;
		}
	}

	private void End()
	{
		if (!ExecuteGuideItem())
		{
			SaveData();
			//todo:引导结束后要执行的逻辑
			if (_complete != null)
				_complete();
		}
	}
}

/// <summary>
/// 动作组基类
/// </summary>
public abstract class GuideBehaviourGroupBase : GuideGroupBase<IGuideBehaviour> 
{
	protected GuideBehaviourGroupBase(int parentId) : base(parentId)
	{
	}
}

public abstract class GuideGroupGroupBase : GuideGroupBase<IGuideGroup>
{
	protected GuideGroupGroupBase(int parentId) : base(parentId)
	{
	}
}