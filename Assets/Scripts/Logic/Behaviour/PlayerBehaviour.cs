using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IBehaviour
{
    public void Injure(int value)
    {
        if(GameModel.Single.Life == 0)
            return;
        
        var life = GameModel.Single.Life - value;
        if (life <= 0)
        {
            GameModel.Single.Life = 0;
            Dead();
        }
        else
        {
            GameModel.Single.Life = life;
        }

        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_HP);
    }

    public void Dead()
    {
        if(GameModel.Single.Life < 0)
            return;

        //标记已经执行过死亡方法
        GameModel.Single.Life = -1;
        MessageMgr.Single.DispatchMsg(MsgEvent.EVENT_END_GAME);
        AniMgr.Single.PlaneDestroyAni(transform.position);
        Destroy(gameObject);
        AudioMgr.Single.PlayOnce(GameAudio.Explode_Plane.ToString());
    }
}