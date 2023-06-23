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

    const float SIDE_MOVE_SPEED = 30f;

    const float MOVE_SPEED_Y = 10f;

    const float PARABORA_ANGLE = 10f;

    const float PERCENT_HALF = 0.5f;

    Vector3 _sideMoveNormalizedVector = default;

    Vector3 _movingSide = default;

    float _movingY = default;

    float _cacheMoveValue = default;

    float _finishMoveValue = default;

    float percentMoveDistance = default;
    [Tooltip("1or-1")]
    float directionY = default;

    bool _canMoveDown = default;

    bool _IsVisible = default;

    #endregion
    #region property
    #endregion
    #region method

    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeVariables();

        //SetGoalPosition(WaveType.zakoWave2);

        // 最初から正面を向かせる
        _transform.rotation = Quaternion.Euler(Vector3.up * 180f);
        // 横移動ベクトル
        _sideMoveNormalizedVector = GetSideMoveVector().normalized;
        // 横移動量
        _finishMoveValue = GetSideMoveVector().magnitude;
        // 下か上か決める
        _canMoveDown = CheckDown();
    }

    // てすと
    private void Awake()
    {
        _startPosition = new Vector3(0, 0, 0);

        _goalPosition = new Vector3(100, 0, 0);
    }
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

        _IsVisible = false;

        IsFinishMovement = false;
    }

    // スタートからゴールへいく
    // 左行き:下　右行き:上
    public override void MoveSequence()
    {
        if (!IsFinishMovement)
        {
            // 向く処理
            RotateToPlayer(_rotateToPlayerSpeed);
            // 移動処理
            ArcMove();
            // チェック移動エンド
            IsFinishMovement = CheckMoveFinish();

            if (AttackCheck())
            {
                //_birdAttack.NormalAttack();
            }
            return;
        }
        // 移動終了時
        else
        {
            transform.position = _goalPosition;
        }

    }

    /// <summary>
    /// アタックの条件を記述
    /// </summary>
    /// <returns>アタックできるならtrue</returns>
    private bool AttackCheck()
    {
        return _IsVisible;
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

    private void OnBecameVisible()
    {
        _IsVisible = true;
    }
    #endregion
}