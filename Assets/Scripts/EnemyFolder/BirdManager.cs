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
[RequireComponent(typeof(BirdStats), typeof(BirdMove), typeof(CashObjectInformation))]
public class BirdManager : MonoBehaviour
{
    [Tooltip("タグの名前")]
    public TagObject _EnemyControllerTagData = default;


    [Tooltip("取得したBirdMoveクラス")]
    private BirdMove _birdMove = default;

    [Tooltip("取得したBirdStatsクラス")]
    private BirdStats _birdStats = default;

    [Tooltip("取得したBirdAttackクラス")]
    private BirdAttack _birdAttack = default;


    private void Start()
    {
        _birdMove = this.GetComponent<BirdMove>();

        _birdStats = this.GetComponent<BirdStats>();

        _birdAttack = GameObject.FindWithTag(_EnemyControllerTagData.TagName).GetComponent<BirdAttack>();

        // 召喚された雑魚じゃなければ攻撃開始
        if (!_birdStats.IsSummmon)
        {
            _birdMove._onBeforeAttack = _birdAttack.NormalAttack;

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

        _birdMove.MoveSelect();
    }
}