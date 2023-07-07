// --------------------------------------------------------- 
// BirdMoveSecond.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BirdMoveSecond : BirdMoveBase
{
    #region variable 

    const float SIDE_MOVE_SPEED = 12f;

    const float MOVE_SPEED_ARC = 10f;

    const float PARABORA_ANGLE = 10f;

    const float PERCENT_HALF = 0.5f;

    const float VIEW_PORT_MIN = 0f;

    const float VIEW_PORT_MAX = 1f;

    Vector3 _sideMoveNormalizedVector = default;

    Vector3 _movingSide = default;

    Vector3 objectViewPoint = default;

    Vector3 arcDirection = default;

    float _movingArcDistance = default;

    float _cacheMoveValue = default;

    float _finishMoveValue = default;

    float percentMoveDistance = default;
 

    enum ArcMoveDirection
    {
        Up,
        Down,
        Front,
        Back
    }

    ArcMoveDirection arcMoveDirection = ArcMoveDirection.Front;

    #endregion
    #region property
    #endregion
    #region method

    protected override void Start()
    {
        base.Start();

        InitializeVariables();

        //SetGoalPosition(WaveType.zakoWave2, _thisInstanceIndex);

        // 最初から正面を向かせる
        //_transform.rotation = FRONT_ANGLE;
        // 横移動ベクトル
        _sideMoveNormalizedVector = GetSideMoveVector().normalized;

        arcDirection = GetArcDirection(_sideMoveNormalizedVector);

        // 横移動量
        _finishMoveValue = GetSideMoveVector().magnitude;

    }

    // てすと
    //private void Awake()
    //{
    //    _startPosition = new Vector3(0, 0, 0);

    //    _goalPosition = new Vector3(-100, 0, 0);
    //}
    protected void InitializeVariables()
    {

        _sideMoveNormalizedVector = default;

        _movingSide = default;

        _movingArcDistance = default;

        _cacheMoveValue = default;

        _finishMoveValue = default;

        percentMoveDistance = default;

    }

    private Vector3 GetArcDirection(Vector3 moveDirection)
    {
        Vector3 dir;
        // 弧の方向を決める
        dir = arcMoveDirection switch
        {
            ArcMoveDirection.Up => Vector3.Cross(moveDirection,Vector3.left),
            ArcMoveDirection.Down => Vector3.Cross(moveDirection,Vector3.right),
            ArcMoveDirection.Front => Vector3.Cross(moveDirection,Vector3.down),
            ArcMoveDirection.Back => Vector3.Cross(moveDirection,Vector3.up),
            _ => Vector3.up,
        };

        return dir.normalized;
    }

    protected override void EachMovement(ref float movedDistance)
    {
        base.EachMovement(ref movedDistance);

        // 移動処理
        ArcMove();

        movedDistance += _movingSide.magnitude;
    }

    // スタートからゴールへいく
    // 左行き:下　右行き:上
    //public override void MoveSequence()
    //{
    //    if (Paralysing())
    //    {
    //        return;
    //    }

    //    if (!_needDespawn)
    //    {
    //        // 向く処理
    //        RotateToPlayer();
    //        // 移動処理
    //        ArcMove();
    //        // チェック移動エンド
    //        _needDespawn = CheckMoveFinish();

    //        if (_canStartAttack && AttackCheck())
    //        {
    //            _canStartAttack = false;

    //            //StartCoroutine(_birdAttack.NormalAttackLoop(_childSpawner, ConversionToBulletType(), _numberOfBullet));
    //        }

    //        return;
    //    }
    //    // 移動終了時
    //    else
    //    {
    //        _transform.position = _goalPosition;
    //    }

    //}

    /// <summary>
    /// アタックの条件を記述
    /// </summary>
    /// <returns>アタックできるならtrue</returns>
    private bool AttackCheck()
    {
        objectViewPoint = Camera.main.WorldToViewportPoint(_transform.position);

        return IsBetween(objectViewPoint.x, VIEW_PORT_MIN, VIEW_PORT_MAX) &&
            (IsBetween(objectViewPoint.y, VIEW_PORT_MIN, VIEW_PORT_MAX) &&
            objectViewPoint.z > VIEW_PORT_MIN);

        bool IsBetween(float look, float min, float max) => min <= look && look <= max;
    }


    /// <summary>
    /// カメラのポジションから見て左に行くか調べる
    /// </summary>
    /// <returns>左行きならtrue</returns>
    private bool CheckDown()
    {
        // 敵からプレイヤーの方向ベクトル
        Vector3 enemyToPlayerVector = Camera.main.transform.position - _transform.position;

        // 前方ベクトルと敵とプレイヤー方向ベクトルの外積をもとめる
        Vector3 cross = Vector3.Cross(_transform.forward, enemyToPlayerVector);

        // 外積から左右判定
        if (cross.y < 0)
        {
            // 敵から見て右にプレイヤーがいるので下に行く
            return true;
        }

        return false;
    }

    /// <summary>
    /// 横移動のベクトルをもとめる
    /// </summary>
    /// <returns>横移動ベクトル</returns>
    private Vector3 GetSideMoveVector()
    {
        return _goalPosition - _transform.position;
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void ArcMove()
    {
        // 直線移動
        _movingSide = SIDE_MOVE_SPEED * Time.deltaTime * _sideMoveNormalizedVector;
        // 累計横移動量のキャッシュ
        _cacheMoveValue += _movingSide.magnitude;

        _movingArcDistance = AddArc();

        _movingSide += arcDirection * _movingArcDistance;

        _transform.Translate(_movingSide, Space.World);

    }

    /// <summary>
    /// 現在の位置から弧の移動量をもとめる
    /// </summary>
    /// <returns>Y移動量</returns>
    private float AddArc()
    {
        percentMoveDistance = _cacheMoveValue / _finishMoveValue;

        // 前半の動き
        if (percentMoveDistance < PERCENT_HALF)
        {
            return MOVE_SPEED_ARC * PrabolaMove() * Time.deltaTime;
        }
        // 後半の動き
        else
        {
            return MOVE_SPEED_ARC * PrabolaMove() * Time.deltaTime;
        }

        float PrabolaMove()
        {
            // 放物線運動のax^2の部分
            return PARABORA_ANGLE * Mathf.Pow(PERCENT_HALF - percentMoveDistance, 2);

        }

    }
    /// <summary>
    /// 移動終わったか調べる
    /// </summary>
    /// <returns>ゴール着いたらtrue</returns>
    private bool CheckMoveFinish()
    {
        return _finishMoveValue < _cacheMoveValue;
    }


    #endregion
}