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

    Vector3 _sideMoveNormalizedVector = default;

    Vector3 _movingSide = default;

    float _movingY = default;

    float _sideMoveSpeed = default;

    float _moveSpeedY = default;

    float _cacheMoveValue = default;

    float _finishMoveValue = default;

    float percentMoveDistance = default;

    float directionY = default;

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
        _sideMoveNormalizedVector = GetSideMoveVector().normalized;
        // ���ړ���
        _finishMoveValue = GetSideMoveVector().magnitude;
        // �����ォ���߂�
        _moveDown = CheckDown();
    }

    // �Ă���
    private void Awake()
    {
        _startPosition = new Vector3(0, 0, 0);

        _goalPosition = new Vector3(100, 0, 0);
    }
    protected override void InitializeVariables()
    {
        _needRotateTowardDirectionOfTravel = false;

        _sideMoveNormalizedVector = default;

        _movingSide = default;

        _movingY = default;

        _sideMoveSpeed = 10f;

        _moveSpeedY = 1f;

        _cacheMoveValue = default;

        _finishMoveValue = default;

        percentMoveDistance = default;

        directionY = default;

        _moveDown = default;
    }

    // �X�^�[�g����S�[���ւ���
    // ���s��:���@�E�s��:��
    public override void MoveSequence()
    {
        if (!_isFinishMovement)
        {
            // �ړ�����
            ArcMove();
            // �`�F�b�N�ړ��G���h
            _isFinishMovement = CheckMoveFinish();

            return;
        }
        
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

    /// <summary>
    /// �ړ�
    /// </summary>
    private void ArcMove()
    {
        _movingSide = _sideMoveSpeed * Time.deltaTime * _sideMoveNormalizedVector;
        _cacheMoveValue += _movingSide.magnitude; 

        _movingY = MoveCalcY();

        _movingSide += Vector3.up * _movingY;
        print(_movingSide);

        _transform.Translate(_movingSide);

    }

    /// <summary>
    /// ���݂̈ʒu����Y�̈ړ��ʂ����Ƃ߂�
    /// </summary>
    /// <returns>Y�ړ���</returns>
    private float MoveCalcY()
    {
        percentMoveDistance = _cacheMoveValue / _finishMoveValue;

        directionY = 1f;

        if (_moveDown)
        {
            directionY = -1f;
        }

        if(percentMoveDistance < 0.5f)
        {
            return directionY * _moveSpeedY * Time.deltaTime;
        }
        else
        {
            return -directionY * _moveSpeedY * Time.deltaTime;
        }

        
    }
    /// <summary>
    /// �ړ��I����������ׂ�
    /// </summary>
    /// <returns>�S�[����������true</returns>
    private bool CheckMoveFinish()
    {
        return _finishMoveValue < _cacheMoveValue;
    }
    #endregion
}