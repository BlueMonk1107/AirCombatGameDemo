using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_plane : MonoBehaviour {
	public GameObject[] UIlift;
	public Text dunmount;//UI显示盾牌数量
	public Text Powermount;//UI显示大招数量
	public Text moneymount;//UI显示游戏分数
	public Text moneyjinyan;//UI显示游戏经验分数
	public Text score;//UI显示游戏经验分数

	public Text Winorfail;//显示游戏胜利或者失败的UI
	public GameObject WinorfailG;//显示游戏胜利的Gameobject
	public GameObject WinorfailF;//显示游戏失败的Gameobject
	int xunhuanmount = 0;
	public Text Winmonemount;
	public Text Winmoneyjinyan;
	public Text Winscore;
	public Text lift;//显示还剩多少生命
	public GameObject panel;//画布不再能器响应飞机的移动
	bool WinorFailBool=false;

	public GameObject StopgameUI;//停止游戏界面
	Player myplayer;
	// Use this for initialization
	void Start () {
		myplayer = GameObject.FindObjectOfType<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (WinorFailBool) {
			Winmonemount.text=Plane_Gameclass.playermoneymount.ToString ();
			Winmoneyjinyan.text = Plane_Gameclass.playerjinyan.ToString ();
			Winscore.text=Plane_Gameclass.Gamescore.ToString ();
			WinorFailBool=false;
			//GameWinorFailjiesuan ();
				}
		if (myplayer != null) {
						lift.text = myplayer.Playerlife.ToString ();
				} else {
						lift.text="0";
				}
		score.text=Plane_Gameclass.Gamescore.ToString ();
		moneyjinyan.text = Plane_Gameclass.playerjinyan.ToString ();
		moneymount.text = Plane_Gameclass.playermoneymount.ToString ();
		dunmount.text = Plane_Gameclass.playerDUNmount.ToString ();
		Powermount.text = Plane_Gameclass.playerPower.ToString ();
		if (myplayer == null) {
			myplayer = GameObject.FindObjectOfType<Player> ();
				}
		if (myplayer != null) {
			initliftUI((int)(myplayer.Playerlife*10/myplayer.playermaxlife));
				} 
	}
	//飞机生命实时显示
	void initliftUI(int mount)
	{
		foreach (GameObject lift in UIlift) {
			lift.SetActive(false);
				}
		for (int	i=mount-1; i>=0; i--) {
			UIlift[i].SetActive(true);
				}
	}
	//飞机开启盾
	public void addDUN()
	{
		if (myplayer != null) {
			myplayer.addDun();
		} 
	}
	//飞机释放大招
	public void beginPower()
	{
		if (myplayer != null) {
			myplayer.addPower();
		} 
	}
	//游戏胜利
	public void GameWin()
	{
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIgreat"));
		asas ();
		WinorfailG.SetActive (true);
		Winorfail.text="You Win";
		WinorFailBool = true;
		Invoke ("waittimewin", 5);
	}
	//游戏失败
	public void GameFail()
	{
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIgamover"));
		asas ();
		WinorfailF.SetActive (true);
		Winorfail.text="YouFail";
		Invoke ("waittimeFail", 5);
	}
	//游戏胜利
    void waittimewin()
	{
		if (Plane_Gameclass.gamelevel ==Plane_Gameclass.levelpassmount) {
			
			Plane_Gameclass.levelpassmount++;
			PlayerPrefs.SetInt("maxlevel",Plane_Gameclass.levelpassmount);
			
		}

		Application.LoadLevel ("MainUI");
	}
	//游戏失败
	void waittimeFail()
	{
		Application.LoadLevel ("MainUI");
	}
	//清屏
	void asas()
	{
		foreach (GameObject a in GameObject.FindGameObjectsWithTag("enemy")) {
			Destroy(a);
		}
		foreach (GameObject b in GameObject.FindGameObjectsWithTag("buttle")) {
			Destroy(b);
		}
	}
	public void stopgame()
	{
		//panel.SetActive()
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIananniu"));
		StopgameUI.SetActive (true);
		Time.timeScale = 0;
	}
	public void GOgame()
	{
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIananniu"));
		StopgameUI.SetActive (false);
		Time.timeScale = 1;
	}
	public void overgame()
	{
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIananniu"));
		Time.timeScale = 1;
		Application.LoadLevel ("MainUI");
	}
}
