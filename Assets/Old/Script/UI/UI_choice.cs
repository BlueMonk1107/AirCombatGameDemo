using UnityEngine;
using System.Collections;

public class UI_choice : MonoBehaviour {
	public int plaermount;
	public void choiceplayerf()
	{
		Instantiate (Resources.Load<GameObject> ("UIvoice/UIplayer"+plaermount.ToString()));
		Hidechoiceplayer ();
		Plane_Gameclass.playerleading=plaermount;
		gameObject.SetActive (false);
	}
	void Hidechoiceplayer()
	{
		foreach (GameObject a in transform.root.GetComponent<UI_ChiocePlane>().choiceplayer) {
			a.SetActive(true);
				}
	}
}
