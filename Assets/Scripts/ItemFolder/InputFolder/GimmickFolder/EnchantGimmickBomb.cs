// --------------------------------------------------------- 
// EnchantGimmickBomb.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ボム誘爆用
/// </summary>
interface IFUseEnchantGimmickTakeBomb
{
    void TakeBomb();
}
public class EnchantGimmickBomb : MonoBehaviour, IFUseEnchantGimmick, IFUseEnchantGimmickTakeBomb
{
    [SerializeField] float _middleBombAreaSizePercent = 0.3f;

    [SerializeField] int _middleDamage = 10;
    [SerializeField] int _sideDamage = 4;
    [SerializeField] float _bombAreaSize = 5f;

    [SerializeField] int _bombEnchantMiddleDamage = 16;
    [SerializeField] int _bombEnchantSideDamage = 8;
    [SerializeField] float _bombEnchantAreaSize = 10;

    [SerializeField] GameObject[] active;

    [SerializeField] Collider cl;

    ObjectPoolSystem pool = default;

    [SerializeField] float _overlapSize = 10f;


    bool used = false;
    struct Bomb
    {
        public float middleSize;
        public float sideSize;
        public int middleDamage;
        public int sideDamage;
    }


    void Start()
    {
        pool = GameObject.FindWithTag(InhallLibTags.PoolSystem).GetComponent<ObjectPoolSystem>();
    }

    private int _layerMask = 1 << 6 | 1 << 7 | 1 << 10;
    private string HeadTagName = InhallLibTags.HeadPointTag;
    public void CallAction(EnchantmentEnum.EnchantmentState enchantment)
    {
        StartExp(enchantment);
    }

    public void TakeBomb()
    {
        StartExp(EnchantmentEnum.EnchantmentState.bomb);
    }

    /// <summary>
    /// 爆発の準備
    /// </summary>
    /// <param name="enchantment"></param>
    private void StartExp(EnchantmentEnum.EnchantmentState enchantment)
    {
        if (used)
        {
            return;
        }

        used = true;
        Bomb bomb = new Bomb();
        if (enchantment == EnchantmentEnum.EnchantmentState.bomb)
        {
            bomb.middleSize = _bombEnchantAreaSize * _middleBombAreaSizePercent;
            bomb.sideSize = _bombEnchantAreaSize;
            bomb.middleDamage = _bombEnchantMiddleDamage;
            bomb.sideDamage = _bombEnchantSideDamage;
        }
        else
        {
            bomb.middleSize = _bombAreaSize * _middleBombAreaSizePercent;
            bomb.sideSize = _bombAreaSize;
            bomb.middleDamage = _middleDamage;
            bomb.sideDamage = _sideDamage;
        }
        Effect(bomb);
        BombHitDamage(transform.position, bomb);
        // かり
        GameObject.FindObjectOfType<ArrowEnchantSound>().Bomb(this.GetComponent<AudioSource>());


        foreach (GameObject obj in active)
        {
            obj.SetActive(false);
            cl.enabled = false;
        }
    }

    private void Effect(Bomb bomb)
    {
        GameObject obj = pool.CallObject(EffectPoolEnum.EffectPoolState.bomb, transform.position, Quaternion.identity);
        obj.transform.localScale += Vector3.one * bomb.sideSize;
    }
    /// <summary>
    /// 爆発のダメージ計算
    /// </summary>
    /// <param name="hitObjPosition"></param>
    private void BombHitDamage(Vector3 hitObjPosition, Bomb bomb)
    {

        // 爆風範囲内の敵をスキャン
        float sideRadius = bomb.sideSize * _overlapSize;
        Collider[] sideColliders = Physics.OverlapSphere(hitObjPosition,
            sideRadius, _layerMask);
        // 爆心内の敵をスキャン
        Collider[] middleColliders = Physics.OverlapSphere(hitObjPosition,
            bomb.middleSize, _layerMask);
        // sideからmiddleCollidersを排除
        HashSet<Collider> side = new HashSet<Collider>(sideColliders);

        side.ExceptWith(middleColliders);

        // 処理したゲームオブジェクトをぶち込む
        HashSet<GameObject> processedObject = new HashSet<GameObject>();



        // 爆心内のダメージ判定から先に行う
        BombDamage(middleColliders, processedObject, bomb.middleDamage);

        // 爆発外部のダメージ判定を爆心内ダメージ判定を行ったオブジェクトを除外して行う
        BombDamage(sideColliders, processedObject, bomb.sideDamage);



        void BombDamage(Collider[] takeDamageColliders, HashSet<GameObject> processedObject, int damage)
        {
            // このメソッドで影響のでることが決まったEnemyStatsを入れる
            EnemyStats takeBombStats = default;
            // 爆心内ダメージ
            foreach (Collider inCollider in takeDamageColliders)
            {
                // コライダーのゲームオブジェクト
                GameObject checkObject = inCollider.gameObject;


                // 取得したゲームオブジェクトがヘッドショットの判定用のものなら次ループ
                if (checkObject.CompareTag(HeadTagName)) continue;

                // 取得したゲームオブジェクトが処理済みなら次ループ
                if (processedObject.Contains(checkObject)) continue;

                // 処理済みゲームオブジェクトに登録
                processedObject.Add(checkObject);

                if (checkObject.TryGetComponent<IFUseEnchantGimmickTakeBomb>(out IFUseEnchantGimmickTakeBomb gimmick))
                {
                    gimmick.TakeBomb();
                    continue;
                }
                else
                {
                    takeBombStats = checkObject.GetComponent<EnemyStats>();

                }

                if (takeBombStats != null)
                {

                    takeBombStats.TakeBomb(damage);
                }

            }

        }


    }


}