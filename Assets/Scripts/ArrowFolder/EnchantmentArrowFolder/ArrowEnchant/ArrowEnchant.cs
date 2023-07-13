// --------------------------------------------------------- 
// ArrowEnchant.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

// �����������ɌĂ΂����
// ���n���G�����߂Ĉ����̒��g�킽��
public class ArrowEnchant : MonoBehaviour
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

    [Tooltip("�������S���̔��a")]
    [SerializeField] float _bombMiddleAreaSize = 6f;

    [Tooltip("�����O���̔��a")]
    [SerializeField] float _bombSideAreaSize = 30f;

    [Tooltip("�T���_�[�_���[�W")]
    [SerializeField] int _thunderDamage = 20;

    [Tooltip("�m�b�N�o�b�N�_���[�W")]
    [SerializeField] int _knockBackDamage = 20;

    [Tooltip("�z�[�~���O�_���[�W")]
    [SerializeField] int _homingDamage = 20;

    [Tooltip("�ђʃ_���[�W")]
    [SerializeField] int _penetrateDamage = 20;

    [Tooltip("�ǉ��_���[�W���")]
    [SerializeField] int _limitAddDamage = 10;

    [Tooltip("�P�ڂ̒ǉ��_���[�W")]
    [SerializeField] int _firstAddDamage = 10;

    [Tooltip("�Q�ڈȍ~�̒ǉ��_���[�W")]
    [SerializeField] int _AddDamage = 10;

    [Tooltip("�w�b�h�V���b�g�_���[�W�{��")]
    [SerializeField] float _headShotDamageMultiplier = 1.5f;

    #endregion

    private int addDamage = 0;

    readonly private string HeadTagName = InhallLibTags.HeadPointTag;

    /// <summary>
    /// hitObj��EnemyStats
    /// </summary>
    EnemyStats stats = default;

    /// <summary>
    /// �w�b�h�V���b�g���̔����̃N���X
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
    /// �M�~�b�N�Ƀq�b�g�������ɌĂ΂��
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
            X_Debug.LogError("����{���Ƀ{�^���H");
        }
    }

    
    /// <summary>
    /// �����_���[�W��^����hitObj��EnemyStats��������
    /// </summary>
    /// <param name="hitObj"></param>
    /// <returns>hitObj��EnemyStats</returns>
    private EnemyStats NormalHitDamage(GameObject hitObj,int damage)
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
        

        st.TakeDamage(damage);
        // ������
        addDamage = 0;
 
        //EnemyStats st = default;
        return st;
    }

    /// <summary>
    /// �����̃_���[�W�v�Z
    /// </summary>
    /// <param name="hitObj"></param>
    private void BombHitDamage(GameObject hitObj,Enchant enchant)
    {
        //TestBombArea(hitObj);
 
        // �����͈͓��̓G���X�L����
        Collider[] sideColliders = Physics.OverlapSphere(hitObj.transform.position, _bombSideAreaSize, _layerMask);
        // ���S���̓G���X�L����
        Collider[] middleColliders = Physics.OverlapSphere(hitObj.transform.position, _bombMiddleAreaSize, _layerMask);
        // side����middleColliders��r��
        HashSet<Collider> side = new HashSet<Collider>(sideColliders);

        side.ExceptWith(middleColliders);

        // ���������Q�[���I�u�W�F�N�g���Ԃ�����
        HashSet<GameObject> processedObject = new HashSet<GameObject>();

        // ���S���̃_���[�W���肩���ɍs��
        BombDamage(middleColliders, processedObject,_bombMiddleDamage);

        // �����O���̃_���[�W����𔚐S���_���[�W������s�����I�u�W�F�N�g�����O���čs��
        BombDamage(sideColliders, processedObject,_bombSideDamage);


        void BombDamage(Collider[] takeDamageColliders, HashSet<GameObject> processedObject,int damage)
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
    /// �z�����񂾗ʂɂ���ă_���[�W�グ�郁�\�b�h
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
