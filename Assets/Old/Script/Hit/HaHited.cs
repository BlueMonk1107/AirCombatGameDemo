using UnityEngine;
using System.Collections;

public class HaHited : MonoBehaviour {
	public float destorytime=0.1f;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, destorytime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
