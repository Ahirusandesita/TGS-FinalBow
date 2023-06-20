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
        /// 火の玉（ボスの攻撃）
        /// </summary>
        fireBall,
        /// <summary>
        /// 鳥の雑魚
        /// </summary>
        bird ,
        /// <summary>
        /// アイテムの引き寄せ用
        /// </summary>
        targeter,
        /// <summary>
        /// 的
        /// </summary>
        targetObject
    }
}
