// --------------------------------------------------------- 
// EnemyEnum.cs 
// 
// CreateDay: 2023/06/28
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

/// <summary>
/// 鳥雑魚の動きの種類
/// </summary>
public enum MoveType
{
    /// <summary>
    /// 直線移動
    /// </summary>
    linear,
    /// <summary>
    /// 曲線移動
    /// </summary>
    curve
}

/// <summary>
/// 鳥雑魚の種類
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
/// 地上雑魚の行動の種類
/// </summary>
public enum GroundEnemyActionType
{
    /// <summary>
    /// 停止（待機）
    /// </summary>
    stop,
    /// <summary>
    /// ジャンプ
    /// </summary>
    jump,
    /// <summary>
    /// 蟹歩き
    /// </summary>
    crabWalk,
    /// <summary>
    /// 魔法弾（直球）
    /// </summary>
    straightAttack,
    /// <summary>
    /// 投擲（山なり）
    /// </summary>
    throwingAttack,
}