using UnityEngine;
using System.Collections;

public class UI_ChiocePlane : MonoBehaviour {
	public GameObject[] choiceplayer;
	bool ananniu=true;
	void Start () {
		dedaoxinxinzshishuliang ();
	}
	public void powerdownOK()
	{
		if (ananniu) {
						Instantiate (Resources.Load<GameObject> ("UIvoice/UIananniu"));
						Invoke ("waittime", 1);
						ananniu=false;
				}
	}
	void waittime()
	{
		Application.LoadLevel ("MainUI");
		ananniu=true;
	}
	public void qianghua()
	{
		Application.LoadLevel ("palneqhua");
	}
	void dedaoxinxinzshishuliang()
	{
		if (!PlayerPrefs.HasKey ("playerzshi")) {
						PlayerPrefs.SetInt ("playerzshi", 555);
				} else {
						PlayerPrefs.SetInt ("playerzshi", 5200);
				}
		if (!PlayerPrefs.HasKey ("playerxinxin")) {
						PlayerPrefs.SetInt ("playerxinxin", 555);
				}
				else {
				PlayerPrefs.SetInt ("playerxinxin", 5200);
				}
	}
	public void tuichuyouxi()
	{
		Application.Quit ();
	}

 }
