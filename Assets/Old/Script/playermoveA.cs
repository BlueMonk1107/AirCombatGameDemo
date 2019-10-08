using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class playermoveA :MonoBehaviour,IPointerUpHandler,IPointerDownHandler,IDragHandler{
//public class playermoveA :MonoBehaviour{//,IPointerUpHandler,IPointerDownHandler,IDragHandler{
	public static playermoveA instance;
	Vector3 Doenpostion;
	public Vector3 Nowdragpostion;
	public Vector3 ONdragpostion;
	public Vector3 playerpostion;
	bool move=false;
	public Sprite asd;
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnPointerUp (PointerEventData eventData)
	{
		Doenpostion = Vector3.zero;
		ONdragpostion = Vector3.zero;
		move = false;
	}
	public void OnPointerDown (PointerEventData eventData)
	{
		Doenpostion=eventData.position;
		move = true;
	}
	public void OnDrag (PointerEventData eventData)
	{
		if (move) {

						playerpostion=Camera.main.ScreenToWorldPoint(eventData.position);
						playerpostion=new Vector3(playerpostion.x,playerpostion.y,0);
						ONdragpostion =new Vector3 (Doenpostion.x - eventData.position.x, Doenpostion.y - eventData.position.y, 0);
						ONdragpostion.Normalize ();
				}
	}
}
