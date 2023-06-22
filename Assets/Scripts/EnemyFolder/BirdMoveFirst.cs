// --------------------------------------------------------- 
// BirdMoveFirst.cs 
// 
// CreateDay: 2023/06/21
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public class BirdMoveFirst : BirdMoveBase
{
    #region 変数
    [Tooltip("現在の経過時間（攻撃までの頻度に使う）")]
    private float _currentTime = 0f;

    [Tooltip("現在の経過時間（回転後のブレイクに使う）")]
    private float _currentTime2 = 0f;

    [Tooltip("攻撃の頻度")]
    private const float ATTACK_INTERVAL_TIME = 2f;

    [Tooltip("回転後のブレイク時間")]
    private const float AFTER_ROTATE_BREAK_TIME = 0.2f;
    #endregion


    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeVariables();
        // 最初から正面を向かせる
        _transform.rotation = Quaternion.Euler(Vector3.up * 180f);
    }

    protected override void InitializeVariables()
    {
        _needRotateTowardDirectionOfTravel = false;
    }

    public override void MoveSequence()
    {
        _currentTime += Time.deltaTime;

        // 移動が完了するまでループ
        if (!_isFinishMovement)
        {
            LinearMovement(_goalPosition);

            return;
        }

        // 一定間隔で攻撃
        if (_currentTime >= ATTACK_INTERVAL_TIME)
        {
            // 攻撃前にプレイヤーの方向を向く
            if (_transform.rotation != _childSpawner.rotation)
            {
                RotateToPlayer(_rotateToPlayerSpeed);
                return;
            }

            _birdAttack.NormalAttack(_childSpawner);
            _currentTime = 0f;
        }

        _currentTime2 += Time.deltaTime;

        if (_currentTime2 >= 10f)
        {

        }
    }

    //public override void MoveSequence()
    //{
    //    // 攻撃インターバル
    //    if (_currentTime >= ATTACK_INTERVAL_TIME)
    //    {
    //        // 攻撃前にプレイヤーの方向を向く
    //        if (_transform.rotation != _childSpawner.rotation)
    //        {
    //            RotateToPlayer(_rotateToPlayerSpeed);
    //            return;
    //        }

    //        // 回転が終わってから一定時間待機する
    //        if (_currentTime2 <= AFTER_ROTATE_BREAK_TIME)
    //        {
    //            _currentTime2 += Time.deltaTime;
    //            return;
    //        }

    //        _currentTime2 = 0f;

    //        // 攻撃を発射
    //        _birdAttack.NormalAttack(_childSpawner);

    //        _currentTime = 0f;
    //    }

    //    LinearMovement(_goalPosition);

    //    _currentTime += Time.deltaTime;
    //}
}