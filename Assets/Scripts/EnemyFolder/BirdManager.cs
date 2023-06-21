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
[RequireComponent(typeof(BirdStats), typeof(BirdMove), typeof(CashObjectInformation))]
public class BirdManager : MonoBehaviour
{
    [Tooltip("�^�O�̖��O")]
    public TagObject _EnemyControllerTagData = default;


    [Tooltip("�擾����BirdMove�N���X")]
    private BirdMove _birdMove = default;

    [Tooltip("�擾����BirdStats�N���X")]
    private BirdStats _birdStats = default;

    [Tooltip("�擾����BirdAttack�N���X")]
    private BirdAttack _birdAttack = default;


    private void Start()
    {
        _birdMove = this.GetComponent<BirdMove>();

        _birdStats = this.GetComponent<BirdStats>();

        _birdAttack = GameObject.FindWithTag(_EnemyControllerTagData.TagName).GetComponent<BirdAttack>();

        // �������ꂽ�G������Ȃ���΍U���J�n
        if (!_birdStats.IsSummmon)
        {
            _birdMove._onBeforeAttack = _birdAttack.NormalAttack;

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

        _birdMove.MoveSelect();
    }
}