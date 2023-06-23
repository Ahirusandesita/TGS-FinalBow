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

    [Tooltip("現在の経過時間（再び動き出すまでの時間に使う）")]
    private float _currentTime2 = 0f;

    [Tooltip("攻撃の頻度")]
    private const float ATTACK_INTERVAL_TIME = 2f;

    [Tooltip("回転後のブレイク時間")]
    private const float AFTER_ROTATE_BREAK_TIME = 0.2f;

    [Tooltip("正面の角度")]
    private readonly Quaternion FRONT_ANGLE = Quaternion.Euler(new Vector3(0f, 180f, 0f));

    [Tooltip("次の移動完了で処理終了")]
    private bool _isLastMove = default;
    #endregion


    protected override void OnEnable()
    {
        base.OnEnable();

        SetGoalPositionCentral();
        // 最初から正面を向かせる
        _transform.rotation = FRONT_ANGLE;
        _isLastMove = false;
    }


    public override void MoveSequence()
    {
        _currentTime += Time.deltaTime;

        // 移動が完了するまでループ
        if (!IsFinishMovement)
        {
            LinearMovement();

            return;
        }
        else
        {
            // 最後の移動が終了したら、このメソッドを抜ける
            if (_isLastMove)
            {
                return;
            }
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

        // 一定時間経ったら移動再開
        if (_currentTime2 >= 10f)
        {
            SetGoalPosition(WaveType.zakoWave1);
            IsFinishMovement = false;
            _isLastMove = true;
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