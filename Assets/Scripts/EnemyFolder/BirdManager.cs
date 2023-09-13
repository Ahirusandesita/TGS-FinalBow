// --------------------------------------------------------- 
// BirdManager.cs 
// 
// CreateDay: 2023/06/10
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// 鳥雑魚の管理クラス
/// </summary>
// コンポーネントのアタッチを強制
[RequireComponent(typeof(BirdStats), typeof(CashObjectInformation), typeof(BirdMoveComponents))]
[RequireComponent(typeof(ReactionHitKnockBack))]
public class BirdManager : MonoBehaviour
{
    [Tooltip("取得したBirdMoveクラス")]
    private BirdMoveBase _birdMoveBase = default;

    [Tooltip("取得したBirdStatsクラス")]

    private BirdStats _birdStats = default;

    private void Start()
    {
        _birdStats = this.GetComponent<BirdStats>();

        if (!_birdStats.IsSummmon)
        {
            _birdMoveBase = this.GetComponent<BirdMoveBase>();
        }
    }

    private void Update()
    {
        // 倒せなかったら逃げる
        //if (!_birdStats.IsSummmon && _birdMoveBase.NeedDespawn)
        //{
        //    StartCoroutine(_birdMoveBase.SmallerAtDespawn());

        //    if (_birdMoveBase.IsChangeScaleComplete)
        //    {
        //        _birdStats.Despawn();
        //    }
        //}

        if (_birdStats.IsSummmon)
        {
            // 召喚された鳥の挙動
            return;
        }
    }
}