using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IBehaviour
{
    public void Injure(int value)
    {
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
        //todo:播放爆炸特效
        Destroy(gameObject);
        AudioMgr.Single.PlayOnce(GameAudio.Explode_Plane.ToString());
    }
}