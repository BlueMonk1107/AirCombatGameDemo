using UnityEngine;
using System.Collections;

public class box_movefree : MonoBehaviour {
	public float hengspeed=0.02f;
	float waittime;
	float movedertion_x=0;//X移动的方向
	float movedertion_y=0;//y移动的方向
	float randomtime;//随机移动的时间
	float movetype;//移动的方式
	public float move_x=1.24f;//x移动左右的大小
	public float move_y_up=4f;//y移动上的大小
	public float move_y_down=1.24f;//y移动上的大小
	GameObject myplayer;
	// Use this for initialization
	void Start () {
		myplayer = GameObject.FindGameObjectWithTag("player")as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (myplayer != null) {
						if (Mathf.Abs (transform.position.y - myplayer.transform.position.y) < 1.5f) {
								this.gameObject.GetComponent<box_movetoPlayer> ().enabled = true;
								this.enabled = false;
						} else {
								waittime += Time.deltaTime;
								if (waittime > randomtime) {
										movedertion_x = Mathf.Sign (Random.Range (-10, 10));
										movedertion_y = Mathf.Sign (Random.Range (-10, 10));
										randomtime = Random.Range (0.5f, 2f);
										waittime = 0;
										movetype = Random.Range (-1, 2);
								}
								if (movetype == -1) {
										boxs_x ();
								} else if (movetype == 1) {
										boxs_y ();
								} else {
										boxs_x ();
										boxs_y ();
								}
						}
				}
	}
	void boxs_y()
	{
		if (transform.position.y > move_y_down || transform.position.y < move_y_up) {
			transform.Translate (new Vector3 (0, movedertion_y, 0) * hengspeed);
		}
		if (transform.position.y <= move_y_down) {
			transform.Translate (new Vector3 (0, 1, 0) * hengspeed);
		}
		if (transform.position.y >= move_y_up) {
			transform.Translate (new Vector3 (0, -1, 0) * hengspeed);
		}
	}
	void boxs_x()
	{
		if (transform.position.x > -move_x || transform.position.x < move_x) {
			transform.Translate (new Vector3 (movedertion_x, 0, 0) * hengspeed);
		}
		if (transform.position.x <= -move_x) {
			transform.Translate (new Vector3 (1, 0, 0) * hengspeed);
		}
		if (transform.position.x >= move_x) {
			transform.Translate (new Vector3 (-1, 0, 0) * hengspeed);
		}
	}
}
