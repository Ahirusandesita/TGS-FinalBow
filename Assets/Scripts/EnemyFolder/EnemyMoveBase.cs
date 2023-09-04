// --------------------------------------------------------- 
// EnemyMoveBase.cs 
// 
// CreateDay: 2023/07/07
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

/// <summary>
/// 敵が持つ挙動クラスの定義
/// </summary>
public abstract class EnemyMoveBase : MonoBehaviour
{
    [Tooltip("自身のTransformをキャッシュ")]
    protected Transform _transform = default;

    protected virtual void Start()
    {
        _transform = this.transform;
    }

    protected virtual void Update()
    {
        MoveSequence();
    }

    /// <summary>
    /// 各ウェーブの敵の一連の挙動（イベントとして進行をまとめる）
    /// <para>Updateで呼ばれる</para>
    /// </summary>
    protected abstract void MoveSequence();
}