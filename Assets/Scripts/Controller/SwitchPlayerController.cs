using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayerController : ControllerBase {

	private int _id;
	protected override void InitChild()
	{
		GameStateModel.Single.SelectedPlaneId = DataMgr.Single.Get<int>(DataKeys.PLANE_ID);
		_id = GameStateModel.Single.SelectedPlaneId;
		transform.ButtonAction("Left",()=>Switch(ref _id,-1));
		transform.ButtonAction("Right",()=>Switch(ref _id,1));
	}
	
	private void Switch(ref int id,int direction)
	{
		UpdateId(ref id, direction);
		GameStateModel.Single.SelectedPlaneId = id;
	}

	private void UpdateId(ref int id,int direction)
	{
		int min = 0;
		int max = PlaneSpritesModel.Single.Count;
		id += direction;
		id = id < 0 ? 0 : id >= max ? id = max - 1 : id;
	}
}
