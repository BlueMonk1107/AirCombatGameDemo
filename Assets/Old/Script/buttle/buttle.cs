using UnityEngine;
using System.Collections;

public class buttle : MonoBehaviour {
	public float speed;
	public float buttlePower=5;//子弹的威力
	public bool player=true;//用于标记子弹是敌机还是玩家
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime*speed);
		buttlelimit ();
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Dun" && !player) {
			Destroy(gameObject);
			Instantiate (Resources.Load<GameObject> ("preinstall_butlle/buttlevoice 1"));
				}
		if (other.tag == "player"&&!player) {
			other.transform.parent.GetComponent<Player>().Playerdamage(transform.position,buttlePower);
			Destroy(gameObject);
		}
		if (other.tag == "enemy"&&player) {
			other.GetComponent<Enemy>().Enemydamage(transform.position,buttlePower);
			//other.GetComponent<Enemy>().EnemyDestory();
			Instantiate (Resources.Load<GameObject> ("preinstall_butlle/buttlevoice 1"));
			Destroy(gameObject);
		}
	}
	//用于限制子弹超出摄像机删除
	protected void buttlelimit()
	{
		if (transform.position.x < -3f||transform.position.x > 3f||transform.position.y > 4.5f||transform.position.y < -4.5f) {
			Destroy(gameObject);
		}
	}

}
