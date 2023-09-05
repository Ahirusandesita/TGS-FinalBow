// --------------------------------------------------------- 
// EAttackSpawner.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;

public class EAttackSpawner : MonoBehaviour
{
    [Tooltip("�v���C���[��Transform")]
    private Transform _playerPlace = default;

    [Tooltip("���g��Transform�̃L���b�V��")]
    private Transform _transform = default;

    [Tooltip("�X�e�[�W�̐��ʂ̃x�N�g��")]
    private Vector3 _stageFrontPosition = default;

    /// <summary>
    /// �X�e�[�W�̐��ʂ̃x�N�g��
    /// </summary>
    public Vector3 StageFrontPosition
    {
        set { _stageFrontPosition = value; }
    }

    private void Start()
    {
        _transform = this.transform;
        _playerPlace = GameObject.FindWithTag("PlayerController").transform;

        // �Ăяo���R���h�����߁A�܂��́u�v���C���[�ɍU���v�ŏ�����
        RotateAttackSpawner(DirectionType_AtAttack.player);
    }

    /// <summary>
    /// �U�������̕ύX
    /// </summary>
    /// <param name="attackDirection"></param>
    public void RotateAttackSpawner(DirectionType_AtAttack attackDirection)
    {
        switch (attackDirection)
        {
            case DirectionType_AtAttack.player:

                // �v���C���[�̕����֊p�x��ς���
                _transform.LookAt(_playerPlace);

                break;
            case DirectionType_AtAttack.front:

                // �X�e�[�W�̐��ʕ���������
                _transform.LookAt(_stageFrontPosition);

                break;
        }
    }
}
