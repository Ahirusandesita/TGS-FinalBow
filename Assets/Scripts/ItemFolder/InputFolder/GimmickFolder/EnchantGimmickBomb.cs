// --------------------------------------------------------- 
// EnchantGimmickBomb.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// �{���U���p
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
    /// �����̏���
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
        // ����
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
    /// �����̃_���[�W�v�Z
    /// </summary>
    /// <param name="hitObjPosition"></param>
    private void BombHitDamage(Vector3 hitObjPosition, Bomb bomb)
    {

        // �����͈͓��̓G���X�L����
        float sideRadius = bomb.sideSize * _overlapSize;
        Collider[] sideColliders = Physics.OverlapSphere(hitObjPosition,
            sideRadius, _layerMask);
        // ���S���̓G���X�L����
        Collider[] middleColliders = Physics.OverlapSphere(hitObjPosition,
            bomb.middleSize, _layerMask);
        // side����middleColliders��r��
        HashSet<Collider> side = new HashSet<Collider>(sideColliders);

        side.ExceptWith(middleColliders);

        // ���������Q�[���I�u�W�F�N�g���Ԃ�����
        HashSet<GameObject> processedObject = new HashSet<GameObject>();



        // ���S���̃_���[�W���肩���ɍs��
        BombDamage(middleColliders, processedObject, bomb.middleDamage);

        // �����O���̃_���[�W����𔚐S���_���[�W������s�����I�u�W�F�N�g�����O���čs��
        BombDamage(sideColliders, processedObject, bomb.sideDamage);



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