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
    bombBird,
    penetrateBird,
    thunderBird,
    bombBirdBig,
    thunderBirdBig,
    penetrateBirdBig,
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