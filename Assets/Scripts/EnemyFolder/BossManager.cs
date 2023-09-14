// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// ボスの管理クラス
/// </summary>
// コンポーネントのアタッチを強制
[RequireComponent(typeof(BossStats), typeof(BossActionClass), typeof(BossAttack))]
[RequireComponent(typeof(Animator))]
public class BossManager : MonoBehaviour
{
    [Tooltip("取得したBossStatsクラス")]
    private BossStats _bossStats = default;

    [Tooltip("取得したBossAttackクラス")]
    private BossAttack _bossAttack = default;

    [Tooltip("取得したBossMoveクラス")]
    private BossActionClass _bossMove = default;

    private void Start()
    {
        _bossStats = this.GetComponent<BossStats>();

        _bossAttack = this.GetComponent<BossAttack>();

        _bossMove = this.GetComponent<BossActionClass>();
     
        StartCoroutine(FGStart());
    }

    private void Update()
    {
        // HPが0になったら死ぬ
        if (_bossStats.HP <= 0)
        {
            _bossStats.Death();
        }

        _bossMove.MoveSelect();
    }

    private IEnumerator FGStart()
    {
        yield return new WaitForSeconds(1f);

        _bossMove.IsAttack = true;
        _bossAttack.SpawnBirdsForFireGatling();
    }
}