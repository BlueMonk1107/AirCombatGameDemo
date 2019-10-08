using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Eventpiont : MonoBehaviour,IPointerUpHandler,IPointerDownHandler,IDragHandler {
	public static Eventpiont instance;
	bool Bmove=false;
	Vector3 movexiangl;
	public Vector3 Movexiangl{
		get
		{
			return movexiangl;
		}
	}
	public GameObject image;
	Vector3 mousemovepostion;
	Camera[] mycamer;
	Camera selfCamer;
	// Use this for initialization
	void Start () {
		instance = this;
		mycamer=new Camera[5];
		Camera.GetAllCameras (mycamer);
		foreach(Camera a in mycamer)
		{
			if(a!=null)
			{
				if(a.tag=="UI")
				{
					selfCamer=a;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void uuu()
	{}
	public void OnPointerUp (PointerEventData eventData)
	{
		Bmove = false;
		image.transform.position = transform.position;
		movexiangl = Vector3.zero;
	}
	public void OnPointerDown (PointerEventData eventData)
	{
		Bmove = true;
		mousemovepostion = selfCamer.ScreenToWorldPoint (eventData.position);
		image.transform.position = new Vector3 (mousemovepostion.x, mousemovepostion.y, transform.position.y);
		movexiangl = new Vector3 (mousemovepostion.x - transform.position.x, mousemovepostion.y - transform.position.y, 0);
		movexiangl.Normalize ();
	}
	public void OnDrag (PointerEventData eventData)
	{
		if (Bmove) {
						mousemovepostion = selfCamer.ScreenToWorldPoint (eventData.position);
						image.transform.position = new Vector3 (mousemovepostion.x, mousemovepostion.y, transform.position.y);
						movexiangl = new Vector3 (mousemovepostion.x - transform.position.x, mousemovepostion.y - transform.position.y, 0);
						movexiangl.Normalize ();
				}
	}
}
