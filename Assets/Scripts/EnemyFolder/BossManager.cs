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
[RequireComponent(typeof(BossStats), typeof(BossMove), typeof(BossAttack))]
[RequireComponent(typeof(Animator))]
public class BossManager : MonoBehaviour
{
    [Tooltip("取得したBossStatsクラス")]
    private BossStats _bossStats = default;

    [Tooltip("取得したBossAttackクラス")]
    private BossAttack _bossAttack = default;

    [Tooltip("取得したBossMoveクラス")]
    private BossMove _bossMove = default;

    [Tooltip("召喚までの行動数")]
    private int _summonsIntervalTimes = 4;　// この数に1回召喚

    [Tooltip("現在時間")]
    private float _currentTime = 0f;

    [Tooltip("攻撃行動の間隔")]
    private const float ATTACK_INTERVAL_TIME = 5f;

    private void Start()
    {
        _bossStats = this.GetComponent<BossStats>();

        _bossAttack = this.GetComponent<BossAttack>();

        _bossMove = this.GetComponent<BossMove>();

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