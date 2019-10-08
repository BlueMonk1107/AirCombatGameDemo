using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_planeqhua : MonoBehaviour {
	public static UI_planeqhua instance;
	int STATE=1;
	public Sprite[] SpriteRendererone;
	public Sprite[] SpriteRenderertwo;
	public Sprite[] SpriteRendererthree;
	public Image dangqian;//当前飞机的图片
	public Slider[] shuxing;//当前飞机的属性
	int player;//当前选择的角色飞机等级
	Hashtable playerHa;//存取player当前属性的哈希表
	public Text xinxintext;//显示现在星星的数量
	public Text zhuanshi;//显示现在钻石的数量
	public Text Textgongji;
	public Text Textgongsu;
	public Text Textgonglift;
	int i=0;
	int dsds;
	int qianghuaquerenM;//强化确认需要确定强化那一个属性
	public GameObject qianghuaquerenUI;//调出强化确认界面
	// Use this for initialization
	void Start () {
		instance = this;
		playerHa=new Hashtable();
		choicechang ();
	}
	
	// Update is called once per frame
	void Update () {
		Textgonglift.text=PlayerPrefs.GetInt ("playershengming" + STATE.ToString ()).ToString();;
		Textgongji.text=PlayerPrefs.GetInt ("playergongji" + STATE.ToString ()).ToString();
		Textgongsu.text=(((float)(int)((10.0f/PlayerPrefs.GetInt ("playerfangyu" + STATE.ToString ()))*100))/100).ToString();
		xinxintext.text = PlayerPrefs.GetInt ("playerxinxin").ToString ();
		zhuanshi.text = PlayerPrefs.GetInt ("playerzshi").ToString ();
		shuxing [0].value = PlayerPrefs.GetInt ("playergongji" + STATE.ToString ());
		shuxing [1].value = PlayerPrefs.GetInt ("playerfangyu" + STATE.ToString ());
		shuxing[2].value=PlayerPrefs.GetInt ("playershengming" + STATE.ToString ());
		dsds=PlayerPrefs.GetInt ("player" + STATE.ToString ());
		if (dsds < 4) {
			if(STATE==1)
			{
				dangqian.sprite = (Sprite)SpriteRendererone.GetValue (dsds);
			}
			if(STATE==2)
			{
				dangqian.sprite = (Sprite)SpriteRenderertwo.GetValue (dsds);
			}
			if(STATE==3)
			{
				dangqian.sprite = (Sprite)SpriteRendererthree.GetValue (dsds);
			}
				}
	}
	void choicechang()
	{
		switch (STATE) {
		case 1:
			findplayer(1);
			break;
		case 2:
			findplayer(2);
			break;
		case 3:
			findplayer(3);
			break;
		default:
			break;
		}
//		i=(int)playerHa["player"];
//		if (i > 4) {
//		dangqian.sprite = (Sprite)SpriteRendererone.GetValue (i);
//				}
		i=(int)playerHa["playergongji"];
		shuxing[0].value=i;
		i=(int)playerHa["playerfangyu"];
		shuxing[1].value=i;
		i=(int)playerHa["playershengming"];
		shuxing[2].value=i;
	}
	public void findplayer(int a)
	{
		//哪一个飞机
		if(!PlayerPrefs.HasKey("player"+a.ToString()))
		{
			PlayerPrefs.SetInt("player"+a.ToString(),1);
			playerHa.Add("player",PlayerPrefs.GetInt("player"+a.ToString()));
		}
		else
		{
			if(PlayerPrefs.GetInt("player"+a.ToString())>=4)
			{
			PlayerPrefs.SetInt("player"+a.ToString(),3);
			playerHa.Add("player",PlayerPrefs.GetInt("player"+a.ToString()));
			}
		}
		//飞机现在的攻击力
		if(!PlayerPrefs.HasKey("playergongji"+a.ToString()))
		{
			PlayerPrefs.SetInt("playergongji"+a.ToString(),5);
			playerHa.Add("playergongji",PlayerPrefs.GetInt("playergongji"+a.ToString()));
		}
		else
		{
			playerHa.Add("playergongji",PlayerPrefs.GetInt("playergongji"+a.ToString()));
		}
		//飞机现在的防御力
		if(!PlayerPrefs.HasKey("playerfangyu"+a.ToString()))
		{
			PlayerPrefs.SetInt("playerfangyu"+a.ToString(),50);
			playerHa.Add("playerfangyu",PlayerPrefs.GetInt("playerfangyu"+a.ToString()));
		}
		else
		{
			playerHa.Add("playerfangyu",PlayerPrefs.GetInt("playerfangyu"+a.ToString()));
		}
		//飞机现在的生命
		if(!PlayerPrefs.HasKey("playershengming"+a.ToString()))
		{
			PlayerPrefs.SetInt("playershengming"+a.ToString(),100);
			playerHa.Add("playershengming",PlayerPrefs.GetInt("playershengming"+a.ToString()));
		}
		else
		{
			playerHa.Add("playershengming",PlayerPrefs.GetInt("playershengming"+a.ToString()));
		}
	}
	public void qianghuaqueren()
	{
		int m = 0;
		switch (qianghuaquerenM) {
		case 1:
			m=PlayerPrefs.GetInt ("playerxinxin");
			if (m >= 200) {
				m -=200;
				Instantiate (Resources.Load<GameObject> ("UIvoice/UIye"));
				PlayerPrefs.SetInt ("playergongji" + STATE.ToString (), PlayerPrefs.GetInt ("playergongji" + STATE.ToString ()) + 1);
				PlayerPrefs.SetInt("playerxinxin",m);
			}
			break;
		case 2:
			 m=PlayerPrefs.GetInt ("playerxinxin");
			if (m >= 200) {
				m -=200;
				Instantiate (Resources.Load<GameObject> ("UIvoice/UIye"));
				PlayerPrefs.SetInt("playerfangyu"+STATE.ToString(),PlayerPrefs.GetInt ("playerfangyu" + STATE.ToString ())+5);
				PlayerPrefs.SetInt("playerxinxin",m);
			}
			break;
		case 3:
			m=PlayerPrefs.GetInt ("playerxinxin");
			if (m >= 200) {
				m -=200;
				Instantiate (Resources.Load<GameObject> ("UIvoice/UIye"));
				PlayerPrefs.SetInt("playershengming"+STATE.ToString(),PlayerPrefs.GetInt ("playershengming" + STATE.ToString ())+3);
				PlayerPrefs.SetInt("playerxinxin",m);
			}
			break;
		case 4:
			m=PlayerPrefs.GetInt ("playerzshi");
			if (m >= 2000) {
				m -=2000;
				Instantiate (Resources.Load<GameObject> ("UIvoice/UIye"));
				PlayerPrefs.SetInt ("player" + STATE.ToString (), PlayerPrefs.GetInt ("player" + STATE.ToString ()) + 1);
				PlayerPrefs.SetInt("playerzshi",m);
			}
			break;
				}
		qianghuaquerenUI.SetActive (false);
	}
	public void qianghuaquerenquxiao()
	{
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIananniu"));
		qianghuaquerenUI.SetActive (false);
	}
	public void qianghua(int a)
	{
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIananniu"));
		qianghuaquerenM = a;
		qianghuaquerenUI.SetActive (true);
	}
	public void changplaner()
	{
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIananniu"));
		playerHa.Clear ();
		STATE += 1;
		if (STATE == 4) {
						STATE = 1;
						choicechang ();
				} else {
					choicechang ();
				}
		print (STATE);
	}
	public void changplanel()
	{
		playerHa.Clear ();
		STATE -= 1;
		if (STATE == 0) {
			STATE = 3;
			choicechang ();
		} else {
			choicechang ();
		}
		print (STATE);
	}
	public void renturnXZplayer()
	{
		Application.LoadLevel ("ChiocePlane");
	}
}
