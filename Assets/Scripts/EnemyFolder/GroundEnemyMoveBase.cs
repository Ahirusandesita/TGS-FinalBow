// --------------------------------------------------------- 
// GroundMobMoveBase.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

interface IFNeedMoveRotineEnd
{
    void MoveEnd();
}
/// <summary>
/// 地上雑魚の挙動の基盤
/// </summary>
public class GroundEnemyMoveBase : EnemyMoveBase,IFNeedMoveRotineEnd
{
    private Animator myAnimation;
    private enum CrabWalkState { left, right };

    public enum JumpDirectionState { zero, one, two, three, four, five, six, seven, eight, nine, ten, eleven, twelve };
    public JumpDirectionState _jumpDirectionState;

    private int _crabWalkDirection = 0;


    public float _crabWalkingSpeed = default;

    public float _jumpPower = 150f;
    private float _jumpPowerMax;

    CrabWalkState _crabWalk;

    private bool _isJump = false;

    Transform wormSandTransform = default;
    Transform wormGroundTransform = default;
    Transform wormJumpUpTransform = default;

    Coroutine moveRoutine = default;

    //private string[,] WormAnimationTrrigers
    //{
    //    { low,high,}
    //}

    private struct JumpDirection
    {
        public float X;
        public float Z;
    }
    private JumpDirection _jumpDirection;

    private GroundEnemyAttack _groundEnemyAttack = default;


    private float _currentTime = 0f;
    private float _currentTime2 = 0f;
    [HideInInspector]
    public float _reAttackTime_s = default;
    private bool _completedAttack = true;
    [HideInInspector]
    public float _despawnTime_s = default;
    [HideInInspector]
    public bool _needDespawn = false;

    private Vector3 startTransform;
    [SerializeField]
    private float moveMinusSpeed = 0f;
    private float moveMaxMinusSpeed = 0f;

    private bool isJumpUp = true;
    private bool canJumpStop = true;

    private BoxCollider myCollider;

    [SerializeField]
    private float stopTime;

    public enum AttackType
    {
        /// <summary>
        /// 一回
        /// </summary>
        once,
        /// <summary>
        /// 連発
        /// </summary>
        consecutive,
    }
    [HideInInspector]
    public AttackType _attackType = default;


    private GroundEnemyDataTable _groundEnemyData;
    public GroundEnemyDataTable GroundEnemyData { set => _groundEnemyData = value; }

    private GroundEnemyStats _genemyStats = default;

    protected override void Start()
    {
        moveMaxMinusSpeed = moveMinusSpeed;
        _crabWalk = CrabWalkState.left;
        WalkDirectionState();
        _jumpPowerMax = _jumpPower;
        myAnimation = this.GetComponent<Animator>();
        _groundEnemyAttack = this.GetComponent<GroundEnemyAttack>();
        myCollider = this.GetComponent<BoxCollider>();
        _genemyStats = this.GetComponent<GroundEnemyStats>();

        base.Start();
    }

    public void InitializeOnEnable()
    {
        _currentTime = 0f;
        _currentTime2 = 0f;
        _needDespawn = false;
        startTransform = this.transform.position;
        isOnePlay = true;

        wormGroundTransform = this.transform.GetChild(0).GetChild(0).transform;
        wormSandTransform = this.transform.GetChild(0).GetChild(1).transform;
        wormJumpUpTransform = this.transform.GetChild(0).GetChild(2).transform;
        wormSandTransform.gameObject.SetActive(false);
        wormGroundTransform.gameObject.SetActive(false);
        myCollider.enabled = false;

        _transform.LookAt(GameObject.FindWithTag("PlayerController").transform);
    }

    public void MoveEnd()
    {
        if(moveRoutine is not null)
        {
            StopCoroutine(moveRoutine);
        }
    }

    bool isOnePlay = true;
    protected override void MoveSequence()
    {
        if (isOnePlay)
        {
            wormSandTransform.gameObject.SetActive(false);
            wormGroundTransform.gameObject.SetActive(false);
            wormJumpUpTransform.gameObject.SetActive(false);
            moveRoutine = StartCoroutine(WormAction());
            myCollider.enabled = false;
            isOnePlay = false;
        }

        //_transform.Translate(0f/*40f * _jumpDirection.X * Time.deltaTime*/, _jumpPower * Time.deltaTime, 0f/* 40f * _jumpDirection.Z * Time.deltaTime*/);
        //if (isJumpUp)
        //    _jumpPower -= moveMinusSpeed * Time.deltaTime;

        //if(_jumpPower < 0f && canJumpStop)
        //{
        //    _jumpPower = 0;
        //    StartCoroutine(JumpStop());
        //    canJumpStop = false;
        //}

        //if (this.transform.position.y < startTransform.y)
        //{
        //    this.transform.position = startTransform;
        //    _jumpPower = _jumpPowerMax;
        //    canJumpStop = true;
        //}

        //_currentTime += Time.deltaTime;
        //_currentTime2 += Time.deltaTime;

        //if (_currentTime >= _reAttackTime_s && _completedAttack)
        //{
        //    switch (_attackType)
        //    {
        //        case AttackType.once:
        //            _groundEnemyAttack.ThrowingAttack(_transform);
        //            break;

        //        case AttackType.consecutive:
        //            StartCoroutine(ConsecutiveAttack());
        //            break;
        //    }

        //    _currentTime = 0f;
        //}

        //if (_currentTime2 >= _despawnTime_s)
        //{
        //    _needDespawn = true;
        //}

        //CrabWalk();
        //Jump();
    }
    IEnumerator JumpStop()
    {
        isJumpUp = false;
        yield return new WaitForSeconds(stopTime);
        isJumpUp = true;
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

    /// <summary>
    /// 連続攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator ConsecutiveAttack()
    {
        _completedAttack = false;

        int count = 0;
        while (count < 3)
        {
            _groundEnemyAttack.ThrowingAttack(_transform);
            count++;

            yield return new WaitForSeconds(0.5f);
        }

        _completedAttack = true;
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

    //public override void OrignalOnCollisionEnter_HitFloor()
    //{
    //    JumpDirectionSetting();
    //    _jumpPower = _jumpPowerMax;
    //    _isJump = false;
    //}

    //public override void OrignalOnCollisionEnter_HitWall()
    //{
    //    Debug.Log("Hit"!);
    //    if (_crabWalk == CrabWalkState.left)
    //    {
    //        _crabWalk = CrabWalkState.right;
    //    }
    //    else
    //    {
    //        _crabWalk = CrabWalkState.left;
    //    }
    //    WalkDirectionState();
    //}



    // 垂直ジャンプ
    // 前方に歩いてくる
    // 攻撃する（別スクリプトから呼ぶので保留）
    // 止まる
    // ↑↑↑それぞれ関数にまとめる
    // MoveSequenceは一旦ほっといていいよ

    private IEnumerator WormAction()
    {
        wormSandTransform.gameObject.SetActive(true);
        yield return new WaitForSeconds(_groundEnemyData._spawnTime_s);

        if (_groundEnemyData._onlySandDust)
        {
            _genemyStats.Despawn();
            yield break;
        }

        myAnimation.SetTrigger("RiseHigh");
        wormGroundTransform.gameObject.SetActive(true);
        wormSandTransform.gameObject.SetActive(false);
        wormJumpUpTransform.gameObject.SetActive(true);
        myCollider.enabled = true;
        yield return new WaitForSeconds(3f);
        wormJumpUpTransform.gameObject.SetActive(false);
        yield return new WaitForSeconds(_groundEnemyData._appearanceKeep_s - 3f);
        myAnimation.SetTrigger("HideHigh");
        yield return new WaitForSeconds(1.4f);
        wormSandTransform.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        wormGroundTransform.gameObject.SetActive(false);
        myCollider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        wormSandTransform.gameObject.SetActive(false);

        _genemyStats.Despawn();
    }
}