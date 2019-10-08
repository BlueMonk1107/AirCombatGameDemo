using UnityEngine;
using System.Collections;

public class GZ_enemy : Enemy {
	float waittime=0;
	Vector3 postion=Vector3.zero;//目标位置
	bool Qpostion=false;//是否确认目标位置
	public int left_right=1;
	public int jiaodu = -90;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	new void Update () {
		hitTime += Time.deltaTime;
		waittime += Time.deltaTime;
		if (waittime < 0.2f) {
			transform.Translate (new Vector3 (left_right, -1, 0) * speed, Space.World);
				}
		if (waittime > 0.2f&&Qpostion==false) {
			if(postion==Vector3.zero)
			{
			postion = GameObject.FindObjectOfType<Player> ().transform.position;
			}
			Qpostion=true;
				}
		if (Qpostion) {
			if(Mathf.Abs(transform.position.x-postion.x)<0.2f)
			{
				transform.Translate(new Vector3 (0, -1, 0) * speed,Space.World);
			}
			else
			{
				transform.Translate(new Vector3 (left_right, -1, 0) * speed,Space.World);
			}
		}
		if(waittime<=1)
		{
			transform.Rotate(new Vector3(0,0,1) * Time.deltaTime*jiaodu);
		}
		buttlelimit ();
	}
}
