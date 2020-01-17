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
        //AniMgr.Single.PlaneDestroyAni(transform.position);
        Destroy(gameObject);
        AudioMgr.Single.PlayOnce(GameAudio.Explode_Plane.ToString());
    }
}