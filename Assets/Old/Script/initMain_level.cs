using UnityEngine;
using System.Collections;

public class initMain_level : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject a;
		Instantiate (Resources.Load<GameObject> ("GameLevel/Gamelevel"+Plane_Gameclass.gamelevel.ToString()),transform.position,Quaternion.identity);
		a=Instantiate (Resources.Load<GameObject> ("Player/player"+Plane_Gameclass.playerleading.ToString()),transform.position,Quaternion.identity)as GameObject;
		a.transform.position = new Vector3 (0, -4, 0);
		a.GetComponent<Player> ().playermaxlife = PlayerPrefs.GetInt ("playershengming" + Plane_Gameclass.playerleading.ToString ());
		a.GetComponent<Player> ().Playerlife = PlayerPrefs.GetInt ("playershengming" + Plane_Gameclass.playerleading.ToString ());
		a.GetComponent<Player> ().bulletpower = PlayerPrefs.GetInt ("playergongji" + Plane_Gameclass.playerleading.ToString ());
		a.GetComponent<Player> ().bulletspeed = (float)(10.0f/PlayerPrefs.GetInt ("playerfangyu" + Plane_Gameclass.playerleading.ToString ()));
	}	
	
	// Update is called once per frame
	void Update () {
	
	}
}
