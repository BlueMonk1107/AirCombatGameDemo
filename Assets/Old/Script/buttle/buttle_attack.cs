using UnityEngine;
using System.Collections;
//个跟踪弹
public class buttle_attack : buttle {
	Vector3 postionvalue; 
	// Use this for initialization
	void Start () {
		if (GameObject.FindObjectOfType<Player> () != null) {
						postionvalue = GameObject.FindObjectOfType<Player> ().transform.position - transform.position;
				} else {
						postionvalue=new Vector3(0,-1,0);
				}
		postionvalue.Normalize ();
		GetComponent<Rigidbody2D>().AddForce (postionvalue*100);
	}
	
	// Update is called once per frame
	void Update () {
		buttlelimit ();
	}
}
