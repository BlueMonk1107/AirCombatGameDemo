using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MoveComponent _move;

    //飞机中心点到边界的差值
    private Vector2 _offset;
    private SpriteRenderer _renderer;

    // Use this for initialization
    private void Start()
    {
        _move = GetComponent<MoveComponent>();
        _renderer = GetComponent<SpriteRenderer>();
        InputMgr.Single.AddListener(KeyCode.W);
        InputMgr.Single.AddListener(KeyCode.A);
        InputMgr.Single.AddListener(KeyCode.S);
        InputMgr.Single.AddListener(KeyCode.D);

        MessageMgr.Single.AddListener(KeyCode.W, InputState.PREE, ReveiveW);
        MessageMgr.Single.AddListener(KeyCode.A, InputState.PREE, ReveiveA);
        MessageMgr.Single.AddListener(KeyCode.S, InputState.PREE, ReveiveS);
        MessageMgr.Single.AddListener(KeyCode.D, InputState.PREE, ReveiveD);
        InitData();
    }

    private void InitData()
    {
        _offset = transform.position - _renderer.bounds.min;
    }

    private void OnDestroy()
    {
        InputMgr.Single.RemoveListener(KeyCode.W);
        InputMgr.Single.RemoveListener(KeyCode.A);
        InputMgr.Single.RemoveListener(KeyCode.S);
        InputMgr.Single.RemoveListener(KeyCode.D);

        MessageMgr.Single.RemoveListener(KeyCode.W, InputState.PREE, ReveiveW);
        MessageMgr.Single.RemoveListener(KeyCode.A, InputState.PREE, ReveiveA);
        MessageMgr.Single.RemoveListener(KeyCode.S, InputState.PREE, ReveiveS);
        MessageMgr.Single.RemoveListener(KeyCode.D, InputState.PREE, ReveiveD);
    }

    public void ReveiveW(params object[] args)
    {
        if (!JudgeUpBorder()) _move.Move(Vector2.up);
    }

    public void ReveiveA(params object[] args)
    {
        if (!JudgeLeftBorder()) _move.Move(Vector2.left);
    }

    public void ReveiveS(params object[] args)
    {
        if (!JudgeDownBorder()) _move.Move(Vector2.down);
    }

    public void ReveiveD(params object[] args)
    {
        if (!JudgeRightBorder()) _move.Move(Vector2.right);
    }

    private bool JudgeUpBorder()
    {
        return _renderer.bounds.max.y >= GameUtil.GetCameraMax().y;
    }

    private bool JudgeDownBorder()
    {
        return _renderer.bounds.min.y <= GameUtil.GetCameraMin().y;
    }

    private bool JudgeLeftBorder()
    {
        return _renderer.bounds.min.x <= GameUtil.GetCameraMin().x;
    }

    private bool JudgeRightBorder()
    {
        return _renderer.bounds.max.x >= GameUtil.GetCameraMax().x;
    }

    private void OnMouseDrag()
    {
#if UNITY_EDITOR
        Drag(Input.mousePosition);
#else
		if (Input.touches.Length > 0)
		{
			Drag(Input.touches[0].position);
		}
#endif

        if (JudgeUpBorder())
            ResetPosY(GameUtil.GetCameraMax(), Vector2.up);
        else if (JudgeDownBorder()) ResetPosY(GameUtil.GetCameraMin(), Vector2.down);

        if (JudgeLeftBorder())
            ResetPosX(GameUtil.GetCameraMin(), Vector2.left);
        else if (JudgeRightBorder()) ResetPosX(GameUtil.GetCameraMax(), Vector2.right);
    }

    private void ResetPosX(Vector2 border, Vector2 direction)
    {
        var pos = transform.localPosition;
        pos.z = 0;
        pos.x = border.x - Vector2.Dot(_offset, direction);
        transform.localPosition = pos;
    }

    private void ResetPosY(Vector2 border, Vector2 direction)
    {
        var pos = transform.localPosition;
        pos.z = 0;
        pos.y = border.y - Vector2.Dot(_offset, direction);
        transform.localPosition = pos;
    }

    private void Drag(Vector3 screenPos)
    {
        var pos = Camera.main.ScreenToWorldPoint(screenPos);
        pos.z = 0;
        transform.localPosition = pos;
    }
}