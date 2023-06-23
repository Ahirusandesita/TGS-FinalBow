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
    #region �ϐ�
    private bool _isFirstTimes = true;

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

    [Tooltip("���ʂ̊p�x")]
    private readonly Quaternion FRONT_ANGLE = Quaternion.Euler(new Vector3(0f, 180f, 0f));
    #endregion


    protected override void OnEnable()
    {
        base.OnEnable();

        _isLastMove = false;
        SetGoalPositionCentral();
    }


    public override void MoveSequence()
    {
        _currentTime += Time.deltaTime;

        // �ړ�����������܂Ń��[�v
        if (!IsFinishMovement)
        {
            LinearMovement();

            return;
        }
        else
        {
            // �Ō�̈ړ����I��������A���̃��\�b�h�𔲂���
            if (_isLastMove)
            {
                return;
            }
        }

        // ���Ԋu�ōU��
        if (_currentTime >= ATTACK_INTERVAL_TIME)
        {
            // �U���O�Ƀv���C���[�̕���������
            if (_transform.rotation != _childSpawner.rotation)
            {
                RotateToPlayer(_rotateToPlayerSpeed);
                return;
            }

            _birdAttack.NormalAttack(_childSpawner);
            _currentTime = 0f;
        }

        _currentTime2 += Time.deltaTime;

        // ��莞�Ԍo������ړ��ĊJ
        if (_currentTime2 >= RE_ATTACK_TIME)
        {
            IsFinishMovement = false;

            // �X�^�[�g�ʒu�ƃS�[���ʒu�̍Đݒ�
            _startPosition = _transform.position;
            SetGoalPosition(WaveType.zakoWave1);

            // ���ʂ������Ȃ���
            _transform.rotation = FRONT_ANGLE;

            // ���̈ړ����Ō�
            _isLastMove = true;
        }
    }
}