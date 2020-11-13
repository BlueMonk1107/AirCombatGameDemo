using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideUiMgr {
	protected static GuideUiMgr _single;
	public static GuideUiMgr Single
	{
		get
		{
			if (_single == null)
			{
				var t = new GuideUiMgr();

				_single = t;
			}

			return _single;
		}
	}
	private Transform _root;
	private Dictionary<string, Transform> _prefabCache;

	public void Init(Transform parent)
	{
		_prefabCache = new Dictionary<string, Transform>();
		
		var go = new GameObject("GuideUiRoot");
		var rect = go.AddComponent<RectTransform>();
		var canvas = go.AddComponent<Canvas>();
		go.AddComponent<GraphicRaycaster>();
		go.transform.SetParent(parent);
		_root = go.transform;

		rect.localPosition = Vector3.zero;
		rect.sizeDelta = _root.parent.GetComponent<RectTransform>().rect.size;
		canvas.overrideSorting = true;
		canvas.sortingOrder = 1;
	}

	public Transform Show(string path)
	{
		Transform trans = null;
		if (_prefabCache.ContainsKey(path))
		{
			if (_prefabCache[path].gameObject.activeSelf)
			{
				trans = CreateNew(path);
				trans.gameObject.AddComponent<AutoDestroy>();
			}
			else
			{
				trans = _prefabCache[path];
			}
		}
		else
		{
			trans = CreateNew(path);
			_prefabCache.Add(path,trans);
		}

		trans.gameObject.SetActive(true);
		return trans;
	}

	private Transform CreateNew(string path)
	{
		var prefab = Resources.Load<GameObject>(path);
		return Object.Instantiate(prefab, _root).transform;
	}

	public void Hide(Transform target)
	{
		target.gameObject.SetActive(false);
	}
}
