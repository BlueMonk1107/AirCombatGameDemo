using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ExplainDialogModule : MonoBehaviour
{
	public RectTransform Target;
	
	private Text _contentText;
	private RectTransform _bgRect;
	private RectTransform _selfRect;
	private LayoutElement _layoutElement;
	private float _offset = 10;
	
	// Use this for initialization
	async void Start ()
	{
		Init(Target, DialogPosition.LEFT);
	}

	public async void Init(RectTransform targetUi,DialogPosition position)
	{
		if (targetUi == null)
		{
			Debug.LogError("目标ui不能为空");
			return;
		}
		
		InitComponent();
		gameObject.SetActive(false);
		await Task.Delay(100);
		gameObject.SetActive(true);

		switch (position)
		{
			case DialogPosition.LEFT:
				float max = targetUi.rect.xMin;
				float min = -_selfRect.rect.width / 2;
				float widthMax = max - min - _offset * 2;

				if (_selfRect.rect.width > widthMax)
				{
					_layoutElement.preferredWidth = widthMax;
				}
				else
				{
					_layoutElement.preferredWidth = -1;
				}

				float y = _contentText.transform.InverseTransformPoint(targetUi.position).y;
				_contentText.transform.localPosition = new Vector3(min + widthMax /2,y);

				break;
			case DialogPosition.RIGLT:
				break;
			case DialogPosition.UP:
				break;
			case DialogPosition.DOWN:
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(position), position, null);
		}
	}
	
	private void InitComponent()
	{
		if (_contentText == null)
			_contentText = transform.Find("Content").GetComponent<Text>();

		if (_contentText != null)
		{
			if (_bgRect == null)
				_bgRect = _contentText.transform.Find("ContentBg").GetComponent<RectTransform>();

			if (_layoutElement == null)
				_layoutElement = _contentText.GetComponent<LayoutElement>();
		}

		if (_selfRect == null)
			_selfRect = GetComponent<RectTransform>();
		
	}
}

public enum DialogPosition
{
	LEFT,
	RIGLT,
	UP,
	DOWN
}
