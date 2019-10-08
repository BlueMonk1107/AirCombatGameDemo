using UnityEngine;
using System.Collections;

public class Enmeyfactory : MonoBehaviour {
	public AudioClip bossvioce;
	public enum enemyType{enemy,GZenemy,ATenemy,Bossenemy,Nothing}
	public enemyType statetype=enemyType.enemy;
	//enemy
	float time;
	bool factoryOK=false;
	public int jiaodu=90;
	public GameObject[] enemytype;

	//GZenemy
	public float enemytime=5f;//普通飞机产生时间间隔
	public int GZmount=5;//记录产生飞机的个数
	int enemy=0;//已经产生飞机的个数

	//ATenemy
	public GameObject[] ATenemytype;
	public int ATmount=3;//一次出现敌机的个数
	public int ATlevelmount=3;//出现几次ATenemy
	public int  mounthave=0;//已出现AT飞机的个数
	public int jiluATmount;//一次出现敌机的个数
	public int jiluATlevelmount;//出现几次ATenemy
	public int jilumounthave;//已出现AT飞机的个数

	//Bossenemy
	public GameObject[] Bossenemytype;
	public int bossmount=1;
	// Use this for initialization
	void Start () {
		jiluATmount = ATmount;
		jiluATlevelmount = ATlevelmount;
		jilumounthave = mounthave;
		Plane_Gameclass.ATenemyboss = bossmount;
	}
	
	// Update is called once per frame
	void Update () {
		time+=Time.deltaTime;
		switch (statetype) {
		case enemyType.enemy:
			if (time > enemytime) {
				factoryOK=true;
				time=0;
			}
			if (factoryOK) {
				Instantiate (enemytype[Random.Range(0,enemytype.Length)], new Vector3(Random.Range(-1.9f,1.9f),4.5f,0), Quaternion.identity);
				factoryOK=false;
			}
			break;
		case enemyType.GZenemy:
			if (time > enemytime) {
				factoryOK=true;
				time=0;
			}
			if (factoryOK) {
				Instantiate (enemytype[Random.Range(0,enemytype.Length)]);
				factoryOK=false;
			}
			break;
		case enemyType.ATenemy:
			if (time > 4f) {
				factoryOK=true;
				time=0;
			}
			if (factoryOK) {
				Instantiate (ATenemytype[Random.Range(0,ATenemytype.Length)], new Vector3(Random.Range(-1.9f,1.9f),4.5f,0), Quaternion.identity);
				factoryOK=false;
				mounthave++;
				Plane_Gameclass.ATenemymount++;
				if(mounthave==ATmount)
				{
					ATlevelmount--;
					if(ATlevelmount<0)
					{
						statetype=enemyType.Bossenemy;
					}
					else
					{
						statetype=enemyType.Nothing;
					}
				}
			}
			break;
		case enemyType.Bossenemy:
			if (time > 4f) {
				factoryOK=true;
				time=0;
			}
			if (Plane_Gameclass.ATenemymount==0&&ATlevelmount<0) {
				GameObject.FindObjectOfType<initMain_level>().GetComponent<AudioSource>().clip=bossvioce;
				GameObject.FindObjectOfType<initMain_level>().GetComponent<AudioSource>().Play();
				chuxianboss();
				statetype=enemyType.Nothing;
			}
			break;
		case enemyType.Nothing:
			if(ATlevelmount>=0&&Plane_Gameclass.ATenemymount==0)
			{
				mounthave=0;
				statetype=enemyType.ATenemy;
			}
			break;
				}
	}
	//新的Boss来了
	public void newbosslaile()
	{
		ATmount = jiluATmount;
		ATlevelmount = jiluATlevelmount;
		mounthave = jilumounthave;
		Plane_Gameclass.ATenemymount = 0;
	}
	void chuxianboss()
	{
 		print (Plane_Gameclass.ATenemyboss);
		Instantiate (Bossenemytype[Plane_Gameclass.ATenemyboss-1], new Vector3(0,4.5f,0), Quaternion.identity);
	}
}
