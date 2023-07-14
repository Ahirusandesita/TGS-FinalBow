// --------------------------------------------------------- 
// BirdMoveSecond.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;


public enum ArcMoveDirection
{
    Up,
    Down,
    Front,
    Back
}

public class BirdMoveSecond : BirdMoveBase
{
    #region variable 

    // ���N���X��_movementSpeed�ɕύX������
    //public float _sideMoveSpeed = 12f;

    float _moveSpeedArc = 10f;

    const float PARABORA_ANGLE = 10f;

    const float PERCENT_HALF = 0.5f;

    const float VIEW_PORT_MIN = 0f;

    const float VIEW_PORT_MAX = 1f;

    Vector3 _sideMoveNormalizedVector = default;

    Vector3 _movingSide = default;

    Vector3 objectViewPoint = default;

    Vector3 arcDirection = default;

    Vector3 _movingVector = default;

    float _movingArcDistance = default;

    float _cacheMoveValue = default;

    float _finishMoveValue = default;

    float percentMoveDistance = default;

    ArcMoveDirection _arcMoveDirection = default;

    #endregion
    #region property
    public float MoveSpeedArc
    {
        set
        {
            _moveSpeedArc = value;
        }
    }
    #endregion

    public ArcMoveDirection ArcMoveDirection
    {
        set
        {
            _arcMoveDirection = value;
        }
    }
    #region method

    protected override void Start()
    {
        base.Start();

        InitializeVariables();

        //SetGoalPosition(WaveType.zakoWave2, _thisInstanceIndex);

        // �ŏ����琳�ʂ���������
        //_transform.rotation = FRONT_ANGLE;

    }


    protected void InitializeVariables()
    {

        _sideMoveNormalizedVector = default;

        _movingSide = default;

        _movingArcDistance = default;

        _movingVector = default;

        _cacheMoveValue = default;

        _finishMoveValue = default;

        percentMoveDistance = default;

        // ���ړ��x�N�g��
        _sideMoveNormalizedVector = GetSideMoveVector().normalized;

        arcDirection = GetArcDirection(_sideMoveNormalizedVector);

        // ���ړ���
        _finishMoveValue = GetSideMoveVector().magnitude;
    }

    private Vector3 GetArcDirection(Vector3 moveDirection)
    {
        Vector3 dir;
        // �ʂ̕��������߂�
        dir = _arcMoveDirection switch
        {
            ArcMoveDirection.Up => Vector3.Cross(moveDirection, Vector3.forward),
            ArcMoveDirection.Down => Vector3.Cross(moveDirection, Vector3.back),
            ArcMoveDirection.Back => Vector3.Cross(moveDirection, Vector3.down),
            ArcMoveDirection.Front => Vector3.Cross(moveDirection, Vector3.up),
            _ => Vector3.up,
        };
        return dir.normalized;
    }

    protected override void EachMovement(ref float movedDistance)
    {
        base.EachMovement(ref movedDistance);

        // �ړ�����
        ArcMove();

        movedDistance += _movingSide.magnitude;
    }

    // �X�^�[�g����S�[���ւ���
    // ���s��:���@�E�s��:��
    //public override void MoveSequence()
    //{
    //    if (Paralysing())
    //    {
    //        return;
    //    }

    //    if (!_needDespawn)
    //    {
    //        // ��������
    //        RotateToPlayer();
    //        // �ړ�����
    //        ArcMove();
    //        // �`�F�b�N�ړ��G���h
    //        _needDespawn = CheckMoveFinish();

    //        if (_canStartAttack && AttackCheck())
    //        {
    //            _canStartAttack = false;

    //            //StartCoroutine(_birdAttack.NormalAttackLoop(_childSpawner, ConversionToBulletType(), _numberOfBullet));
    //        }

    //        return;
    //    }
    //    // �ړ��I����
    //    else
    //    {
    //        _transform.position = _goalPosition;
    //    }

    //}

    /// <summary>
    /// �A�^�b�N�̏������L�q
    /// </summary>
    /// <returns>�A�^�b�N�ł���Ȃ�true</returns>
    private bool AttackCheck()
    {
        objectViewPoint = Camera.main.WorldToViewportPoint(_transform.position);

        return IsBetween(objectViewPoint.x, VIEW_PORT_MIN, VIEW_PORT_MAX) &&
            (IsBetween(objectViewPoint.y, VIEW_PORT_MIN, VIEW_PORT_MAX) &&
            objectViewPoint.z > VIEW_PORT_MIN);

        bool IsBetween(float look, float min, float max) => min <= look && look <= max;
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
        if (cross.y < 0)
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
        // �����ړ�
        _movingSide = _movementSpeed * Time.deltaTime * _sideMoveNormalizedVector;
        // �݌v���ړ��ʂ̃L���b�V��
        _cacheMoveValue += _movingSide.magnitude;

        _movingArcDistance = AddArc();

        _movingVector = _movingSide;

        _movingVector += arcDirection * _movingArcDistance;

        _transform.Translate(_movingVector, Space.World);
    }

    /// <summary>
    /// ���݂̈ʒu����ʂ̈ړ��ʂ����Ƃ߂�
    /// </summary>
    /// <returns>Y�ړ���</returns>
    private float AddArc()
    {
        percentMoveDistance = _cacheMoveValue / _finishMoveValue;

        // �O���̓���
        if (percentMoveDistance < PERCENT_HALF)
        {
            return _moveSpeedArc * PrabolaMove() * Time.deltaTime;
        }
        // �㔼�̓���
        else
        {
            return -_moveSpeedArc * PrabolaMove() * Time.deltaTime;
        }

        float PrabolaMove()
        {
            // �������^����ax^2�̕���
            return PARABORA_ANGLE * Mathf.Pow(PERCENT_HALF - percentMoveDistance, 2);

        }

    }

    protected override void InitializeForRe_Movement()
    {
        base.InitializeForRe_Movement();

        InitializeVariables();

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