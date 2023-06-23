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
        SetGoalPositionCentral();
        _currentTime = 0f;
        _currentTime2 = 0f;
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
                RotateToPlayer();
                return;
            }

            _birdAttack.NormalAttack(_childSpawner);
            _currentTime = 0f;
        }

        _currentTime2 += Time.deltaTime;

        // ��莞�Ԍo������ړ��ĊJ
        if (_currentTime2 >= RE_ATTACK_TIME)
        {
            if (_transform.rotation != FRONT_ANGLE)
            {
                RotateToFront();
                return;
            }

            IsFinishMovement = false;

            // �X�^�[�g�ʒu�ƃS�[���ʒu�̍Đݒ�
            _startPosition = _transform.position;
            SetGoalPosition(WaveType.zakoWave1);

            // ���̈ړ����Ō�
            _isLastMove = true;
        }
    }
}