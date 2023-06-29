// --------------------------------------------------------- 
// BirdMoveFirst.cs 
// 
// CreateDay: 2023/06/21
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System;

public class BirdMoveFirst : BirdMoveBase
{
    #region 変数
    [Tooltip("現在の経過時間（攻撃までの頻度に使う）")]
    private float _currentTime = 0f;

    [Tooltip("現在の経過時間（再び動き出すまでの時間に使う）")]
    private float _currentTime2 = 0f;

    [Tooltip("次の移動完了で処理終了")]
    private bool _isLastMove = default;

    [Tooltip("次のゴール設定の回数")]
    private int _nextSetGoalCount = 1;  // 初期値1

    [Tooltip("攻撃の頻度")]
    private const float ATTACK_INTERVAL_TIME = 2f;
    #endregion


    protected override void OnEnable()
    {
        base.OnEnable();

        _isLastMove = false;
        _currentTime = 0f;
        _currentTime2 = 0f;
    }

    protected override void Start()
    {
        base.Start();

        //SetGoalPositionCentral();
        SetGoalPosition(_spawnedWave, _thisInstanceIndex, howManyTimes: _nextSetGoalCount);
        _nextSetGoalCount++;

        // もしInspectorで設定ミスがあったら仮設定する
        if (_linearMovementSpeed == 0f)
        {
            _linearMovementSpeed = 20f;
            X_Debug.LogError("EnemySpawnPlaceData.Speed が設定されてません");
        }

        if (_reAttackTime == 0f)
        {
            _reAttackTime = 5f;
            X_Debug.LogError("EnemySpawnPlaceData.WaitTime_s が設定されてません");
        }
    }


    public override void MoveSequence()
    {
        // 麻痺状態か判定する（麻痺だったら動かない）
        if (Paralysing())
        {
            return;
        }

        _currentTime += Time.deltaTime;

        // 移動処理（移動が完了していたらこのブロックは無視される）-----------------------------------------------------------

        if (!IsFinishMovement)
        {
            LinearMovement();

            return;
        }
        else
        {
            // 最後の移動が終了したら、デスポーンさせる
            if (_isLastMove)
            {
                _needDespawn = true;

                // クラスをはがす
                Destroy(this);

                return;
            }
        }

        // 攻撃処理（一定間隔で実行される）-----------------------------------------------------------------------------------

        if (_currentTime >= ATTACK_INTERVAL_TIME)
        {
            // 攻撃前にプレイヤーの方向を向く
            if (_transform.rotation != _childSpawner.rotation)
            {
                RotateToPlayer();
                return;
            }

            //　攻撃を実行
            _birdAttack.NormalAttack(_childSpawner, ConversionToBulletType(), _numberOfBullet);
            _currentTime = 0f;
        }

        _currentTime2 += Time.deltaTime;

        // 再移動のためのリセット処理（攻撃がスタートしてから一定時間後に実行）-----------------------------------------------

        // 攻撃が終了
        if (_currentTime2 >= _reAttackTime)
        {
            // 設定されたゴールの数が1のとき AND すべてのゴールが設定されたら、次の行動が最後
            if (_numberOfGoal == 1 && _nextSetGoalCount > _numberOfGoal)
            {
                // 次の移動が最後
                _isLastMove = true;

                return;
            }

            // 再移動前に正面を向く
            if (_transform.rotation != FRONT_ANGLE)
            {
                RotateToFront();
                return;
            }

            IsFinishMovement = false;

            // スタート位置とゴール位置の再設定
            _startPosition = _transform.position;
            SetGoalPosition(_spawnedWave, _thisInstanceIndex, howManyTimes: _nextSetGoalCount);
            _nextSetGoalCount++;

            // すべてのゴールが設定されたら、次の行動が最後
            if (_nextSetGoalCount > _numberOfGoal)
            {
                _isLastMove = true;
            }

            _currentTime2 = 0f;
        }
    }
}