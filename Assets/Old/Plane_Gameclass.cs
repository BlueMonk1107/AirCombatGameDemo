using UnityEngine;
using System.Collections;

public class Plane_Gameclass  {
	public static int ATenemymount=0;//动态记录ATenemy敌机数量当归零时如果工厂中还有次数可以再次产生 
	public static int ATenemyboss=1;//动态记录Boss数量
	public static int playerDUNmount=2;//初始换盾牌数量
	public static int playerPower=2;//大招的数量
	public static int playermoneymount=0;//角色得到星星数量
	public static int playerjinyan = 0;//角色得到经验
	public static int Gamescore=0;//当前关卡的游戏分数
	public static int playerjinyandouble=1;//角色获得几倍经验
	public static int playerleading = 1;//游戏主角
	public static int gamelevel = 1;//游戏关卡
	public static int levelpassmount=20;//当前可以进行的游戏关卡
	public static float playerMaxlife=100;//玩家的生命
	public static float playerdamage=5;//玩家的伤害
	public static float playerAttackspped=0.2f;//玩家速度
}
