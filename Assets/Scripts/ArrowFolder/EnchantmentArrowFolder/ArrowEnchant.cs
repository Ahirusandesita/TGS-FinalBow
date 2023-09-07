// --------------------------------------------------------- 
// ArrowEnchant.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

interface IArrowEnchantDamageable
{
    void SetAttackDamage();
}
// 当たった時に呼ばれるやつら
// 情報渡す敵を決めて引数の中身わたす
[RequireComponent(typeof(ChainLightningManager))]
public class ArrowEnchant : MonoBehaviour, IArrowEnchantable<GameObject, EnchantmentEnum.EnchantmentState>
{

    private int _layerMask = 1 << 6 | 1 << 7;

    #region パラメータ

    [Tooltip("直撃基本ダメージ")]
    [SerializeField] int _normalDamage = 5;

    [Tooltip("ボム直撃ダメージ")]
    [SerializeField] int _bombDirectHitDamage = 0;

    [Tooltip("爆発中心部のダメージ")]
    [SerializeField] int _bombMiddleDamage = 20;

    [Tooltip("爆発外部のダメージ")]
    [SerializeField] int _bombSideDamage = 40;

    [Tooltip("爆発中心部の半径0-1(小数点%)")]
    [SerializeField] float _bombMiddleAreaSizePercent = 0.3f;
    

    [Tooltip("サンダーダメージ")]
    [SerializeField] int _thunderDamage = 20;

    [Tooltip("ノックバックダメージ")]
    [SerializeField] int _knockBackDamage = 20;

    [Tooltip("ホーミングダメージ")]
    [SerializeField] int _homingDamage = 20;

    [Tooltip("貫通ダメージ")]
    [SerializeField] int _penetrateDamage = 20;
 
    int _limitAddDamage = 10;

    [Tooltip("１個目の追加ダメージ")]
    [SerializeField] int _firstAddDamage = 10;

    [Tooltip("２個目以降の追加ダメージ")]
    [SerializeField] int _AddDamage = 10;

    [Tooltip("ヘッドショットダメージ倍率")]
    [SerializeField] float _headShotDamageMultiplier = 1.5f;

    [SerializeField] MaxInhallObjectSetting maxInhallData = default;

    #endregion

    private int addDamage = 0;

    private int enchantPower = 0;

    private int maxEnchantPower = 0;

    readonly private string HeadTagName = InhallLibTags.HeadPointTag;

    const float BOMB_FIRST_SIZE = 10f;

    /// <summary>
    /// hitObjのEnemyStats
    /// </summary>
    EnemyStats stats = default;

    /// <summary>
    /// ヘッドショット時の反応のクラス
    /// </summary>
    HeadShotEffects headShot = default;

    ChainLightningManager chain = default;

    Size size = default;
    enum Enchant
    {
        thunder,
        knockBack,
        homing,
        penetrate,
        none,
    }

    private void Start()
    {
        headShot = GetComponent<HeadShotEffects>();
        chain = GetComponent<ChainLightningManager>();
        size.firstSize = BOMB_FIRST_SIZE;
        maxEnchantPower = maxInhallData.GetMaxInhall;
        

        _limitAddDamage = _firstAddDamage + _AddDamage * (maxEnchantPower - 1);
    }

 
    /// <summary>
    /// ギミックにヒットした時に呼ばれる
    /// </summary>
    /// <param name="hitObj"></param>
    public void HitGimmick(GameObject hitObj)
    {
        if (hitObj.TryGetComponent<IFCanTakeArrowButton>(out IFCanTakeArrowButton button))
        {
            button.ButtonPush();
        }
        else
        {
            X_Debug.LogError("それ本当にボタン？");
        }
    }


    /// <summary>
    /// 直撃ダメージを与えてhitObjのEnemyStatsをかえす
    /// </summary>
    /// <param name="hitObj"></param>
    /// <returns>hitObjのEnemyStats</returns>
    private EnemyStats NormalHitDamage(GameObject hitObj, int damage,out int calced)
    {
        EnemyStats st = default;

        // ヘッドショットかどうか
        if (hitObj.CompareTag(HeadTagName))
        {
            // Effect
            headShot.PlayHeadShotEffect(hitObj.transform.position);

            // 倍率ダメージ切り捨て
            damage = Mathf.FloorToInt(damage * _headShotDamageMultiplier);

            // ステータス持っているのは親オブジェクトなので
            st = hitObj.transform.parent.GetComponent<EnemyStats>();
        }
        else
        {
            st = hitObj.GetComponent<EnemyStats>();
        }


        calced = damage;
        

        //EnemyStats st = default;
        return st;
    }

    

    private void AddInit()
    {
        // 初期化
        addDamage = 0;
        enchantPower = 0;
    }

    /// <summary>
    /// 爆発のダメージ計算
    /// </summary>
    /// <param name="hitObj"></param>
    private void BombHitDamage(GameObject hitObj, Enchant enchant)
    {
        //TestBombArea(hitObj);

        // 爆風範囲内の敵をスキャン
        float sideRadius = size.GetFirstSize + size.GetMinimumSize * size.plusCount;
        Collider[] sideColliders = Physics.OverlapSphere(hitObj.transform.position,
            sideRadius, _layerMask);
        // 爆心内の敵をスキャン
        Collider[] middleColliders = Physics.OverlapSphere(hitObj.transform.position,
            sideRadius * _bombMiddleAreaSizePercent, _layerMask);
        // sideからmiddleCollidersを排除
        HashSet<Collider> side = new HashSet<Collider>(sideColliders);

        side.ExceptWith(middleColliders);

        // 処理したゲームオブジェクトをぶち込む
        HashSet<GameObject> processedObject = new HashSet<GameObject>();

        // 爆心内のダメージ判定から先に行う
        BombDamage(middleColliders, processedObject, _bombMiddleDamage);

        // 爆発外部のダメージ判定を爆心内ダメージ判定を行ったオブジェクトを除外して行う
        BombDamage(sideColliders, processedObject, _bombSideDamage);

        size.plusCount = 0;


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

                takeBombStats = checkObject.GetComponent<EnemyStats>();

                if (takeBombStats != null)
                {

                    takeBombStats.TakeBomb(damage);

                    // 追加効果
                    switch (enchant)
                    {
                        case Enchant.homing:
                            //takeBombStats.TakeHoming();
                            break;
                        case Enchant.knockBack:
                            takeBombStats.TakeRapidShots();
                            break;
                        case Enchant.penetrate:
                            //takeBombStats.TakePenetrate();
                            break;
                        case Enchant.thunder:
                            takeBombStats.TakeThunder(enchantPower);
                            break;
                        default:
                            break;
                    }

                }

            }

        }
    }

    /// <summary>
    /// 吸い込んだ量によってダメージ上げるメソッド
    /// </summary>
    public void SetAttackDamage(int numberOfInhalled)
    {
        addDamage = 0;
        size.plusCount = numberOfInhalled;
        enchantPower = numberOfInhalled;

        if(numberOfInhalled > 0)
        {
            numberOfInhalled--;
            addDamage += _firstAddDamage;

            addDamage += numberOfInhalled * _AddDamage;
        }
        
    }



    private void TestBombArea(GameObject hi)
    {
        GameObject obj = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), hi.transform.position, Quaternion.identity);
        obj.transform.localScale = Vector3.one * (size.GetFirstSize + size.GetMinimumSize * size.plusCount);
    }

    public void Normal(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _normalDamage + addDamage, out int damage);
        stats.TakeDamage(damage);
        AddInit();
    }

    public void Bomb(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        BombHitDamage(t1, Enchant.none);
        stats = NormalHitDamage(t1, _bombDirectHitDamage + _normalDamage + addDamage,out int damage);
        stats.TakeDamage(damage);
        AddInit();
    }

    public void Thunder(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _thunderDamage + _normalDamage + addDamage, out int damage);
        stats.TakeThunder(enchantPower);
        chain.ChainLightning(t1.transform,enchantPower + 1,enchantPower);
        stats.TakeDamage(damage);
        AddInit();
    }

    public void RapidShots(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _knockBackDamage + _normalDamage + addDamage, out int damage);
        stats.TakeRapidShots();
        stats.TakeDamage(damage);
        AddInit();
    }

    public void Penetrate(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _penetrateDamage + _normalDamage + addDamage, out int damage);
        stats.TakePenetrate();
        stats.TakeDamage(damage);
        AddInit();
    }

    public void Homing(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _homingDamage + _normalDamage + addDamage, out int damage);
        stats.TakeHoming();
        stats.TakeDamage(damage);
        AddInit();
    }

    public void BombThunder(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _bombDirectHitDamage + _thunderDamage + _normalDamage + addDamage, out int damage);
        BombHitDamage(t1, Enchant.thunder);
        stats.TakeDamage(damage);
        AddInit();

    }

    public void BombKnockBack(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _bombDirectHitDamage + _knockBackDamage + _normalDamage + addDamage, out int damage);
        BombHitDamage(t1, Enchant.knockBack);
        stats.TakeDamage(damage);
        AddInit();

    }

    public void BombPenetrate(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _bombDirectHitDamage + _penetrateDamage + _normalDamage + addDamage, out int damage);
        BombHitDamage(t1, Enchant.penetrate);
        stats.TakeDamage(damage);
        AddInit();

    }

    public void BombHoming(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _bombDirectHitDamage + _homingDamage + _normalDamage + addDamage, out int damage);
        BombHitDamage(t1, Enchant.homing);
        stats.TakeDamage(damage);
        AddInit();

    }

    public void ThunderKnockBack(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _thunderDamage + _knockBackDamage + _normalDamage + addDamage, out int damage);

    }

    public void ThunderPenetrate(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _thunderDamage + _penetrateDamage + _normalDamage + addDamage, out int damage);
    }

    public void ThunderHoming(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _thunderDamage + _homingDamage + _normalDamage + addDamage, out int damage);
    }

    public void KnockBackPenetrate(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _knockBackDamage + _penetrateDamage + _normalDamage + addDamage, out int damage);
    }

    public void KnockBackHoming(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _knockBackDamage + _homingDamage + _normalDamage + addDamage, out int damage);
    }

    public void PenetrateHoming(GameObject t1, EnchantmentEnum.EnchantmentState t2)
    {
        stats = NormalHitDamage(t1, _penetrateDamage + _homingDamage + _normalDamage + addDamage, out int damage);
    }
}
