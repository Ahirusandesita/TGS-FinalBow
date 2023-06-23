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

    [Tooltip("�U���̕p�x")]
    private const float ATTACK_INTERVAL_TIME = 2f;

    [Tooltip("��]��̃u���C�N����")]
    private const float AFTER_ROTATE_BREAK_TIME = 0.2f;

    [Tooltip("���ʂ̊p�x")]
    private readonly Quaternion FRONT_ANGLE = Quaternion.Euler(new Vector3(0f, 180f, 0f));

    [Tooltip("���̈ړ������ŏ����I��")]
    private bool _isLastMove = default;
    #endregion


    protected override void OnEnable()
    {
        base.OnEnable();

        SetGoalPositionCentral();
        // �ŏ����琳�ʂ���������
        _transform.rotation = FRONT_ANGLE;
        _isLastMove = false;
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
        if (_currentTime2 >= 10f)
        {
            SetGoalPosition(WaveType.zakoWave1);
            IsFinishMovement = false;
            _isLastMove = true;
        }
    }

    //public override void MoveSequence()
    //{
    //    // �U���C���^�[�o��
    //    if (_currentTime >= ATTACK_INTERVAL_TIME)
    //    {
    //        // �U���O�Ƀv���C���[�̕���������
    //        if (_transform.rotation != _childSpawner.rotation)
    //        {
    //            RotateToPlayer(_rotateToPlayerSpeed);
    //            return;
    //        }

    //        // ��]���I����Ă����莞�ԑҋ@����
    //        if (_currentTime2 <= AFTER_ROTATE_BREAK_TIME)
    //        {
    //            _currentTime2 += Time.deltaTime;
    //            return;
    //        }

    //        _currentTime2 = 0f;

    //        // �U���𔭎�
    //        _birdAttack.NormalAttack(_childSpawner);

    //        _currentTime = 0f;
    //    }

    //    LinearMovement(_goalPosition);

    //    _currentTime += Time.deltaTime;
    //}
}