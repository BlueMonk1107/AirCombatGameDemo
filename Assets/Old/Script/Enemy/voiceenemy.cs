using UnityEngine;
using System.Collections;

public class voiceenemy : MonoBehaviour {
	public float enemyshuohua=4;//敌人说话的时间间隔
	float shuohuatime=0;//敌人说话记录时间
	AT_enemy[] atenemy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		shuohuatime += Time.deltaTime;
		if (shuohuatime > enemyshuohua) {
			atenemy=GameObject.FindObjectsOfType<AT_enemy>();
			if(atenemy.Length!=0)
			{
				int b=Random.Range(0,atenemy.Length);
				if(atenemy[b].BOSSYES==false)
				{
					GameObject selfenemy=Instantiate (Resources.Load<GameObject> ("UIvoice/"+atenemy[b].viocestring))as GameObject;
					selfenemy.transform.position=atenemy[b].transform.position;
					selfenemy.transform.parent=atenemy[b].transform;
					shuohuatime=0;
				}
			}
				}
	}
}
