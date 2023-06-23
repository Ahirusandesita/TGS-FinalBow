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
[RequireComponent(typeof(BirdStats), typeof(CashObjectInformation))]
public class BirdManager : MonoBehaviour
{
    [Tooltip("取得したBirdMoveクラス")]
    private BirdMoveBase _birdMoveBase = default;

    [Tooltip("取得したBirdStatsクラス")]
    private BirdStats _birdStats = default;


    private void Start()
    {
        _birdMoveBase = this.GetComponent<BirdMoveBase>();

        _birdStats = this.GetComponent<BirdStats>();

        // 召喚された雑魚じゃなければ攻撃開始
        if (!_birdStats.IsSummmon)
        {
            //StartCoroutine(_birdAttack.NormalAttackLoop(_attackSpawnPlace));
        }
    }

    private void Update()
    {
        // HPが0になったら死ぬ
        if (_birdStats.HP <= 0)
        {
            _birdStats.Death();
        }

        if (_birdStats.IsSummmon)
        {
            // 召喚された鳥の挙動
            return;
        }
    }
}