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
    [Tooltip("�U����")]
    [SerializeField] int _numberOfAttackRepeatTimes;
    [Tooltip("�x�e����")]
    [SerializeField] float _attackStopTime;
    [Tooltip("�����x�e����")]
    [SerializeField] float _starStopTime;
    [Tooltip("�ʏ�U���x�e����")]
    [SerializeField] float _normalStopTime;

    [Tooltip("���[�v�x�e����")]
    [SerializeField] float _teleportationStopTime;
    [Tooltip("���[�v�~���a")]
    [SerializeField] float _teleportRange;
    [Tooltip("���[�v�~���S����")]
    [SerializeField] float _teleportMiddleDistance;

    [Tooltip("�V�[���h�o���ő唼�a")]
    [SerializeField] float _shieldSpawnMaxDistance;
    [Tooltip("�V�[���h�o���O���̋���")]
    [SerializeField] float _shieldSpawnFowardDistance;

    [Tooltip("�ӂ����Ȃ�Ԃ炷�Ƃ̗����J�E���g")]
    [SerializeField] float _finalChargeTime;

    [Tooltip("�U���̃��[�e�[�V����")]
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
        // ���݁[
    }

    protected override void AttackProcess()
    {
        AttackSelect();
    }

    protected override void MoveAnimation()
    {
        // �A�C�h��
    }

    protected override void MoveProcess()
    {
        IdleMove();
    }

    private void AttackSelect()
    {
        // �`�F���W�A�^�b�N
        if (_canChangeAction)
        {
            _canChangeAction = false;

            _numberOfEndAction = 0;

            // �ӂ����Ȃ�Ԃ炷�Ƃ��ǂ���
            if(_starUseCount >= NUMBER_OF_FINAL_BLAST_COUNT)
            {
                attackType = AttackType.FinalBlast;
                _starUseCount = 0;
            }
            // �U���o�^
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
        // �U���I����x�e���ԏI���A�O�̂��ߕt����
        else if (0 < _actionEndTime && _actionEndTime + _attackStopTime < Time.time)
        {
            _canChangeAction = true;
            return;
        }

        _attackAction();

    }
    /// <summary>
    /// ��󔭎˂ɏd�͕t��������
    /// </summary>
    private void StarAttack()
    {
        if (CoolTimeCheck(_starStopTime, ref _actionTimeLineCacheTime) && CheckNumberOfAttack(_numberOfEndAction, NUMBER_OF_STAR_ATTACK))
        {
            
            // ����
            print("aaa �X�^�[");
            AttackedCacheValue();
            CheckEndAttack(_numberOfEndAction, NUMBER_OF_STAR_ATTACK);
        }

    }



    private void NormalAttack()
    {
        if (CoolTimeCheck(_normalStopTime, ref _actionTimeLineCacheTime) && CheckNumberOfAttack(_numberOfEndAction, NUMBER_OF_NORMAL_ATTACK))
        {
            // ����
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

            // �V�[���h��
            for(int i = 0; i < NUMBER_OF_SHIELDS; i++)
            {
                Transform shield = default;
                shield = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
                Vector2 randomPoint = RandomCirclePoint(_shieldSpawnMaxDistance);
                shield.position = _shieldSpawnRootPoint + (Vector3)randomPoint;
            }
           
            // �E�ɍs�������ɍs�����̏���

            AttackedCacheValue();
            CheckEndAttack(_numberOfEndAction, 1);

        }
    }

    private void FinalBlast()
    {
        // �`���[�W���Ԍv���̂���
        if(_actionTimeLineCacheTime <= 0)
        {
            _actionTimeLineCacheTime = Time.time;
        }

        if(_actionTimeLineCacheTime + _finalChargeTime < Time.time)
        {
            // �ӂ����Ȃ�Ԃ炷��
            print("aaa final");
            _actionEndTime = Time.time;
            Move();
            isAttack = false;
        }
    }

    /// <summary>
    /// �N�[���^�C���`�F�b�N
    /// </summary>
    /// <param name="coolTime">�N�[���^�C��</param>
    /// <returns>�N�[���^�C���߂����true</returns>
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
    /// ���E�㉺
    /// </summary>
    private void IdleMove()
    {
        _transformMove.Move();
        // �U���I����x�e���ԏI���
        if (_actionEndTime + _attackStopTime < Time.time)
        {
            _canChangeAction = true;
            Attack();
            isMove = false;
            return;
        }
    }
}