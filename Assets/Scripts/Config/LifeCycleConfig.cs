using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycleConfig  {
	
	public static Dictionary<LifeName,ILifeCycle> LifeCycles = new Dictionary<LifeName,ILifeCycle>
	{
		{LifeName.INIT,new LifeCycle<IInit>()},
		{LifeName.UPDATE,new LifeCycle<IUpdate>()},
	};
    
	public static Dictionary<LifeName,Action> LifeCycleFuns = new Dictionary<LifeName,Action>
	{
		{LifeName.INIT, ()=>LifeCycles[LifeName.INIT].Execute((IInit o)=>o.Init())},
		{LifeName.UPDATE,()=>LifeCycles[LifeName.UPDATE].Execute((IUpdate o)=>o.UpdateFun())},
	};
	
}

public enum LifeName
{
	INIT,
	UPDATE
}
