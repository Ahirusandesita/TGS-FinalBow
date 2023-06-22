// --------------------------------------------------------- 
// BirdMoveSecond.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BirdMoveSecond : BirdMoveBase
{
    #region variable 

    Vector3 _sideMoveVector = default;

    float _moveY = default;

    float _sideMoveSpeed = default;

    float _moveSpeedY = default;

    bool _moveDown = default;

    #endregion
    #region property
    #endregion
    #region method

    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeVariables();
        // �ŏ����琳�ʂ���������
        _transform.rotation = Quaternion.Euler(Vector3.up * 180f);
        // ���ړ��x�N�g��
        _sideMoveVector = GetSideMoveVector();
        // �����ォ���߂�
        _moveDown = CheckDown();
    }


    protected override void InitializeVariables()
    {
        _needRotateTowardDirectionOfTravel = false;
    }

    // �X�^�[�g����S�[���ւ���
    // ���s��:���@�E�s��:��
    public override void MoveSequence()
    {
        
    }

    /// <summary>
    /// �J�����̃|�W�V�������猩�č��ɍs�������ׂ�
    /// </summary>
    /// <returns>���s���Ȃ�true</returns>
    private bool CheckDown()
    {
        // �G����v���C���[�̕����x�N�g��
        Vector3 enemyToPlayerVector = Camera.main.transform.position - _transform.position;

        // �O���x�N�g���ƓG�ƃv���C���[�����x�N�g���̊O�ς����Ƃ߂�
        Vector3 cross = Vector3.Cross(_transform.forward, enemyToPlayerVector);

        // �O�ς��獶�E����
        if(cross.y < 0)
        {
            // �G���猩�ĉE�Ƀv���C���[������̂ŉ��ɍs��
            return true;
        }

        return false;
    }

    /// <summary>
    /// ���ړ��̃x�N�g�������Ƃ߂�
    /// </summary>
    /// <returns>���ړ��x�N�g��</returns>
    private Vector3 GetSideMoveVector()
    {
        return _goalPosition - _startPosition;
    }

    private bool CheckMoveFinish()
    {
        return false;
    }
    #endregion
}