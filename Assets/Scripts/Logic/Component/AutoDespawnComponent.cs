using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDespawnComponent : MonoBehaviour,IUpdate
{

	private SpriteRenderer _renderer;
	private float _width;

	// Use this for initialization
	void Awake ()
	{
		_renderer = GetComponent<SpriteRenderer>();
		if(_renderer == null)
			Debug.LogError("当前物体没有spriteRenderer组件");

	}
	
	protected void OnEnable()
	{
		_width = _renderer.bounds.size.x;
		LifeCycleMgr.Single.Add(LifeName.UPDATE,this);
	}

	private void OnDisable()
	{
		LifeCycleMgr.Single.Remove(LifeName.UPDATE,this);
	}


	public int Times { get; set; }

	public int UpdateTimes
	{
		get { return 30; }
	}

	public void UpdateFun()
	{
		if (JudgeBeyondBorder())
		{
			if (!PoolMgr.Single.Despawn(gameObject))
			{
				Destroy(gameObject);
			}
		}
	}
	
	private bool JudgeBeyondBorder()
	{
		if (_renderer == null)
			return true;
		//判断底边界限
		if (_renderer.bounds.max.y < GameUtil.GetCameraMin().y)
		{
			return true;
		}
		else
		{
			if (transform.position.x < GameUtil.GetCameraMin().x - _width)
			{
				//判断左边界限
				return true;
			} 
			else if (transform.position.x > GameUtil.GetCameraMax().x + _width)
			{
				//判断右边界限
				return true;
			}
		}

		return false;
	}

}
