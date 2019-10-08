using UnityEngine;
using System.Collections;

public class GameGround : MonoBehaviour {
	public GameObject[] map;
	public Vector3 endsence ;
	public Vector3 startmountion;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
				movescene (map, endsence, 0.5f);
}
	void movescene(GameObject[] scene,Vector3 endsence,float speed)
	{
		foreach(GameObject mysence in scene){
			if(mysence!=null)
			{
				float dY=mysence.transform.localPosition.y-endsence.y;
				if(dY<0){
					print (1);
					mysence.transform.localPosition=startmountion+new Vector3(dY+0.05f,0,0);
				}
				mysence.transform.localPosition= Vector3.MoveTowards (mysence.transform.localPosition, new Vector3(0,-200f,0), Time.deltaTime *speed);
			}
		}

	}

}
