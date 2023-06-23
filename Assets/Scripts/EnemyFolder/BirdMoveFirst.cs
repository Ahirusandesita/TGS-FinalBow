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

    [Tooltip("���݂̌o�ߎ��ԁi��]��̃u���C�N�Ɏg���j")]
    private float _currentTime2 = 0f;

    [Tooltip("�U���̕p�x")]
    private const float ATTACK_INTERVAL_TIME = 2f;

    [Tooltip("��]��̃u���C�N����")]
    private const float AFTER_ROTATE_BREAK_TIME = 0.2f;

    [Tooltip("���ʂ̊p�x")]
    private readonly Quaternion FRONT_ANGLE = Quaternion.Euler(new Vector3(0f, 180f, 0f));
    #endregion


    protected override void OnEnable()
    {
        base.OnEnable();

        SetGoalPositionCentral();
        // �ŏ����琳�ʂ���������
        _transform.rotation = FRONT_ANGLE;
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