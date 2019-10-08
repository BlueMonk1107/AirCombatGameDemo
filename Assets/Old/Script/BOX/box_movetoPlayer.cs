using UnityEngine;
using System.Collections;


public class box_movetoPlayer : MonoBehaviour {
	GameObject playerpotion;
	public float movetime=1;//等待移动的时间
	float time=0;
	// Use this for initialization
	void Start () {
		playerpotion = GameObject.FindGameObjectWithTag("player")as GameObject;
		//iTween.MoveTo (gameObject,iTween.Hash("position",playerpotion.transform.position,"time",2f,"delay",movetime,"easetype",EaseType.easeInBack));
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (playerpotion != null) {
						if (time > movetime) {
								transform.position = Vector3.MoveTowards (transform.position, playerpotion.transform.position, 0.1f);
						}
				}
		//transform.Translate(playerpotion,)
	}
}
