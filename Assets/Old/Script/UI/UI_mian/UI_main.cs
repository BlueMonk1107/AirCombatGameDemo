using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_main : MonoBehaviour {
	public GameObject UIloading;//告诉玩家关卡还在开发中，请期待
	int levelpassmount=0;
	bool ananniu=true;
	public GameObject[] gamelevelUI;//游戏关卡的UI通过不影藏
	void Start () {
		levelpassmount = Plane_Gameclass.levelpassmount;
		foreach (GameObject a in gamelevelUI) {
				a.SetActive(true);
				}
		for (int i=0; i<levelpassmount; i++) {
			gamelevelUI[i].SetActive(false);
				}
	}
	
	// Update is called once per frame

	public void powerdownReturn()
	{
		if (ananniu) {
			Instantiate (Resources.Load<GameObject> ("UIvoice/UIananniu"));
			Invoke ("waittime", 1);
			ananniu=false;
		}
	}
	void waittime()
	{
		ananniu=true;
		Application.LoadLevel ("ChiocePlane");
	}
}
