// --------------------------------------------------------- 
// ArrowEnchant.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

// 当たった時に呼ばれるやつら
// 情報渡す敵を決めて引数の中身わたす
public class ArrowEnchant : MonoBehaviour
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

    [Tooltip("爆発中心部の半径")]
    [SerializeField] float _bombMiddleAreaSize = 6f;

    [Tooltip("爆発外部の半径")]
    [SerializeField] float _bombSideAreaSize = 30f;

    [Tooltip("サンダーダメージ")]
    [SerializeField] int _thunderDamage = 20;

    [Tooltip("ノックバックダメージ")]
    [SerializeField] int _knockBackDamage = 20;

    [Tooltip("ホーミングダメージ")]
    [SerializeField] int _homingDamage = 20;

    [Tooltip("貫通ダメージ")]
    [SerializeField] int _penetrateDamage = 20;

    [Tooltip("追加ダメージ上限")]
    [SerializeField] int _limitAddDamage = 10;

    [Tooltip("１個目の追加ダメージ")]
    [SerializeField] int _firstAddDamage = 10;

    [Tooltip("２個目以降の追加ダメージ")]
    [SerializeField] int _AddDamage = 10;

    [Tooltip("ヘッドショットダメージ倍率")]
    [SerializeField] float _headShotDamageMultiplier = 1.5f;

    #endregion

    private int addDamage = 0;

    readonly private string HeadTagName = InhallLibTags.HeadPointTag;

    /// <summary>
    /// hitObjのEnemyStats
    /// </summary>
    EnemyStats stats = default;

    /// <summary>
    /// ヘッドショット時の反応のクラス
    /// </summary>
    HeadShotEffects headShot = default;
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
    }

    public void ArrowEnchantment_Normal(GameObject hitObj,EnchantmentEnum.EnchantmentState state)
    {
        stats = NormalHitDamage(hitObj,_normalDamage + addDamage);
    }

   

    public void ArrowEnchantment_Bomb(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
      
        stats = NormalHitDamage(hitObj,_bombDirectHitDamage + _normalDamage + addDamage);

        BombHitDamage(hitObj,Enchant.none);
    }


    public void ArrowEnchantment_Thunder(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_thunderDamage + _normalDamage + addDamage);

        stats.TakeThunder();
    }





    public void ArrowEnchantment_KnockBack(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_knockBackDamage + _normalDamage + addDamage);

        stats.TakeKnockBack();

    }





    public void ArrowEnchantment_Homing(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_homingDamage + _normalDamage + addDamage);

    }





    public void ArrowEnchantment_Penetrate(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_penetrateDamage + _normalDamage + addDamage);

    }





    public void ArrowEnchantment_BombThunder(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_bombDirectHitDamage + _thunderDamage + _normalDamage + addDamage);

        BombHitDamage(hitObj, Enchant.thunder);
    }





    public void ArrowEnchantment_BombKnockBack(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_bombDirectHitDamage + _knockBackDamage + _normalDamage + addDamage);

        BombHitDamage(hitObj, Enchant.knockBack);
    }





    public void ArrowEnchantment_BombHoming(GameObject hitObj, EnchantmentEnum.EnchantmentState state)
    {
        
        stats = NormalHitDamage(hitObj,_bombDirectHitDamage + _homingDamage + _normalDamage + addDamage);

        BombHitDamage(hitObj, Enchant.homing);
    }





    public void ArrowEnchantment_BombPenetrate(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_bombDirectHitDamage + _penetrateDamage + _normalDamage + addDamage);

        BombHitDamage(hitObj, Enchant.penetrate);
    }





    public void ArrowEnchantment_ThunderKnockBack(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_thunderDamage + _knockBackDamage + _normalDamage + addDamage);

        stats.TakeThunder();
        stats.TakeKnockBack();
    }





    public void ArrowEnchantment_ThunderHoming(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_thunderDamage + _homingDamage + _normalDamage + addDamage);

        stats.TakeThunder();

    }





    public void ArrowEnchantment_ThunderPenetrate(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_thunderDamage + _penetrateDamage + _normalDamage + addDamage);

        stats.TakeThunder();
    }





    public void ArrowEnchantment_KnockBackHoming(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
       
        stats = NormalHitDamage(hitObj, _knockBackDamage + _homingDamage + _normalDamage + addDamage);

        stats.TakeKnockBack();
    }





    public void ArrowEnchantment_KnockBackPenetrate(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_knockBackDamage + _penetrateDamage + _normalDamage + addDamage);

        stats.TakeKnockBack();

    }





    public void ArrowEnchantment_HomingPenetrate(GameObject hitObj, EnchantmentEnum.EnchantmentState state) 
    {
        
        stats = NormalHitDamage(hitObj,_homingDamage + _penetrateDamage + _normalDamage + addDamage);

    }

    /// <summary>
    /// ギミックにヒットした時に呼ばれる
    /// </summary>
    /// <param name="hitObj"></param>
    public void HitGimmick(GameObject hitObj)
    {
        if(hitObj.TryGetComponent<IFCanTakeArrowButton>(out IFCanTakeArrowButton button))
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
    private EnemyStats NormalHitDamage(GameObject hitObj,int damage)
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
        

        st.TakeDamage(damage);
        // 初期化
        addDamage = 0;
 
        //EnemyStats st = default;
        return st;
    }

    /// <summary>
    /// 爆発のダメージ計算
    /// </summary>
    /// <param name="hitObj"></param>
    private void BombHitDamage(GameObject hitObj,Enchant enchant)
    {
        //TestBombArea(hitObj);
 
        // 爆風範囲内の敵をスキャン
        Collider[] sideColliders = Physics.OverlapSphere(hitObj.transform.position, _bombSideAreaSize, _layerMask);
        // 爆心内の敵をスキャン
        Collider[] middleColliders = Physics.OverlapSphere(hitObj.transform.position, _bombMiddleAreaSize, _layerMask);
        // sideからmiddleCollidersを排除
        HashSet<Collider> side = new HashSet<Collider>(sideColliders);

        side.ExceptWith(middleColliders);

        // 処理したゲームオブジェクトをぶち込む
        HashSet<GameObject> processedObject = new HashSet<GameObject>();

        // 爆心内のダメージ判定から先に行う
        BombDamage(middleColliders, processedObject,_bombMiddleDamage);

        // 爆発外部のダメージ判定を爆心内ダメージ判定を行ったオブジェクトを除外して行う
        BombDamage(sideColliders, processedObject,_bombSideDamage);


        void BombDamage(Collider[] takeDamageColliders, HashSet<GameObject> processedObject,int damage)
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
                            takeBombStats.TakeKnockBack();
                            break;
                        case Enchant.penetrate:
                            //takeBombStats.TakePenetrate();
                            break;
                        case Enchant.thunder:
                            takeBombStats.TakeThunder();
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
    public void SetAttackDamage()
    {
        if(addDamage == 0)
        {
            addDamage += _firstAddDamage; 
        }
        else
        {
            addDamage += addDamage;
        }

        if(addDamage > _limitAddDamage)
        {
            addDamage = _limitAddDamage;
        }
    }



    private void TestBombArea(GameObject hi)
    {
        GameObject obj = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), hi.transform.position, Quaternion.identity);
        obj.transform.localScale = Vector3.one * _bombSideAreaSize * 2;
    }
}
