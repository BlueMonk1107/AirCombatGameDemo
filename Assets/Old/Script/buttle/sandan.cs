using UnityEngine;
using System.Collections;

public class sandan : MonoBehaviour {
	float time;//产生子弹的时间
	public int mount=6;
	public string buttlestring;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 2f) {
			GameObject a;
			float augle=0;//角度
			for(int i=0;i<=mount;i++)
			{
				a=Instantiate (Resources.Load<GameObject> ("preinstall_butlle/rotate/rotate_bullet"+buttlestring), transform.position, Quaternion.Euler(new Vector3(0,0,-augle+180+(180/mount)*i)))as GameObject;
			a.GetComponent<Rigidbody2D>().AddForce(a.transform.right*100);
			//augle+=180/mount;
			}
			time=0;
		}
	}
}
