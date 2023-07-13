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
    [Tooltip("�U���Ԃ̋x�e�b��")]
    private WaitForSeconds _attackIntervalWait = default;

    [SerializeField, Tooltip("�U���̊Ԋu")]
    private float _attackIntervalTime = 3f;


    protected override void Start()
    {
        // PoolSystem��GetComponenet
        base.Start();

        // �ݒ肵���U���Ԋu��WaitForSeconds�ɑ��
        _attackIntervalWait = new WaitForSeconds(_attackIntervalTime);
    }

    /// <summary>
    /// �G���F�ʏ�U���i���[�v�j
    /// </summary>
    /// <param name="spawnPlace">�X�|�[��������ʒu</param>
    /// <param name="selectedType">�e�̎��</param>
    /// <param name="numberOfBullet">�e�̐�</param>
    /// <returns></returns>
    public IEnumerator NormalAttackLoop(Transform spawnPlace, PoolEnum.PoolObjectType selectedType, int numberOfBullet = 1)
    {
        while (true)
        {
            // �ʏ�̋ʂ��X�|�[��
            SpawnEAttackFanForm(selectedType, spawnPlace, numberOfBullet);

            // �U���Ԃ̃u���C�N
            yield return _attackIntervalWait;
        }
    }

    /// <summary>
    /// �G���F�ʏ�U��
    /// </summary>
    /// <param name="selectedType">�e�̎��</param>
    /// <param name="spawnPlace">�X�|�[��������ʒu</param>
    /// <param name="numberOfBullet">�e�̐�</param>
    public void NormalAttack(PoolEnum.PoolObjectType selectedType, Transform spawnPlace, int numberOfBullet = 1)
    {
        // �ʏ�̋ʂ��X�|�[��
        SpawnEAttackFanForm(selectedType, spawnPlace, numberOfBullet);
    }
}