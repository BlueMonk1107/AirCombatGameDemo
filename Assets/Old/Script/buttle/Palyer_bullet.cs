using UnityEngine;
using System.Collections;

public class Palyer_bullet : MonoBehaviour {
	public  enum bullettype{one,two,three,four}
	public bullettype state=bullettype.one;
	public string bulletS;//哪种子弹
	public float bulletpower;//子弹的威力
	public float speed=0.2f;//子弹的速度
	float Bullettime;//子弹发射的速度 
	bool Bulletshoot=false;
	GameObject bullet;
	public float agnel;//爆炸方向的角度
	public int L=-1;//爆炸的方向
	// Use this for initialization
	void Start () {
		bulletpower=transform.root.GetComponent<Player>().bulletpower;
		speed=transform.root.GetComponent<Player>().bulletspeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Bulletshoot) {
						Bullettime += Time.deltaTime;
				if (Bullettime > speed) {
				Instantiate(Resources.Load<GameObject> ("preinstall_butlle/buttlevoice"));
								Bulletshoot = true;
								Bullettime = 0;
						}
				}
		switch (state) {
		case bullettype.one:
			if (Bulletshoot) {
				 bullet = Instantiate (Resources.Load<GameObject> ("preinstall_butlle/shell" + bulletS), transform.position, Quaternion.identity)as GameObject;
				bullet.transform.Find ("butlle").GetComponent<buttle> ().player = true;
				bullet.transform.Find ("butlle").GetComponent<buttle> ().buttlePower=bulletpower;
				Bulletshoot=false;
			}
			break;
		case bullettype.two:
			if (Bulletshoot) {
				bullet = Instantiate (Resources.Load<GameObject> ("preinstall_butlle/angle/shell" + bulletS), transform.position, Quaternion.identity)as GameObject;
				bullet.transform.Find ("butlle").GetComponent<buttle> ().player = true;
				bullet.transform.Find ("butlle").GetComponent<angle_buttle> ().agnel=agnel;
				bullet.transform.Find ("butlle").GetComponent<angle_buttle> ().L=L;
				Bulletshoot=false;
			}
			break;
				}
	}
}
