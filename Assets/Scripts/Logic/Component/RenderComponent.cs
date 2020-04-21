using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderComponent : MonoBehaviour {
	
	public SpriteRenderer Renderer { get;private set;}
	private CapsuleCollider2D _collider;

	public void Init()
	{
		if(Renderer != null && _collider != null)
			return;
		Renderer = GetComponent<SpriteRenderer>();
		_collider = GetComponent<CapsuleCollider2D>();
	}

	public bool SetSprite(Sprite sprite)
	{
		if (Renderer != null)
		{
			bool success = false;
			success = Renderer.sprite != sprite;
			
			float oldX = Renderer.bounds.size.x;
			float oldY = Renderer.bounds.size.y;
			Renderer.sprite = sprite;
			float newX = Renderer.bounds.size.x;
			float newY = Renderer.bounds.size.y;
			float xRatio = newX / oldX;
			float yRatio = newY / oldY;
			
			var size = _collider.size;
			size = new Vector2(size.x*xRatio,size.y*yRatio);
			_collider.size = size;

			return success;
		}
		else
		{
			return false;
			Debug.LogError("当前物体SpriteRenderer为空，物体名:"+gameObject.name);
		}
	}
}
