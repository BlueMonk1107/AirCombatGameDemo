using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : NormalSingleton<ItemFactory> {

	public ItemViewBase GetItem(ItemType type)
	{
		switch (type)
		{
			case ItemType.ADD_BULLET:
				return AddBulletView.GetObject();
			case ItemType.ADD_EXP:
				return AddExpView.GetObject();
			case ItemType.SHIELD:
				return ShieldItemView.GetObject();
			case ItemType.POWER:
				return PowerItemView.GetObject();
		}

		return null;
	}
}
