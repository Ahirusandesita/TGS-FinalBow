// --------------------------------------------------------- 
// EnemyEnum.cs 
// 
// CreateDay: 2023/06/28
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

/// <summary>
/// ���G���̓����̎��
/// </summary>
public enum MoveType
{
    /// <summary>
    /// �����ړ�
    /// </summary>
    linear,
    /// <summary>
    /// �Ȑ��ړ�
    /// </summary>
    curve
}

/// <summary>
/// ���G���̎��
/// </summary>
public enum BirdType
{
    normalBird,
    watcher,
    bomberBug,
    giantBat,
    oceanDragon,
    desertDragon
}

/// <summary>
/// ���G���̍U�����@�̎��
/// </summary>
public enum BirdAttackType
{
    /// <summary>
    /// ���Ԋu
    /// </summary>
    equalIntervals,
    /// <summary>
    /// �b���w��
    /// </summary>
    specifySeconds,
    /// <summary>
    /// �A���U��
    /// </summary>
    consecutive,
    /// <summary>
    /// �U�����Ȃ�
    /// </summary>
    none
}

/// <summary>
/// �n��G���̍s���̎��
/// </summary>
public enum GroundEnemyActionType
{
    /// <summary>
    /// ��~�i�ҋ@�j
    /// </summary>
    stop,
    /// <summary>
    /// �W�����v
    /// </summary>
    jump,
    /// <summary>
    /// �I����
    /// </summary>
    crabWalk,
    /// <summary>
    /// ���@�e�i�����j
    /// </summary>
    straightAttack,
    /// <summary>
    /// �����i�R�Ȃ�j
    /// </summary>
    throwingAttack,
}

/// <summary>
/// ���[�v�̎��
/// </summary>
public enum RoopType
{
    /// <summary>
    /// �X�^�[�g����J��Ԃ��i���s�j
    /// </summary>
    forward,
    /// <summary>
    /// ���̏ꂩ��܂�Ԃ��i�t�s�j
    /// </summary>
    reverse
}

/// <summary>
/// �G�̕����̎��
/// </summary>
public enum DirectionType_AtMoving
{
    /// <summary>
    /// �v���C���[����
    /// </summary>
    player,
    /// <summary>
    /// �v���C���[����Ƃ������[���h����
    /// </summary>
    front,
    /// <summary>
    /// �i�s����
    /// </summary>
    moveDirection
}

/// <summary>
/// ��~���̕����̎��
/// </summary>
public enum DirectionType_AtStopping
{
    /// <summary>
    /// ���܂ł̕������p��
    /// </summary>
    continuation,
    /// <summary>
    /// �v���C���[����
    /// </summary>
    player,
    /// <summary>
    /// �v���C���[����Ƃ������[���h����
    /// </summary>
    front
}

/// <summary>
/// �U�����̕����̎��
/// </summary>
public enum DirectionType_AtAttack
{
    /// <summary>
    /// �v���C���[����
    /// </summary>
    player,
    /// <summary>
    /// �v���C���[����Ƃ������[���h����
    /// </summary>
    front
}