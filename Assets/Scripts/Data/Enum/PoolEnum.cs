// --------------------------------------------------------- 
// PoolEnum.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class PoolEnum : MonoBehaviour
{
    /// <summary>
    /// プール化したオブジェクト
    /// </summary>
    public enum PoolObjectType
    {
        /// <summary>
        /// 矢
        /// </summary>
        arrow,
        /// <summary>
        /// 通常の弾
        /// </summary>
        normalBullet,
        /// <summary>
        /// 爆発の弾
        /// </summary>
        bombBullet,
        /// <summary>
        /// 貫通の弾
        /// </summary>
        penetrateBullet,
        /// <summary>
        /// 電撃の弾
        /// </summary>
        thunderBullet,
        /// <summary>
        /// 鳥の雑魚
        /// </summary>
        bird,
        /// <summary>
        /// アイテムの引き寄せ用
        /// </summary>
        targeter,
        /// <summary>
        /// 的
        /// </summary>
        targetObject,
        /// <summary>
        /// ノーマルザコ鳥
        /// </summary>
        normalBird,
        /// <summary>
        /// 爆弾ザコ鳥
        /// </summary>
        bombBird,
        /// <summary>
        /// 貫通ザコ鳥
        /// </summary>
        penetrateBird,
        /// <summary>
        /// 雷ザコ鳥
        /// </summary>
        thunderBird,
        /// <summary>
        /// でかい爆弾ザコ鳥
        /// </summary>
        bombBirdBig,
        /// <summary>
        /// でかい雷ザコ鳥
        /// </summary>
        thunderBirdBig,
        /// <summary>
        /// でかい貫通ザコ鳥
        /// </summary>
        penetrateBirdBig,
        /// <summary>
        /// 地上の敵
        /// </summary>
        groundEnemy,
        /// <summary>
        /// 投擲する弾
        /// </summary>
        groundBullet,
        /// <summary>
        /// ドロップアイテム1
        /// </summary>
        dropItem_1,
        /// <summary>
        /// 敵に刺さる→
        /// </summary>
        usedArrow,
        /// <summary>
        /// 爆発時の肉片1
        /// </summary>
        birdBodyChip,
        /// <summary>
        /// 爆発時の肉片2
        /// </summary>
        birdBodyChipBig,
        /// <summary>
        /// 爆発時の肉片3
        /// </summary>
        birdBodyChipBigFire,
        /// <summary>
        /// 爆発時の肉片4
        /// </summary>
        birdBodyChipFire,
    }
}
