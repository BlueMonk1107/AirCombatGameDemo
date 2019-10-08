using UnityEngine;
using System.Collections;

public class lethality_buttle : buttle {

	// Use this for initialization
	void Start () {
	
	}
	void Update () {
		transform.Translate(Vector3.down*speed,Space.World);
		buttlelimit ();
	}
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "player") {
			other.transform.parent.GetComponent<Player>().PlayerDestory();
		}
	}
}
