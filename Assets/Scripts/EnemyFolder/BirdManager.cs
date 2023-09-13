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
[RequireComponent(typeof(ReactionHitKnockBack))]
public class BirdManager : MonoBehaviour
{
    [Tooltip("�擾����BirdMove�N���X")]
    private BirdMoveBase _birdMoveBase = default;

    [Tooltip("�擾����BirdStats�N���X")]

    private BirdStats _birdStats = default;

    private void Start()
    {
        _birdStats = this.GetComponent<BirdStats>();

        if (!_birdStats.IsSummmon)
        {
            _birdMoveBase = this.GetComponent<BirdMoveBase>();
        }
    }

    private void Update()
    {
        // �|���Ȃ������瓦����
        //if (!_birdStats.IsSummmon && _birdMoveBase.NeedDespawn)
        //{
        //    StartCoroutine(_birdMoveBase.SmallerAtDespawn());

        //    if (_birdMoveBase.IsChangeScaleComplete)
        //    {
        //        _birdStats.Despawn();
        //    }
        //}

        if (_birdStats.IsSummmon)
        {
            // �������ꂽ���̋���
            return;
        }
    }
}