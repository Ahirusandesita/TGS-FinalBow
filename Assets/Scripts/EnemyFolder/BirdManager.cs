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
[RequireComponent(typeof(BirdStats), typeof(CashObjectInformation), typeof(BirdMoveComponents))]
public class BirdManager : MonoBehaviour
{
    [Tooltip("�擾����BirdMove�N���X")]
    private BirdMoveBase _birdMoveBase = default;

    [Tooltip("�擾����BirdStats�N���X")]
    private BirdStats _birdStats = default;

    [SerializeField]
    private DropData dropData;

    private Drop drop;


    private void Start()
    {
        _birdStats = this.GetComponent<BirdStats>();

        if (!_birdStats.IsSummmon)
        {
            _birdMoveBase = this.GetComponent<BirdMoveBase>();
        }
        drop = GameObject.FindObjectOfType<Drop>();
    }

    private void Update()
    {
        // HP��0�ɂȂ����玀��
        if (_birdStats.HP <= 0)
        {
            _birdStats.Death();
            drop.DropStart(dropData, this.transform.position);
        }
        // �|���Ȃ������瓦����
        else if (!_birdStats.IsSummmon && _birdMoveBase.NeedDespawn)
        {
            StartCoroutine(_birdMoveBase.SmallerAtDespawn());

            if (_birdMoveBase.IsChangeScaleComplete)
            {
                _birdStats.Despawn();
            }
        }

        if (_birdStats.IsSummmon)
        {
            // �������ꂽ���̋���
            return;
        }
    }
}