using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed=0.1f;
	public GameObject[] limit;


	//玩家被击中相应
	public float Playerlife=100;//玩家的生命
	public float playermaxlife=100;//玩家最大生命值
	public float bulletspeed=0.2f;//飞机的攻击速度
	public float bulletpower=5;//飞机的攻击力
	protected float hitpiancha=0;//攻击偏差
	

	//power(大招被激活)
	bool poweractivate=false;
	float powertime=0;//用于获取大招的持续时间
	float powertimeC=0;//用于大招的计时
	//DUN(盾牌被激活)
	bool Dunactivate=false;
	float Duntime=0;//用于获取盾牌的持续时间
	float DuntimeC=0;//用于盾牌的计时

	public GameObject Normalstart;//正常攻击模式
	int Normalmount=1;//正常模式枪口
	public GameObject[] Normalmuzzle;//正常模式下的枪口数组
	float timemove=0;//移动记时
	// Use this for initialization
	void Start () {
		initshootmuzzle (-1);
		print (playermaxlife + "*" + bulletspeed + "*" + bulletpower);
	}
	
	// Update is called once per frame
	void Update () {
		if (poweractivate) {
			powertimeC+=Time.deltaTime;
			if(powertime-powertimeC<0)
			{
				powertimeC=0;
				poweractivate=false;
				Normalstart.SetActive(true);
			}
				}
		if (Dunactivate) {
			DuntimeC+=Time.deltaTime;
			if(Duntime-DuntimeC<0)
			{
				DuntimeC=0;
				Dunactivate=false;
			}
		}
//		float Vertical = Input.GetAxis("Vertical");
//		float Horizontal = Input.GetAxis("Horizontal");
//		float Vertical = -playermoveA.instance.ONdragpostion.y;
//		float Horizontal=-playermoveA.instance.ONdragpostion.x;
//		int Heng, vert;
//		playerlimit(Horizontal,Vertical,out Heng,out vert);
//		if (Heng != 0) {
//						Horizontal = 0;
//				} 
//				else if (Horizontal > 0) {
//						Horizontal = 1*0.5f;
//				} else if (Horizontal < 0) {
//						Horizontal=-1*0.5f;
//				}
//		if (vert != 0) {
//				Vertical=0;
//				}
//		else if (Vertical > 0) {
//			Vertical = 1*0.5f;
//		} else if (Vertical < 0) {
//			Vertical=-1*0.5f;
//		}
		if (playerlimit2 ()) {
						transform.position = Vector3.MoveTowards (transform.position, playermoveA.instance.playerpostion, 0.1f);
				}
//		if (Mathf.Abs (playermoveA.instance.playerpostion.x - transform.position.x)>= 0.2|| Mathf.Abs (playermoveA.instance.playerpostion.y - transform.position.y)>= 0.2) {
//						transform.Translate (new Vector3 (Horizontal, Vertical, 0) * speed);
//				}
	}
	//用于限制角色移除摄像机范围
	void playerlimit( float Horizontal,float Vertical ,out int Heng,out int vert)
	{
		Heng = 0;
		vert = 0;
		if (limit [0].transform.position.x < -2.5f&&Horizontal<0||limit [1].transform.position.x > 2.5f&&Horizontal>0) {
				Heng=-1;
			}
		if (limit [2].transform.position.y > 4.5f&&Vertical>0||limit [3].transform.position.y < -4.5f&&Vertical<0) {
				vert=-1;
			}
	}
	bool playerlimit2()
	{
		if (limit [0].transform.position.x < -2.5f&&playermoveA.instance.playerpostion.x<-2.4f || limit [1].transform.position.x > 2.5f&&playermoveA.instance.playerpostion.x>2.4f||limit [2].transform.position.y > 4.5f&&playermoveA.instance.playerpostion.y>4.5f || limit [3].transform.position.y< -4.5f &&playermoveA.instance.playerpostion.y<-4.5f) {
						return false;
				} else {
			return true;
				}
//		if (limit [2].transform.position.y > 4.5f&&playermoveA.instance.playerpostion.y>4.5f || limit [3].transform.position.y< -4.5f &&playermoveA.instance.playerpostion.y<-4.5f) {
//						return false;
//				} else {
//			return true;
//				}
	}
	//更新角色的枪口个数
	public void playershootmuzzle()
	{
		initshootmuzzle (Normalmount);
	}
	public void playershootmuzzle(int Max)
	{
		initshootmuzzle (5);
	}
	//更新枪口
	void initshootmuzzle(int mount)
	{
		print (mount);
		Normalmount += 2;
		if (Normalmount < 8) {
						foreach (GameObject a in Normalmuzzle) {
								a.SetActive (false);
						}
					for (int i=0; i<Normalmount; i++) {
								Normalmuzzle [i].SetActive (true);
						}
				}
	}
	//自己被打中
	public  void Playerdamage(Vector3 postion,float buttlePower)
	{
			Instantiate (Resources.Load<GameObject> ("UIvoice/UIbeigongji"));
			GameObject a;
			a=Instantiate (Resources.Load<GameObject> ("Hited/Hit1"), postion+new Vector3(0,hitpiancha,0), Quaternion.Euler(new Vector3(0,0,(Random.Range(0,360)))))as GameObject;
			a.transform.parent=transform;
		Playerlife -= buttlePower;
		if (Playerlife < 0) {
			PlayerDestory();
		}
		
	}
	public void PlayerDestory()
	{
		Instantiate (Resources.Load<GameObject> ("Hited/Destoryed"),transform.position,Quaternion.identity);
		Destroy (gameObject);
		GameObject.FindObjectOfType<UI_plane>().GameFail();
	}
	//角色得到游戏分数
	public void addmoney()
	{
		Plane_Gameclass.playermoneymount++;
	}
	//角色得到游戏经验
	public void addexperience(int explevel)
	{
		Plane_Gameclass.playerjinyan+=explevel*1;
	}
	//为角色加上护盾
	public void addDun()
	{
		if (Plane_Gameclass.playerDUNmount > 0 && Dunactivate == false) {
						Instantiate (Resources.Load<GameObject> ("UIvoice/UIadddun"));
						GameObject a;
						a = Instantiate (Resources.Load<GameObject> ("Hited/DUN"), transform.position, Quaternion.identity)as GameObject;
						a.transform.parent = transform;
						Duntime = a.GetComponent<HaHited> ().destorytime;
						Dunactivate = true;
						Plane_Gameclass.playerDUNmount--;
				} else {
						Instantiate (Resources.Load<GameObject> ("UIvoice/UItianz"));
				}
	}
	//角色开启大招模式
	public void addPower()
	{
		if (Plane_Gameclass.playerPower > 0 && poweractivate == false) {
						Instantiate (Resources.Load<GameObject> ("UIvoice/UIadddpower"));
						GameObject a;
						a = Instantiate (Resources.Load<GameObject> ("Hited/Power"), transform.position, Quaternion.identity)as GameObject;
						a.transform.parent = transform;
						powertime = a.GetComponent<HaHited> ().destorytime;
						poweractivate = true;
						Normalstart.SetActive (false);
						Plane_Gameclass.playerPower--;
				} else {
						Instantiate (Resources.Load<GameObject> ("UIvoice/UItianz"));
				}
	}
}
