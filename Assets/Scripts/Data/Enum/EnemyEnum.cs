// --------------------------------------------------------- 
// EnemyEnum.cs 
// 
// CreateDay: 2023/06/28
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

/// <summary>
/// 敵の動きの種類
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
/// 敵の種類
/// </summary>
public enum EnemyType
{
    normalBird,
    bombBird,
    penetrateBird,
    thunderBird,
    bombBirdBig,
    thunderBirdBig,
}