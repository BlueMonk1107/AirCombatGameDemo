using UnityEngine;
using System.Collections;

public class UI_mianleve : MonoBehaviour {
	public int whichlevel=1;
	bool ananniu=true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void tipbutton()
	{	
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIananniu"));
		print (whichlevel);
		if (ananniu) {
						if (whichlevel >=100) {
								transform.root.GetComponent<UI_main> ().UIloading.SetActive (true);
								Invoke ("waittime", 3f);
						} else {
								Invoke ("waittime2", 1f);
						}
			ananniu=false;
				}
	}
	void waittime()
	{
		ananniu=true;
		transform.root.GetComponent<UI_main>().UIloading.SetActive(false);
	}
	void waittime2()
	{
		Plane_Gameclass.playerDUNmount=2;//初始换盾牌数量
		Plane_Gameclass.playerPower=2;//大招的数量
		ananniu=true;
		Plane_Gameclass.gamelevel = whichlevel;
		Application.LoadLevel ("1");
	}
}
