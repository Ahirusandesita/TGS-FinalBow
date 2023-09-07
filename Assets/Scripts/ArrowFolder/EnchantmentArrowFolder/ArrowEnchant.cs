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
// �����������ɌĂ΂����
// ���n���G�����߂Ĉ����̒��g�킽��
[RequireComponent(typeof(ChainLightningManager))]
public class ArrowEnchant : MonoBehaviour, IArrowEnchantable<GameObject, EnchantmentEnum.EnchantmentState>
{

    private int _layerMask = 1 << 6 | 1 << 7;

    #region �p�����[�^

    [Tooltip("������{�_���[�W")]
    [SerializeField] int _normalDamage = 5;

    [Tooltip("�{�������_���[�W")]
    [SerializeField] int _bombDirectHitDamage = 0;

    [Tooltip("�������S���̃_���[�W")]
    [SerializeField] int _bombMiddleDamage = 20;

    [Tooltip("�����O���̃_���[�W")]
    [SerializeField] int _bombSideDamage = 40;

    [Tooltip("�������S���̔��a0-1(�����_%)")]
    [SerializeField] float _bombMiddleAreaSizePercent = 0.3f;
    

    [Tooltip("�T���_�[�_���[�W")]
    [SerializeField] int _thunderDamage = 20;

    [Tooltip("�m�b�N�o�b�N�_���[�W")]
    [SerializeField] int _knockBackDamage = 20;

    [Tooltip("�z�[�~���O�_���[�W")]
    [SerializeField] int _homingDamage = 20;

    [Tooltip("�ђʃ_���[�W")]
    [SerializeField] int _penetrateDamage = 20;
 
    int _limitAddDamage = 10;

    [Tooltip("�P�ڂ̒ǉ��_���[�W")]
    [SerializeField] int _firstAddDamage = 10;

    [Tooltip("�Q�ڈȍ~�̒ǉ��_���[�W")]
    [SerializeField] int _AddDamage = 10;

    [Tooltip("�w�b�h�V���b�g�_���[�W�{��")]
    [SerializeField] float _headShotDamageMultiplier = 1.5f;

    [SerializeField] MaxInhallObjectSetting maxInhallData = default;

    #endregion

    private int addDamage = 0;

    private int enchantPower = 0;

    private int maxEnchantPower = 0;

    readonly private string HeadTagName = InhallLibTags.HeadPointTag;

    const float BOMB_FIRST_SIZE = 10f;

    /// <summary>
    /// hitObj��EnemyStats
    /// </summary>
    EnemyStats stats = default;

    /// <summary>
    /// �w�b�h�V���b�g���̔����̃N���X
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
    /// �M�~�b�N�Ƀq�b�g�������ɌĂ΂��
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
            X_Debug.LogError("����{���Ƀ{�^���H");
        }
    }


    /// <summary>
    /// �����_���[�W��^����hitObj��EnemyStats��������
    /// </summary>
    /// <param name="hitObj"></param>
    /// <returns>hitObj��EnemyStats</returns>
    private EnemyStats NormalHitDamage(GameObject hitObj, int damage,out int calced)
    {
        EnemyStats st = default;

        // �w�b�h�V���b�g���ǂ���
        if (hitObj.CompareTag(HeadTagName))
        {
            // Effect
            headShot.PlayHeadShotEffect(hitObj.transform.position);

            // �{���_���[�W�؂�̂�
            damage = Mathf.FloorToInt(damage * _headShotDamageMultiplier);

            // �X�e�[�^�X�����Ă���̂͐e�I�u�W�F�N�g�Ȃ̂�
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
        // ������
        addDamage = 0;
        enchantPower = 0;
    }

    /// <summary>
    /// �����̃_���[�W�v�Z
    /// </summary>
    /// <param name="hitObj"></param>
    private void BombHitDamage(GameObject hitObj, Enchant enchant)
    {
        //TestBombArea(hitObj);

        // �����͈͓��̓G���X�L����
        float sideRadius = size.GetFirstSize + size.GetMinimumSize * size.plusCount;
        Collider[] sideColliders = Physics.OverlapSphere(hitObj.transform.position,
            sideRadius, _layerMask);
        // ���S���̓G���X�L����
        Collider[] middleColliders = Physics.OverlapSphere(hitObj.transform.position,
            sideRadius * _bombMiddleAreaSizePercent, _layerMask);
        // side����middleColliders��r��
        HashSet<Collider> side = new HashSet<Collider>(sideColliders);

        side.ExceptWith(middleColliders);

        // ���������Q�[���I�u�W�F�N�g���Ԃ�����
        HashSet<GameObject> processedObject = new HashSet<GameObject>();

        // ���S���̃_���[�W���肩���ɍs��
        BombDamage(middleColliders, processedObject, _bombMiddleDamage);

        // �����O���̃_���[�W����𔚐S���_���[�W������s�����I�u�W�F�N�g�����O���čs��
        BombDamage(sideColliders, processedObject, _bombSideDamage);

        size.plusCount = 0;


        void BombDamage(Collider[] takeDamageColliders, HashSet<GameObject> processedObject, int damage)
        {
            // ���̃��\�b�h�ŉe���̂ł邱�Ƃ����܂���EnemyStats������
            EnemyStats takeBombStats = default;
            // ���S���_���[�W
            foreach (Collider inCollider in takeDamageColliders)
            {
                // �R���C�_�[�̃Q�[���I�u�W�F�N�g
                GameObject checkObject = inCollider.gameObject;

                // �擾�����Q�[���I�u�W�F�N�g���w�b�h�V���b�g�̔���p�̂��̂Ȃ玟���[�v
                if (checkObject.CompareTag(HeadTagName)) continue;

                // �擾�����Q�[���I�u�W�F�N�g�������ς݂Ȃ玟���[�v
                if (processedObject.Contains(checkObject)) continue;

                // �����ς݃Q�[���I�u�W�F�N�g�ɓo�^
                processedObject.Add(checkObject);

                takeBombStats = checkObject.GetComponent<EnemyStats>();

                if (takeBombStats != null)
                {

                    takeBombStats.TakeBomb(damage);

                    // �ǉ�����
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
    /// �z�����񂾗ʂɂ���ă_���[�W�グ�郁�\�b�h
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
