using UnityEngine;
using System.Collections;

public class shell : MonoBehaviour {
	// Use this for initialization
	void Start () {
		transform.DetachChildren ();
		Destroy (gameObject);
	}
	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
