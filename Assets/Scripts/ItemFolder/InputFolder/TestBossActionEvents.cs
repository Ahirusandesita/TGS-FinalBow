// --------------------------------------------------------- 
// TestBossActionEvents.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System;
public class TestBossActionEvents : TestBossActionBase
{
    [Tooltip("攻撃回数")]
    [SerializeField] int _numberOfAttackRepeatTimes;
    [Tooltip("休憩時間")]
    [SerializeField] float _attackStopTime;
    [Tooltip("流星休憩時間")]
    [SerializeField] float _starStopTime;
    [Tooltip("通常攻撃休憩時間")]
    [SerializeField] float _normalStopTime;

    [Tooltip("ワープ休憩時間")]
    [SerializeField] float _teleportationStopTime;
    [Tooltip("ワープ円半径")]
    [SerializeField] float _teleportRange;
    [Tooltip("ワープ円中心距離")]
    [SerializeField] float _teleportMiddleDistance;

    [Tooltip("シールド出現最大半径")]
    [SerializeField] float _shieldSpawnMaxDistance;
    [Tooltip("シールド出現前方の距離")]
    [SerializeField] float _shieldSpawnFowardDistance;

    [Tooltip("ふぁいなるぶらすとの流星カウント")]
    [SerializeField] float _finalChargeTime;

    [Tooltip("攻撃のローテーション")]
    [SerializeField] AttackType[] _attackRotation;

    const int NUMBER_OF_NORMAL_ATTACK = 3;
    const int NUMBER_OF_STAR_ATTACK = 4;
    const int NUMBER_OF_TELEPOTATION = 4;
    const int NUMBER_OF_SHIELDS = 4;
    const int NUMBER_OF_FINAL_BLAST_COUNT = 5;

    Transform _transform = default;
    ObjectPoolSystem _pool = default;
    Action _attackAction = default;
    TestBossMoveActionEventTransform _transformMove = default;
    EnemyAttack enemyAttack = new();

    Vector3 _firstPosition = default;
    Vector3 _leftTeleportPoint = default;
    Vector3 _rightTeleportPoint = default;

    Vector3 _shieldSpawnRootPoint = default;

    Vector3 _cachePosition = default;

    AttackType attackType = default;

    int _attackRotationIndex = 0;

    int _starUseCount = 0;

    int _numberOfEndAction = 0;

    float _actionEndTime = 0;

    float _actionTimeLineCacheTime = 0;

    bool _canChangeAction = false;

    enum AttackType
    {
        ShotStar,
        ShotNormal,
        Telepotation,
        CreateShield,
        FinalBlast
    }

    protected override void Start()
    {
        //base.Start();
        _transform = transform;
        _pool = FindObjectOfType<ObjectPoolSystem>();
        _transformMove = new TestBossMoveActionEventTransform();
        _transformMove.SetTransform  = _transform;

        _firstPosition = _transform.position;
        _leftTeleportPoint = new Vector3(_firstPosition.x - _teleportRange, _firstPosition.y, _firstPosition.z);
        _rightTeleportPoint = new Vector3(_firstPosition.x + _teleportRange, _firstPosition.y, _firstPosition.z);
        _shieldSpawnRootPoint = _firstPosition + Vector3.forward * _shieldSpawnMaxDistance;

        _canChangeAction = false;
        Move();
    }
    protected override void AttackAnimation()
    {
        // だみー
    }

    protected override void AttackProcess()
    {
        AttackSelect();
    }

    protected override void MoveAnimation()
    {
        // アイドル
    }

    protected override void MoveProcess()
    {
        IdleMove();
    }

    private void AttackSelect()
    {
        // チェンジアタック
        if (_canChangeAction)
        {
            _canChangeAction = false;

            _numberOfEndAction = 0;

            // ふぁいなるぶらすとかどうか
            if(_starUseCount >= NUMBER_OF_FINAL_BLAST_COUNT)
            {
                attackType = AttackType.FinalBlast;
                _starUseCount = 0;
            }
            // 攻撃登録
            else
            {
               
                attackType = _attackRotation[_attackRotationIndex];

                if(attackType == AttackType.ShotStar)
                {
                    _starUseCount++;
                }

                _attackRotationIndex++;

                if (_attackRotationIndex == _attackRotation.Length)
                {
                    _attackRotationIndex = 0;
                }
            }

            _actionEndTime = 0;
            _actionTimeLineCacheTime = 0;

            switch (attackType)
            {
                case AttackType.ShotStar:
                    _attackAction = () => StarAttack();

                    break;
                case AttackType.ShotNormal:
                    _attackAction = () => NormalAttack();
                    break;
                case AttackType.Telepotation:
                    _attackAction = () => Telepotation();

                    break;
                case AttackType.CreateShield:
                    _attackAction = () => ShieldAttack();
                    break;
                case AttackType.FinalBlast:
                    _attackAction = () => FinalBlast();
                    break;
            }


        }
        // 攻撃終了後休憩時間終わり、念のため付ける
        else if (0 < _actionEndTime && _actionEndTime + _attackStopTime < Time.time)
        {
            _canChangeAction = true;
            return;
        }

        _attackAction();

    }
    /// <summary>
    /// 扇状発射に重力付けただけ
    /// </summary>
    private void StarAttack()
    {
        if (CoolTimeCheck(_starStopTime, ref _actionTimeLineCacheTime) && CheckNumberOfAttack(_numberOfEndAction, NUMBER_OF_STAR_ATTACK))
        {
            
            // うつ
            print("aaa スター");
            AttackedCacheValue();
            CheckEndAttack(_numberOfEndAction, NUMBER_OF_STAR_ATTACK);
        }

    }



    private void NormalAttack()
    {
        if (CoolTimeCheck(_normalStopTime, ref _actionTimeLineCacheTime) && CheckNumberOfAttack(_numberOfEndAction, NUMBER_OF_NORMAL_ATTACK))
        {
            // うつ
            print("aaa normal");
            AttackedCacheValue();
            CheckEndAttack(_numberOfEndAction, NUMBER_OF_NORMAL_ATTACK);
        }
    }

    private void Telepotation()
    {
        print("cool" + (CoolTimeCheck(_teleportationStopTime, ref _actionTimeLineCacheTime)));
        print("check" + CheckNumberOfAttack(_numberOfEndAction, NUMBER_OF_TELEPOTATION));

        if (CoolTimeCheck(_teleportationStopTime, ref _actionTimeLineCacheTime) && CheckNumberOfAttack(_numberOfEndAction, NUMBER_OF_TELEPOTATION))
        {
            if(_numberOfEndAction == 0)
            {
                _cachePosition = _transform.position;
            }
            Vector2 teleportPoint = RandomCirclePoint(_teleportRange);

            if (_numberOfEndAction % 2 == 0)
            {
                _transform.position = _rightTeleportPoint + (Vector3)teleportPoint;
            }
            else
            {
                _transform.position = _leftTeleportPoint + (Vector3)teleportPoint;
            }

            AttackedCacheValue();
            print("aaa warp");
            print(_numberOfEndAction +","+_teleportationStopTime + "," + _actionTimeLineCacheTime);
            
        }
        else if (CoolTimeCheck(_teleportationStopTime, ref _actionTimeLineCacheTime) && _numberOfEndAction >= NUMBER_OF_TELEPOTATION )
        {
            print("aaa warpend");
            _transform.position = _cachePosition;
            AttackedCacheValue();
            CheckEndAttack(_numberOfEndAction, NUMBER_OF_TELEPOTATION);
        }


    }


    private void ShieldAttack()
    {
        if (CheckNumberOfAttack(_numberOfEndAction, 1))
        {

            // シールド仮
            for(int i = 0; i < NUMBER_OF_SHIELDS; i++)
            {
                Transform shield = default;
                shield = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
                Vector2 randomPoint = RandomCirclePoint(_shieldSpawnMaxDistance);
                shield.position = _shieldSpawnRootPoint + (Vector3)randomPoint;
            }
           
            // 右に行くか左に行くかの処理

            AttackedCacheValue();
            CheckEndAttack(_numberOfEndAction, 1);

        }
    }

    private void FinalBlast()
    {
        // チャージ時間計測のため
        if(_actionTimeLineCacheTime <= 0)
        {
            _actionTimeLineCacheTime = Time.time;
        }

        if(_actionTimeLineCacheTime + _finalChargeTime < Time.time)
        {
            // ふぁいなるぶらすと
            print("aaa final");
            _actionEndTime = Time.time;
            Move();
            isAttack = false;
        }
    }

    /// <summary>
    /// クールタイムチェック
    /// </summary>
    /// <param name="coolTime">クールタイム</param>
    /// <returns>クールタイム過ぎればtrue</returns>
    private bool CoolTimeCheck(float coolTime, ref float useTime)
    {

        return useTime + coolTime < Time.time;
    }

    private void CheckEndAttack(int attacked, int end)
    {
        if (attacked >= end)
        {
            _actionEndTime = Time.time;
            Move();
            isAttack = false;

        }
    }

    private bool CheckNumberOfAttack(int attacked, int end)
    {
        return attacked < end;
    }

    private void AttackedCacheValue()
    {
        _actionTimeLineCacheTime = Time.time;
        _numberOfEndAction++;
    }

    private Vector2 RandomCirclePoint(float maxDistance)
    {
        return UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(0, maxDistance);
    }


    /// <summary>
    /// 左右上下
    /// </summary>
    private void IdleMove()
    {
        _transformMove.Move();
        // 攻撃終了後休憩時間終わり
        if (_actionEndTime + _attackStopTime < Time.time)
        {
            _canChangeAction = true;
            Attack();
            isMove = false;
            return;
        }
    }
}