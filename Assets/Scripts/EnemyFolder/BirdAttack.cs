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
    /// <param name="selectedType">弾の種類</param>
    /// <param name="numberOfBullet">弾の数</param>
    /// <returns></returns>
    public IEnumerator NormalAttackLoop(Transform spawnPlace, PoolEnum.PoolObjectType selectedType, int numberOfBullet = 1)
    {
        while (true)
        {
            // 通常の玉をスポーン
            SpawnEAttackFanForm(selectedType, spawnPlace, numberOfBullet);

            // 攻撃間のブレイク
            yield return _attackIntervalWait;
        }
    }

    /// <summary>
    /// 雑魚：通常攻撃
    /// </summary>
    /// <param name="selectedType">弾の種類</param>
    /// <param name="spawnPlace">スポーンさせる位置</param>
    /// <param name="numberOfBullet">弾の数</param>
    public void NormalAttack(PoolEnum.PoolObjectType selectedType, Transform spawnPlace, int numberOfBullet = 1)
    {
        // 通常の玉をスポーン
        SpawnEAttackFanForm(selectedType, spawnPlace, numberOfBullet);
    }
}