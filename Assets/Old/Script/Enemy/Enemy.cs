using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float speed=0.1f;
	public float enemylife=100;
	public int moneymount=20;//默认产生金币的个数
	protected float hitTime=0;//记录击中的时间
	protected float hitpiancha=0;//攻击偏差
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		hitTime += Time.deltaTime;
		transform.Translate (new Vector3(0,-1,0)*speed,Space.World);
		buttlelimit ();
	}
	void OnTriggerEnter2D(Collider2D other) {
	}
	//用于限制飞机,超出摄像机删除
	protected void buttlelimit()
	{
		if (transform.position.x < -4f||transform.position.x > 4f||transform.position.y > 6f||transform.position.y < -6f) {
			Destroy(gameObject);
		}
	}
	//自己被打中
	public  void Enemydamage(Vector3 postion,float buttlePower)
	{
		if (hitTime > 0.2f) {
			GameObject a;
			a=Instantiate (Resources.Load<GameObject> ("Hited/Hit1"), postion+new Vector3(0,hitpiancha,0), Quaternion.Euler(new Vector3(0,0,(Random.Range(0,360)))))as GameObject;
			a.transform.parent=transform;
			hitTime=0;
				}
			enemylife -= buttlePower;
			if (enemylife < 0) {
					EnemyDestory();
				}
			
	}
	public virtual void EnemyDestory()
	{
			for(int i=0;i<moneymount;i++)
			{
				Instantiate (Resources.Load<GameObject> ("BOX/BOX_addmoney"),transform.position+new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0),Quaternion.identity);
			}
			Instantiate (Resources.Load<GameObject> ("Hited/Destoryed"),transform.position,Quaternion.identity);
			Destroy (gameObject);
	}
}
