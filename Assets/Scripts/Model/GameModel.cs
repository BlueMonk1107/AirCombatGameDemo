using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : NormalSingleton<GameModel> {

	public GameModel()
	{
		Life = Const.LIFE_MAX;
	}

	public int Life { get; set; }
	public int Score { get; set; }
	public int Stars { get; set; }
	public int ShieldCount { get; set; }
	public int BombCount { get; set; }

	/// <summary>
	/// 选中的关卡
	/// </summary>
	public int SelectedLevel { get; set; }

	public void Clear()
	{
		_single = new GameModel();
	}
}
