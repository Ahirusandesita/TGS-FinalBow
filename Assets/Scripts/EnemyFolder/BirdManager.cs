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
public class BirdManager : MonoBehaviour
{
    [Tooltip("取得したBirdMoveクラス")]
    private BirdMoveBase _birdMoveBase = default;

    [Tooltip("取得したBirdStatsクラス")]
    private BirdStats _birdStats = default;

    [SerializeField]
    private DropData dropData;

    private Drop drop;


    private void Start()
    {
        _birdStats = this.GetComponent<BirdStats>();

        if (!_birdStats.IsSummmon)
        {
            _birdMoveBase = this.GetComponent<BirdMoveBase>();
        }
        drop = GameObject.FindObjectOfType<Drop>();
    }

    private void Update()
    {
        // HPが0になったら死ぬ
        if (_birdStats.HP <= 0)
        {
            _birdStats.Death();
            drop.DropStart(dropData, this.transform.position);
        }
        // 倒せなかったら逃げる
        else if (!_birdStats.IsSummmon && _birdMoveBase.NeedDespawn)
        {
            StartCoroutine(_birdMoveBase.SmallerAtDespawn());

            if (_birdMoveBase.IsChangeScaleComplete)
            {
                _birdStats.Despawn();
            }
        }

        if (_birdStats.IsSummmon)
        {
            // 召喚された鳥の挙動
            return;
        }
    }
}