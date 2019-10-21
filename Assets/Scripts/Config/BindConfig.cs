using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindConfig {

	public void Init()
	{
		BindUtil.Bind(Paths.START_VIEW,typeof(StartView));
		BindUtil.Bind(Paths.SELECTED_HERO_VIEW,typeof(SelectedHeroView));
		BindUtil.Bind(Paths.STRENGTHEN_VIEW,typeof(StrengthenView));
		BindUtil.Bind(Paths.LEVELS_VIEW,typeof(LevelsView));
	}
}
