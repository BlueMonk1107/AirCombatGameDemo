using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandModule : MonoBehaviour
{

	private RectTransform _rect;

	private RectTransform Rect
	{
		get
		{
			if (_rect == null)
			{
				_rect = transform.Rect();
			}

			return _rect;
		}
	}

	public void SetHand(Vector3 pos)
	{
		Rect.position = pos;
	}

	/// <summary>
	/// posRatio代表所在点在宽高中的位置比例，例如左下点是0，0，右上点是1,1
	/// </summary>
	/// <param name="targetRect"></param>
	/// <param name="posRatio"></param>
	public void SetHand(RectTransform targetRect,Vector2 posRatio)
	{
		Vector2 pos = targetRect.position;
		var ratio = posRatio - new Vector2(0.5f, 0.5f);
		pos += new Vector2(
			ratio.x * targetRect.rect.width * targetRect.localScale.x,
			ratio.y * targetRect.rect.height * targetRect.localScale.y);
		SetHand(pos);
	}
}
