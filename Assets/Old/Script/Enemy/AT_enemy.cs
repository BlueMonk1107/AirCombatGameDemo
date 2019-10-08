using UnityEngine;
using System.Collections;

public class AT_enemy : Enemy {
	public AudioClip normalvioce;//boss被摧毁返回原来的音乐
	public float hengspeed=0.02f;
	public string viocestring;//该智能飞机的Vioce
//	bool left=false;
//	bool right=false;
	float waittime;
	float movedertion_x=0;//X移动的方向
	float movedertion_y=0;//y移动的方向
	float movetype;//移动的方式
	public float move_x=1.24f;//x移动左右的大小
	public float move_y_up=4f;//y移动上的大小
	public float move_y_down=1.24f;//y移动上的大小
	public float moveY=2;//移动到Y方向的位置
	float randomtime;//随机移动的时间
	public int planetype=2;//默认为普通AT飞机的移动方式，0为BOSS的移动方式
	public bool BOSSYES=false;
	// Use this for initialization
	void Start () {
		hitpiancha = 0.64f;
		randomtime = Random.Range (0.5f, 2f);
	}
	
	// Update is called once per frame
	new void Update () {
		hitTime += Time.deltaTime;
		waittime += Time.deltaTime;
		switch (planetype) {
		case 0:
			if (transform.position.y > move_y_down) {
				transform.Translate (new Vector3 (0, -1, 0) * speed,Space.World);
			}
			break;
		case 2:
			if (waittime > randomtime) {
				movedertion_x = Mathf.Sign (Random.Range (-10, 10));
				movedertion_y = Mathf.Sign (Random.Range (-10, 10));
				randomtime = Random.Range (0.5f, 2f);
				waittime = 0;
				movetype = Random.Range (-1, 2);
			}
			if (movetype == -1) {
				enemymove_x ();
			} else if (movetype == 1) {
				enemymove_y ();
			} else {
				enemymove_x ();
				enemymove_y ();
			}
			break;
		case 1:
			transform.Translate (new Vector3 (0, -1, 0) * speed);
			if (waittime>0.5f) {
				waittime=0;
				planetype=2;
			}
			break;
				}
				
	}
	 void enemymove_y()
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
	 void enemymove_x()
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
	public override void EnemyDestory()
	{
		Instantiate (Resources.Load<GameObject> ("Hited/Destoryed"),transform.position,Quaternion.identity);
		if (BOSSYES) {
			print ("GameWin");
			GameObject.FindObjectOfType<initMain_level>().GetComponent<AudioSource>().clip=normalvioce;
			GameObject.FindObjectOfType<initMain_level>().GetComponent<AudioSource>().Play();
			Plane_Gameclass.ATenemyboss--;
			if(Plane_Gameclass.ATenemyboss<=0)
			{
			GameObject.FindObjectOfType<UI_plane>().GameWin();
			}
			else
			{
				foreach(Enmeyfactory a in GameObject.FindObjectsOfType<Enmeyfactory>())
				{
					if(a.statetype==Enmeyfactory.enemyType.Nothing)
					{
						print ("Boss come");
						a.newbosslaile();
					}
				}
			}
				
		} else {
			for(int i=0;i<moneymount;i++)
			{
				Instantiate (Resources.Load<GameObject> ("BOX/BOX_addmoney"),transform.position+new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0),Quaternion.identity);
			}
			int m=Random.Range(0,10);
			if(m==1)
			{
				Instantiate (Resources.Load<GameObject> ("BOX/BOX_addjinyan"),transform.position+new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0),Quaternion.identity);
			}
			else if(m==2)
			{
			Instantiate (Resources.Load<GameObject> ("BOX/BOX_addButtle"),transform.position+new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0),Quaternion.identity);
			}
			else if(m==3)
			{
				Instantiate (Resources.Load<GameObject> ("BOX/BOX_addMaxButtle"),transform.position+new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0),Quaternion.identity);
			}
			Plane_Gameclass.ATenemymount--;
				}
		Destroy (gameObject);
	}
}
