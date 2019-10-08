using UnityEngine;
using System.Collections;

public class angle_buttle : buttle {
	float time;//产生子弹的时间
	bool turn=false;
	public int L=-1;
	public float agnel=10;//子弹偏移角度
	public int power=2;//该子弹的威力
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		buttlelimit ();
		time += Time.deltaTime;
		transform.Translate(Vector3.up * Time.deltaTime*speed);
		if (!turn) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, agnel*L));
						turn=true;
				}
		if (time < 1f&&turn) {
			transform.Translate(-Vector3.right * Time.deltaTime*0.05f*(agnel/10.0f)*L);
			transform.Rotate (transform.forward * -Time.deltaTime * agnel*L, Space.Self);
				}
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "enemy"&&player) {
			other.GetComponent<Enemy>().Enemydamage(transform.position,power);
		}
		if (other.tag == "buttle"&&player) {
			Destroy(other.gameObject);
				}
	}
}
