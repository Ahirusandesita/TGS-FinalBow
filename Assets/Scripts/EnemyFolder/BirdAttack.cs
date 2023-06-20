// --------------------------------------------------------- 
// BirdAttack.cs 
// 
// CreateDay: 2023/06/12
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BirdAttack : EnemyAttack
{
    [Tooltip("攻撃間の休憩秒数")]
    private WaitForSeconds _attackIntervalWait = default;

    // デバッグ用↓↓↓
    [SerializeField, Tooltip("一度に出す玉の数")]
    private int _numberOfFireBall = 3;

    [SerializeField, Tooltip("攻撃の間隔")]
    private float _attackIntervalTime = 3f;


    protected override void Start()
    {
        // PoolSystemのGetComponenet
        base.Start();

        // 設定した攻撃間隔をWaitForSecondsに代入
        _attackIntervalWait = new WaitForSeconds(_attackIntervalTime);
    }

    /// <summary>
    /// 雑魚：通常攻撃（ループ）
    /// </summary>
    /// <param name="spawnPlace">スポーンさせる位置</param>
    /// <returns></returns>
    public IEnumerator NormalAttackLoop(Transform spawnPlace)
    {
        while (true)
        {
            // 火の玉をスポーン
            SpawnEAttackFanForm(PoolEnum.PoolObjectType.fireBall, spawnPlace, _numberOfFireBall);

            // 攻撃間のブレイク
            yield return _attackIntervalWait;
        }
    }

    /// <summary>
    /// 雑魚：通常攻撃
    /// </summary>
    /// <param name="spawnPlace">スポーンさせる位置</param>
    public void NormalAttack(Transform spawnPlace)
    {
        // 火の玉をスポーン
        SpawnEAttackFanForm(PoolEnum.PoolObjectType.fireBall, spawnPlace, _numberOfFireBall);
    }
}