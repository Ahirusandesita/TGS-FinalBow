// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// �{�X�̊Ǘ��N���X
/// </summary>
// �R���|�[�l���g�̃A�^�b�`������
[RequireComponent(typeof(BossStats), typeof(BossMove), typeof(BossAttack))]
[RequireComponent(typeof(Animator))]
public class BossManager : MonoBehaviour
{
    [Tooltip("�擾����BossStats�N���X")]
    private BossStats _bossStats = default;

    [Tooltip("�擾����BossAttack�N���X")]
    private BossAttack _bossAttack = default;

    [Tooltip("�擾����BossMove�N���X")]
    private BossMove _bossMove = default;

    [Tooltip("�����܂ł̍s����")]
    private int _summonsIntervalTimes = 4;�@// ���̐���1�񏢊�

    [Tooltip("���ݎ���")]
    private float _currentTime = 0f;

    [Tooltip("�U���s���̊Ԋu")]
    private const float ATTACK_INTERVAL_TIME = 5f;

    private void Start()
    {
        _bossStats = this.GetComponent<BossStats>();

        _bossAttack = this.GetComponent<BossAttack>();

        _bossMove = this.GetComponent<BossMove>();

        StartCoroutine(FGStart());
    }

    private void Update()
    {
        // HP��0�ɂȂ����玀��
        if (_bossStats.HP <= 0)
        {
            _bossStats.Death();
        }

        _bossMove.MoveSelect();
    }

    private IEnumerator FGStart()
    {
        yield return new WaitForSeconds(1f);

        _bossMove.IsAttack = true;
        _bossAttack.SpawnBirdsForFireGatling();
    }
}