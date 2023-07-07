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

    const float MOVE_SPEED_Y = 10f;

    const float PARABORA_ANGLE = 10f;

    const float PERCENT_HALF = 0.5f;

    const float VIEW_PORT_MIN = 0f;

    const float VIEW_PORT_MAX = 1f;

    Vector3 _sideMoveNormalizedVector = default;

    Vector3 _movingSide = default;

    Vector3 objectViewPoint = default;

    float _movingY = default;

    float _cacheMoveValue = default;

    float _finishMoveValue = default;

    float percentMoveDistance = default;
    [Tooltip("1or-1")]
    float directionY = default;

    bool _canMoveDown = default;

    bool _canStartAttack = default;

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
        // 横移動量
        _finishMoveValue = GetSideMoveVector().magnitude;
        // 下か上か決める
        _canMoveDown = CheckDown();
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

        _movingY = default;

        _cacheMoveValue = default;

        _finishMoveValue = default;

        percentMoveDistance = default;

        directionY = default;

        _canMoveDown = false;

        _canStartAttack = true;
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
        return _goalPosition - _startPosition;
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void ArcMove()
    {
        _movingSide = SIDE_MOVE_SPEED * Time.deltaTime * _sideMoveNormalizedVector;
        // 累計横移動量のキャッシュ
        _cacheMoveValue += _movingSide.magnitude;

        _movingY = MoveCalcY();

        _movingSide += Vector3.up * _movingY;

        _transform.Translate(_movingSide, Space.World);

    }

    /// <summary>
    /// 現在の位置からYの移動量をもとめる
    /// </summary>
    /// <returns>Y移動量</returns>
    private float MoveCalcY()
    {
        percentMoveDistance = _cacheMoveValue / _finishMoveValue;

        directionY = 1f;

        if (_canMoveDown)
        {
            directionY = -1f;
        }

        // 前半の動き
        if (percentMoveDistance < PERCENT_HALF)
        {
            return directionY * MOVE_SPEED_Y * PrabolaMove() * Time.deltaTime;
        }
        // 後半の動き
        else
        {
            return -directionY * MOVE_SPEED_Y * PrabolaMove() * Time.deltaTime;
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