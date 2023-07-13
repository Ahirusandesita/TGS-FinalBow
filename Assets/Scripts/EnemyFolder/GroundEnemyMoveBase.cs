// --------------------------------------------------------- 
// GroundMobMoveBase.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// 地上雑魚の挙動の基盤
/// </summary>
public class GroundEnemyMoveBase : EnemyMoveBase
{
    private enum CrabWalkState { left, right };

    public enum JumpDirectionState { zero, one, two, three, four, five, six, seven, eight, nine, ten, eleven, twelve };
    public JumpDirectionState _jumpDirectionState;

    private int _crabWalkDirection = 0;


    public float _crabWalkingSpeed = default;

    public float _jumpPower = 150f;
    private float _jumpPowerMax;

    CrabWalkState _crabWalk;

    private bool _isJump = false;

    private struct JumpDirection
    {
        public float X;
        public float Z;
    }
    private JumpDirection _jumpDirection;

    private GroundEnemyAttack _groundEnemyAttack = default;


    protected override void Start()
    {
        base.Start();

        _crabWalk = CrabWalkState.left;
        WalkDirectionState();
        _jumpPowerMax = _jumpPower;

        _groundEnemyAttack = this.GetComponent<GroundEnemyAttack>();
    }

    private void Update()
    {
        MoveSequence();
    }


    protected override void MoveSequence()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _groundEnemyAttack.Attack(PoolEnum.PoolObjectType.groundBullet, _transform);
        }

        if (Input.GetKey(KeyCode.J))
        {
            _isJump = true;
        }
        else
        {
            _isJump = false;
        }

        //CrabWalk();
        Jump();
    }

    private void Jump()
    {
        if (_isJump)
        {
            _transform.Translate(40f * _jumpDirection.X * Time.deltaTime, _jumpPower * Time.deltaTime, 40f * _jumpDirection.Z * Time.deltaTime);
            _jumpPower -= 200f * Time.deltaTime;
        }
    }

    private void CrabWalk()
    {
        _transform.Translate(_crabWalkingSpeed * _crabWalkDirection * Time.deltaTime, 0f, 0f);
    }

    private void Stop()
    {

    }

    private void WalkDirectionState()
    {
        if (_crabWalk == CrabWalkState.left)
        {
            _crabWalkDirection = 1;
        }
        else if (_crabWalk == CrabWalkState.right)
        {
            _crabWalkDirection = -1;
        }
    }

    private void JumpDirectionSetting()
    {
        switch (_jumpDirectionState)
        {
            case JumpDirectionState.zero:
                _jumpDirection.X = 0f;
                _jumpDirection.Z = 0f;
                break;
            case JumpDirectionState.one:
                _jumpDirection.X = 0.2886f;
                _jumpDirection.Z = 0.5f;
                break;
            case JumpDirectionState.two:
                _jumpDirection.X = 0.5f;
                _jumpDirection.Z = 0.2886f;
                break;
            case JumpDirectionState.three:
                _jumpDirection.X = 1f;
                _jumpDirection.Z = 0f;
                break;
            case JumpDirectionState.four:
                _jumpDirection.X = 0.5f;
                _jumpDirection.Z = -0.2886f;
                break;
            case JumpDirectionState.five:
                _jumpDirection.X = 0.2886f;
                _jumpDirection.Z = -0.5f;
                break;
            case JumpDirectionState.six:
                _jumpDirection.X = 0f;
                _jumpDirection.Z = -1f;
                break;
            case JumpDirectionState.seven:
                _jumpDirection.X = -0.2886f;
                _jumpDirection.Z = -0.5f;
                break;
            case JumpDirectionState.eight:
                _jumpDirection.X = -0.5f;
                _jumpDirection.Z = -0.2886f;
                break;
            case JumpDirectionState.nine:
                _jumpDirection.X = -1f;
                _jumpDirection.Z = 0f;
                break;
            case JumpDirectionState.ten:
                _jumpDirection.X = -0.5f;
                _jumpDirection.Z = 0.2886f;
                break;
            case JumpDirectionState.eleven:
                _jumpDirection.X = -0.2886f;
                _jumpDirection.Z = 0.5f;
                break;
            case JumpDirectionState.twelve:
                _jumpDirection.X = 0f;
                _jumpDirection.Z = 1f;

                break;

        }
    }

    public override void OrignalOnCollisionEnter_HitFloor()
    {
        JumpDirectionSetting();
        _jumpPower = _jumpPowerMax;
        _isJump = false;
    }

    public override void OrignalOnCollisionEnter_HitWall()
    {
        Debug.Log("Hit"!);
        if (_crabWalk == CrabWalkState.left)
        {
            _crabWalk = CrabWalkState.right;
        }
        else
        {
            _crabWalk = CrabWalkState.left;
        }
        WalkDirectionState();
    }



    // 垂直ジャンプ
    // 前方に歩いてくる
    // 攻撃する（別スクリプトから呼ぶので保留）
    // 止まる
    // ↑↑↑それぞれ関数にまとめる
    // MoveSequenceは一旦ほっといていいよ
}