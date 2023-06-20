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

    [Tooltip("�X�|�[���̃X�^�[�g�p�x�����߂�W��")]
    private const float START_SPAWN_ANGLE_COEFFICIENT = 8.1f;

    [Tooltip("�X�|�[�����̋ʂ̊Ԋu")]
    private const float SPACE_SPAWN_ANGLE_COEFFICIENT = 18f;


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
                    spawnPlace.eulerAngles.y + (START_SPAWN_ANGLE_COEFFICIENT * numberOfSpawn) - i * SPACE_SPAWN_ANGLE_COEFFICIENT, 
                    spawnPlace.eulerAngles.z));
        }
    }
}