// --------------------------------------------------------- 
// TestBossIdleMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestBossIdleMove : NewTestBossMoveBase
{
    Vector2 _moveDirection = Vector2.one;

    float _cacheMoveTimeLine = 0;

    float _sideCache = 0;

    float _virticalCache = 0;

    float _cacheActionTime = 0;

    [SerializeField] float idlingTime = 3f;

    [SerializeField] float idleSideTime = 0.5f;
    [SerializeField] float idleSideSpeed = 1f;
    [SerializeField] float idleUpTime = 1f;
    [SerializeField] float idleUpSpeed = 0.3f;

    protected override void Start()
    {
        base.Start();
        Init();

    }

    protected override void MoveAnimation()
    {
        Init();
    }

    protected override void MoveProcess()
    {
        
        ActionMove();
    }

    public void ActionMove()
    {

        _cacheMoveTimeLine += Time.deltaTime;

        if (_sideCache + idleSideTime < _cacheMoveTimeLine)
        {
            _sideCache = _cacheMoveTimeLine;
            _moveDirection.x *= -1;
        }

        if (_virticalCache + idleUpTime < _cacheMoveTimeLine)
        {
            _virticalCache = _cacheMoveTimeLine;
            _moveDirection.y *= -1;
        }

        Vector2 _moveSpeed = new Vector2(_moveDirection.x * idleSideSpeed, _moveDirection.y * idleUpSpeed);

        transform.Translate(_moveSpeed * Time.deltaTime);

    }

    private void Init()
    {
        _cacheActionTime = 0;
        _sideCache = 0;
        _virticalCache = 0;
    }

}