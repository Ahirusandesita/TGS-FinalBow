// --------------------------------------------------------- 
// BirdMoveFirst.cs 
// 
// CreateDay: 2023/06/21
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class BirdMoveFirst : BirdMoveBase
{
    #region �ϐ�
    [Tooltip("���݂̌o�ߎ��ԁi�U���܂ł̕p�x�Ɏg���j")]
    private float _currentTime = 0f;

    [Tooltip("���݂̌o�ߎ��ԁi�Ăѓ����o���܂ł̎��ԂɎg���j")]
    private float _currentTime2 = 0f;

    [Tooltip("���̈ړ������ŏ����I��")]
    private bool _isLastMove = default;

    [Tooltip("�U���̕p�x")]
    private const float ATTACK_INTERVAL_TIME = 2f;

    [Tooltip("�Ăѓ����o���܂ł̎���")]
    private const float RE_ATTACK_TIME = 10f;
    #endregion


    protected override void OnEnable()
    {
        base.OnEnable();

        _isLastMove = false;
        //SetGoalPositionCentral();
        SetGoalPosition(_spawnedWave, _spawnNumber);
        _currentTime = 0f;
        _currentTime2 = 0f;
    }


    public override void MoveSequence()
    {
        // ��჏�Ԃ����肷��i��Ⴢ������瓮���Ȃ��j
        Paralysing();

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
            _birdAttack.NormalAttack(_childSpawner);
            _currentTime = 0f;
        }

        _currentTime2 += Time.deltaTime;

        // �Ĉړ��̂��߂̃��Z�b�g�����i�U�����X�^�[�g���Ă����莞�Ԍ�Ɏ��s�j-----------------------------------------------

        if (_currentTime2 >= RE_ATTACK_TIME)
        {
            // �Ĉړ��O�ɐ��ʂ�����
            if (_transform.rotation != FRONT_ANGLE)
            {
                RotateToFront();
                return;
            }

            IsFinishMovement = false;

            // �X�^�[�g�ʒu�ƃS�[���ʒu�̍Đݒ�
            _startPosition = _transform.position;
            SetGoalPosition(WaveType.zakoWave1, _spawnNumber + 2);

            // ���̈ړ����Ō�
            _isLastMove = true;
        }
    }
}