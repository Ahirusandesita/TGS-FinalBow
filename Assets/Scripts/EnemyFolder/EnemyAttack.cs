// --------------------------------------------------------- 
// EnemyAttack.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Tooltip("�^�O�̖��O")]
    public TagObject _PoolSystemTagData = default;


    [Tooltip("�擾����ObjectPoolSystem�N���X")]
    protected ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("�e�̊Ԋu�̊p�x")]
    private const float SPACE_ANGLE_COEFFICIENT = 8.1f;


    protected virtual void Start()
    {
        _objectPoolSystem = GameObject.FindWithTag(_PoolSystemTagData.TagName).GetComponent<ObjectPoolSystem>();
    }

    /// <summary>
    /// �G�̍U������ɃX�|�[������
    /// </summary>
    /// <param name="spawnObjectType">�Ăяo���I�u�W�F�N�g�̎��</param>
    /// <param name="spawnPlace">�X�|�[��������I�u�W�F�N�g��Transform���</param>
    /// <param name="numberOfSpawn">��x�ɃX�|�[�������</param>
    protected virtual void SpawnEAttackFanForm(PoolEnum.PoolObjectType spawnObjectType, Transform spawnPlace, int numberOfSpawn)
    {
        // ��x�̃X�|�[���ʕ��A�J��Ԃ�
        for (int i = 0; i < numberOfSpawn; i++)
        {
            // ���͈̔͂ɁA�n���ꂽ�����ϓ��Ԋu�Ŕz�u����
            // �X�|�[���ʒu�͓����ŁA�p�x������ς���
            // ��͈̔͂̓X�|�[���ʂɂ���ď���������
            _objectPoolSystem.CallObject(
                spawnObjectType,
                spawnPlace.position,
                Quaternion.Euler(
                    spawnPlace.eulerAngles.x, 
                    spawnPlace.eulerAngles.y + (SPACE_ANGLE_COEFFICIENT * (numberOfSpawn / 2)) - i * SPACE_ANGLE_COEFFICIENT, 
                    spawnPlace.eulerAngles.z));
        }
    }
}