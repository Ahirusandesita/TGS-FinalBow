// --------------------------------------------------------- 
// EnemyEnum.cs 
// 
// CreateDay: 2023/06/28
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

/// <summary>
/// ’¹G‹›‚Ì“®‚«‚Ìí—Ş
/// </summary>
public enum MoveType
{
    /// <summary>
    /// ’¼üˆÚ“®
    /// </summary>
    linear,
    /// <summary>
    /// ‹ÈüˆÚ“®
    /// </summary>
    curve
}

/// <summary>
/// ’¹G‹›‚Ìí—Ş
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
/// ’¹G‹›‚ÌUŒ‚•û–@‚Ìí—Ş
/// </summary>
public enum BirdAttackType
{
    /// <summary>
    /// “™ŠÔŠu
    /// </summary>
    equalIntervals,
    /// <summary>
    /// •b”w’è
    /// </summary>
    specifySeconds,
    /// <summary>
    /// ˜A‘±UŒ‚
    /// </summary>
    consecutive,
    /// <summary>
    /// UŒ‚‚µ‚È‚¢
    /// </summary>
    none
}

/// <summary>
/// ’nãG‹›‚Ìs“®‚Ìí—Ş
/// </summary>
public enum GroundEnemyActionType
{
    /// <summary>
    /// ’â~i‘Ò‹@j
    /// </summary>
    stop,
    /// <summary>
    /// ƒWƒƒƒ“ƒv
    /// </summary>
    jump,
    /// <summary>
    /// ŠI•à‚«
    /// </summary>
    crabWalk,
    /// <summary>
    /// –‚–@’ei’¼‹…j
    /// </summary>
    straightAttack,
    /// <summary>
    /// “Š±iR‚È‚èj
    /// </summary>
    throwingAttack,
}