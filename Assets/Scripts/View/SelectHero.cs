using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHero : MonoBehaviour
{
	private List<HeroItem> _items;
	// Use this for initialization
	void Start () {
		_items = new List<HeroItem>();
		HeroItem item = null;
		foreach (Transform trans in transform)
		{
			item = trans.gameObject.AddComponent<HeroItem>();
			item.AddResetListener(ResetState);
			_items.Add(item);
		}
	}

	public void ResetState()
	{
		foreach (HeroItem item in _items)
		{
			item.UnSelected();
		}
	}
}
