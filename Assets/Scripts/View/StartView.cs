using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BindPrefab(Paths.START_VIEW)]
public class StartView : ViewBase {

	protected override void InitChild()
	{
		Util.Get("Start").AddListener(() =>
		{
			UIManager.Single.Show(Paths.SELECTED_HERO_VIEW);
		});
	}
}
