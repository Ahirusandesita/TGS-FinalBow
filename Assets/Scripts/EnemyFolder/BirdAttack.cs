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

    // �f�o�b�O�p������
    [SerializeField, Tooltip("��x�ɏo���ʂ̐�")]
    private int _numberOfFireBall = 3;

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
    /// <returns></returns>
    public IEnumerator NormalAttackLoop(Transform spawnPlace)
    {
        while (true)
        {
            // �΂̋ʂ��X�|�[��
            SpawnEAttackFanForm(PoolEnum.PoolObjectType.fireBall, spawnPlace, _numberOfFireBall);

            // �U���Ԃ̃u���C�N
            yield return _attackIntervalWait;
        }
    }

    /// <summary>
    /// �G���F�ʏ�U��
    /// </summary>
    /// <param name="spawnPlace">�X�|�[��������ʒu</param>
    public void NormalAttack(Transform spawnPlace)
    {
        // �΂̋ʂ��X�|�[��
        SpawnEAttackFanForm(PoolEnum.PoolObjectType.fireBall, spawnPlace, _numberOfFireBall);
    }
}