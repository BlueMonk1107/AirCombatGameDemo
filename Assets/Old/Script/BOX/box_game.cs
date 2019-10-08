using UnityEngine;
using System.Collections;

public class box_game : MonoBehaviour {
	public AudioClip music;
	public enum Box_gametype{addbuttle,addbuttleMax,addexperience,addmoney,Doubleexperience}
	public Box_gametype boxtype=Box_gametype.addbuttle;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "player") {
			switch(boxtype)
			{
			case Box_gametype.addbuttle:
				Instantiate (Resources.Load<GameObject> ("UIvoice/UIye"));
				other.transform.parent.GetComponent<Player>().playershootmuzzle();
				break;
			case Box_gametype.addbuttleMax:
				Instantiate (Resources.Load<GameObject> ("UIvoice/UIye"));
				other.transform.parent.GetComponent<Player>().Playerlife+=10;
				if(other.transform.parent.GetComponent<Player>().Playerlife>other.transform.parent.GetComponent<Player>().playermaxlife)
				{
					other.transform.parent.GetComponent<Player>().Playerlife=other.transform.parent.GetComponent<Player>().playermaxlife;
				}
				break;
			case Box_gametype.addexperience:
				other.transform.parent.GetComponent<Player>().addexperience(Plane_Gameclass.playerjinyandouble);
				break;
			case Box_gametype.addmoney:
				other.transform.parent.GetComponent<Player>().addmoney();
				break;
			case Box_gametype.Doubleexperience:
				Instantiate (Resources.Load<GameObject> ("UIvoice/UIye"));
				Plane_Gameclass.playerjinyandouble++;
				break;
			default:
				break;
			}
			Instantiate (Resources.Load<GameObject> ("BOX/boxvoice"));
			Destroy(gameObject);
				}
		}
}
