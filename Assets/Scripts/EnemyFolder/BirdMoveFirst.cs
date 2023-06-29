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
    #region �ϐ�
    [Tooltip("���݂̌o�ߎ��ԁi�U���܂ł̕p�x�Ɏg���j")]
    private float _currentTime = 0f;

    [Tooltip("���݂̌o�ߎ��ԁi�Ăѓ����o���܂ł̎��ԂɎg���j")]
    private float _currentTime2 = 0f;

    [Tooltip("���̈ړ������ŏ����I��")]
    private bool _isLastMove = default;

    [Tooltip("���̃S�[���ݒ�̉�")]
    private int _nextSetGoalCount = 1;  // �����l1

    [Tooltip("�U���̕p�x")]
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

        // ����Inspector�Őݒ�~�X���������牼�ݒ肷��
        if (_linearMovementSpeed == 0f)
        {
            _linearMovementSpeed = 20f;
            X_Debug.LogError("EnemySpawnPlaceData.Speed ���ݒ肳��Ă܂���");
        }

        if (_reAttackTime == 0f)
        {
            _reAttackTime = 5f;
            X_Debug.LogError("EnemySpawnPlaceData.WaitTime_s ���ݒ肳��Ă܂���");
        }
    }


    public override void MoveSequence()
    {
        // ��჏�Ԃ����肷��i��Ⴢ������瓮���Ȃ��j
        if (Paralysing())
        {
            return;
        }

        _currentTime += Time.deltaTime;

        // �ړ������i�ړ����������Ă����炱�̃u���b�N�͖��������j-----------------------------------------------------------

        if (!IsFinishMovement)
        {
            LinearMovement();

            return;
        }
        else
        {
            // �Ō�̈ړ����I��������A�f�X�|�[��������
            if (_isLastMove)
            {
                _needDespawn = true;

                // �N���X���͂���
                Destroy(this);

                return;
            }
        }

        // �U�������i���Ԋu�Ŏ��s�����j-----------------------------------------------------------------------------------

        if (_currentTime >= ATTACK_INTERVAL_TIME)
        {
            // �U���O�Ƀv���C���[�̕���������
            if (_transform.rotation != _childSpawner.rotation)
            {
                RotateToPlayer();
                return;
            }

            //�@�U�������s
            _birdAttack.NormalAttack(_childSpawner, ConversionToBulletType(), _numberOfBullet);
            _currentTime = 0f;
        }

        _currentTime2 += Time.deltaTime;

        // �Ĉړ��̂��߂̃��Z�b�g�����i�U�����X�^�[�g���Ă����莞�Ԍ�Ɏ��s�j-----------------------------------------------

        // �U�����I��
        if (_currentTime2 >= _reAttackTime)
        {
            // �ݒ肳�ꂽ�S�[���̐���1�̂Ƃ� AND ���ׂẴS�[�����ݒ肳�ꂽ��A���̍s�����Ō�
            if (_numberOfGoal == 1 && _nextSetGoalCount > _numberOfGoal)
            {
                // ���̈ړ����Ō�
                _isLastMove = true;

                return;
            }

            // �Ĉړ��O�ɐ��ʂ�����
            if (_transform.rotation != FRONT_ANGLE)
            {
                RotateToFront();
                return;
            }

            IsFinishMovement = false;

            // �X�^�[�g�ʒu�ƃS�[���ʒu�̍Đݒ�
            _startPosition = _transform.position;
            SetGoalPosition(_spawnedWave, _thisInstanceIndex, howManyTimes: _nextSetGoalCount);
            _nextSetGoalCount++;

            // ���ׂẴS�[�����ݒ肳�ꂽ��A���̍s�����Ō�
            if (_nextSetGoalCount > _numberOfGoal)
            {
                _isLastMove = true;
            }

            _currentTime2 = 0f;
        }
    }
}