// --------------------------------------------------------- 
// BirdManager.cs 
// 
// CreateDay: 2023/06/10
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// ���G���̊Ǘ��N���X
/// </summary>
// �R���|�[�l���g�̃A�^�b�`������
[RequireComponent(typeof(BirdStats), typeof(CashObjectInformation))]
public class BirdManager : MonoBehaviour
{
    [Tooltip("�擾����BirdMove�N���X")]
    private BirdMoveBase _birdMoveBase = default;

    [Tooltip("�擾����BirdStats�N���X")]
    private BirdStats _birdStats = default;


    private void Start()
    {
        _birdMoveBase = this.GetComponent<BirdMoveBase>();

        _birdStats = this.GetComponent<BirdStats>();

        // �������ꂽ�G������Ȃ���΍U���J�n
        if (!_birdStats.IsSummmon)
        {
            //StartCoroutine(_birdAttack.NormalAttackLoop(_attackSpawnPlace));
        }
    }

    private void Update()
    {
        // HP��0�ɂȂ����玀��
        if (_birdStats.HP <= 0)
        {
            _birdStats.Death();
        }

        if (_birdStats.IsSummmon)
        {
            // �������ꂽ���̋���
            return;
        }
    }
}