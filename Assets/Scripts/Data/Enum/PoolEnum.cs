// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
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
        /// 通常の玉
        /// </summary>
        normalBullet,
        /// <summary>
        /// 火の玉（ボスの攻撃）
        /// </summary>
        fireBullet,
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
        kantuBird,
        /// <summary>
        /// 雷ザコ鳥
        /// </summary>
        ThunderBird,
    }
}
