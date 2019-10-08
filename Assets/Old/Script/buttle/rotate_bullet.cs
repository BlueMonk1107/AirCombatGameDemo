using UnityEngine;
using System.Collections;
//自旋转子弹
public class rotate_bullet : MonoBehaviour {
	 float time;//产生子弹的时间
	 float augle;//角度
	public string buttlestring;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 0.4f) {
			GameObject a;
			a=Instantiate (Resources.Load<GameObject> ("preinstall_butlle/rotate/rotate_bullet"+buttlestring), transform.position, Quaternion.Euler(new Vector3(0,0,augle)))as GameObject;
			a.GetComponent<Rigidbody2D>().AddForce(a.transform.up*100);
			augle+=20;
			time=0;
				}
	}
}
