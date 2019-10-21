using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthenView : ViewBase
{
	protected override void InitChild()
	{
		PlaneProperty property = Util.Get("Property").Go.AddComponent<PlaneProperty>();
		
		SwitchPlayer switchP = Util.Get("Switchplayer").Go.AddComponent<SwitchPlayer>();
		switchP.AddUpdateDataListener(property.UpdateData);
		
		Util.Get("Back").AddListener(UIManager.Single.Back);
	}
}
