using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Win : MonoBehaviour {
	public Text gamemoney;
	int moneymount=0;
	int xunhuanmount = 0;
	// Use this for initialization
	void Start () {
		moneymount = Plane_Gameclass.playermoneymount;
	}
	
	// Update is called once per frame
	void Update () {
		if (xunhuanmount < moneymount) {
						xunhuanmount += 1;
						gamemoney.text = xunhuanmount.ToString ();
				} else {
						gamemoney.text = moneymount.ToString ();
				}
	}
	public void renturnmain()
	{
		Application.LoadLevel ("MainUI");
	}
}
