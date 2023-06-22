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

    Vector3 _sideMoveNormalizedVector = default;

    Vector3 _movingSide = default;

    float _movingY = default;

    float _sideMoveSpeed = default;

    float _moveSpeedY = default;

    float _cacheMoveValue = default;

    float _finishMoveValue = default;

    float percentMoveDistance = default;

    float directionY = default;

    bool _moveDown = default;

    #endregion
    #region property
    #endregion
    #region method

    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeVariables();

        // 最初から正面を向かせる
        _transform.rotation = Quaternion.Euler(Vector3.up * 180f);
        // 横移動ベクトル
        _sideMoveNormalizedVector = GetSideMoveVector().normalized;
        // 横移動量
        _finishMoveValue = GetSideMoveVector().magnitude;
        // 下か上か決める
        _moveDown = CheckDown();
    }

    // てすと
    private void Awake()
    {
        _startPosition = new Vector3(0, 0, 0);

        _goalPosition = new Vector3(100, 0, 0);
    }
    protected override void InitializeVariables()
    {
        _needRotateTowardDirectionOfTravel = false;

        _sideMoveNormalizedVector = default;

        _movingSide = default;

        _movingY = default;

        _sideMoveSpeed = 10f;

        _moveSpeedY = 1f;

        _cacheMoveValue = default;

        _finishMoveValue = default;

        percentMoveDistance = default;

        directionY = default;

        _moveDown = default;
    }

    // スタートからゴールへいく
    // 左行き:下　右行き:上
    public override void MoveSequence()
    {
        if (!_isFinishMovement)
        {
            // 移動処理
            ArcMove();
            // チェック移動エンド
            _isFinishMovement = CheckMoveFinish();

            return;
        }
        
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
        if(cross.y < 0)
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
        _movingSide = _sideMoveSpeed * Time.deltaTime * _sideMoveNormalizedVector;
        _cacheMoveValue += _movingSide.magnitude; 

        _movingY = MoveCalcY();

        _movingSide += Vector3.up * _movingY;
        print(_movingSide);

        _transform.Translate(_movingSide);

    }

    /// <summary>
    /// 現在の位置からYの移動量をもとめる
    /// </summary>
    /// <returns>Y移動量</returns>
    private float MoveCalcY()
    {
        percentMoveDistance = _cacheMoveValue / _finishMoveValue;

        directionY = 1f;

        if (_moveDown)
        {
            directionY = -1f;
        }

        if(percentMoveDistance < 0.5f)
        {
            return directionY * _moveSpeedY * Time.deltaTime;
        }
        else
        {
            return -directionY * _moveSpeedY * Time.deltaTime;
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