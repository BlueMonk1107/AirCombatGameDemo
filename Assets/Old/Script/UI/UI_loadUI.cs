using UnityEngine;
using System.Collections;

public class UI_loadUI : MonoBehaviour {
	float waittime=3;//延时时间
	float time=0;
	bool BG=false;//开始了么？
	Animator animation;
	// Use this for initialization
	void Start () {
	Screen.SetResolution (500, 900, false);
		print (PlayerPrefs.GetInt("maxlevel"));
		if(!PlayerPrefs.HasKey("maxlevel"))
		{
			PlayerPrefs.SetInt("maxlevel",1);
			Plane_Gameclass.levelpassmount=PlayerPrefs.GetInt("maxlevel");
		}
		else
		{
			PlayerPrefs.SetInt("maxlevel",20);
			Plane_Gameclass.levelpassmount=PlayerPrefs.GetInt("maxlevel");
		}
		initplayershuxin (1);
		initplayershuxin (2);
		initplayershuxin (3);
		animation = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (BG) {
						time += Time.deltaTime;
			if(time>waittime)
			{
				Application.LoadLevel("ChiocePlane");
				BG=false;
			}
				}

	}
	public void beginGame()
	{
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIstartgam"),transform.position,Quaternion.identity);
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIlaoding"),transform.position,Quaternion.identity);
		animation.SetTrigger ("begingame");
		BG = true;
	}
	void initplayershuxin(int a)
	{
		if(!PlayerPrefs.HasKey("player"+a.ToString()))
		{
			PlayerPrefs.SetInt("player"+a.ToString(),1);
		}
		if(!PlayerPrefs.HasKey("playergongji"+a.ToString()))
		{
			PlayerPrefs.SetInt("playergongji"+a.ToString(),5);
		}
		if(!PlayerPrefs.HasKey("playerfangyu"+a.ToString()))
		{
			PlayerPrefs.SetInt("playerfangyu"+a.ToString(),50);
		}
		if(!PlayerPrefs.HasKey("playershengming"+a.ToString()))
		{
			PlayerPrefs.SetInt("playershengming"+a.ToString(),100);
		}
	}
}
