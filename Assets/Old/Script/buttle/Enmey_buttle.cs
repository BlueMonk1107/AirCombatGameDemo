using UnityEngine;
using System.Collections;

public class Enmey_buttle : MonoBehaviour {
	float time;
	public float buttlewaittime=4f;//装弹时间

	bool buttleOK=false;
	public string  zidan;//发射哪种子弹
	public int mount=3;//一次产生发射子弹的个数
	public float mounttime=0.3f;//一次产生子弹的间隔
	int nowmount=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time+=Time.deltaTime;
		if (time > buttlewaittime) {
			buttleOK=true;
			time=0;
		}
		if (buttleOK) {
			if(time>mounttime)
			{
			GameObject bullet=Instantiate(Resources.Load<GameObject>("preinstall_butlle/shell"+zidan),transform.position,Quaternion.Euler(new Vector3(0,0,180)))as GameObject;
			bullet.transform.Find("butlle").GetComponent<buttle>().player=false;
			bullet.transform.Find("butlle").GetComponent<buttle>().speed=6f;
			time=0;
			nowmount++;
			}
			if(nowmount==mount)
			{
				buttleOK=false;
				nowmount=0;
			}
		}
	}
}
