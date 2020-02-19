using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialStateMgrBase : MonoBehaviour
{
	private Dictionary<BuffType,IBuff> _buffs;
	private Dictionary<DebuffType,IDebuff> _debuffs;
	private HashSet<BuffType> _canBuffs;
	private HashSet<DebuffType> _canDebuffs;
	
	protected abstract HashSet<BuffType> GetCanBuffs();
	protected abstract HashSet<DebuffType> GetCanDebuffs();
	protected SubMsgMgr _msgMgr;
	
	void Start ()
	{
		_buffs = new Dictionary<BuffType, IBuff>();
		_debuffs = new Dictionary<DebuffType, IDebuff>();
		_msgMgr = GameUtil.GetSubMsgMgr(transform);
		_canBuffs = GetCanBuffs();
		_canDebuffs = GetCanDebuffs();
		AddListener();
	}

	private void OnDestroy()
	{
		RemoveListener();
	}

	protected virtual void AddListener()
	{
		_msgMgr.AddListener(MsgEvent.EVENT_BUFF,Buff);
		_msgMgr.AddListener(MsgEvent.EVENT_DEBUFF,Debuff);
	}

	protected virtual void RemoveListener()
	{
		_msgMgr.RemoveListener(MsgEvent.EVENT_BUFF,Buff);
		_msgMgr.RemoveListener(MsgEvent.EVENT_DEBUFF,Debuff);
	}

	private void Buff(object[] paras)
	{
		BuffType type = (BuffType) paras[0];
		ExecuteBuff(type);
	}
	
	private void Debuff(object[] paras)
	{
		DebuffType type = (DebuffType) paras[0];
		ExecuteDebuff(type);
	}

	protected virtual void ExecuteBuff(BuffType type)
	{
		if (_canBuffs.Contains(type))
		{
			var buff = GetBuffObject(type);
			if(buff != null)
				buff.Start(_msgMgr);
		}
	}

	private IBuff GetBuffObject(BuffType type)
	{
		if (_buffs.ContainsKey(type))
		{
			return _buffs[type];
		}
		else
		{
			IBuff buff = SpecialStateFactory.GetBuff(type);
			_buffs.Add(type,buff);
			return buff;
		}
	}
	
	private IDebuff GetDebuffObject(DebuffType type)
	{
		if (_debuffs.ContainsKey(type))
		{
			return _debuffs[type];
		}
		else
		{
			IDebuff debuff = SpecialStateFactory.GetDebuff(type);
			_debuffs.Add(type,debuff);
			return debuff;
		}
	}
	
	protected virtual void ExecuteDebuff(DebuffType type)
	{
		if (_canDebuffs.Contains(type))
		{
			var debuff = GetDebuffObject(type);
			if(debuff != null)
				debuff.Start(_msgMgr);
		}
	}
}
