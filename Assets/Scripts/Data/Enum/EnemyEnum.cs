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
/// 鳥雑魚の攻撃方法の種類
/// </summary>
public enum BirdAttackType
{
    /// <summary>
    /// 等間隔
    /// </summary>
    equalIntervals,
    /// <summary>
    /// 秒数指定
    /// </summary>
    specifySeconds,
    /// <summary>
    /// 連続攻撃
    /// </summary>
    consecutive,
    /// <summary>
    /// 攻撃しない
    /// </summary>
    none
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

/// <summary>
/// ループの種類
/// </summary>
public enum RoopType
{
    /// <summary>
    /// スタートから繰り返す（順行）
    /// </summary>
    forward,
    /// <summary>
    /// その場から折り返す（逆行）
    /// </summary>
    reverse
}

/// <summary>
/// 敵の方向の種類
/// </summary>
public enum DirectionType
{
    /// <summary>
    /// プレイヤー方向
    /// </summary>
    player,
    /// <summary>
    /// プレイヤーを基準としたワールド正面
    /// </summary>
    front,
    /// <summary>
    /// 進行方向
    /// </summary>
    moveDirection
}