// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;

public class EAttackSpawner : MonoBehaviour
{
    [Tooltip("�v���C���[��Transform")]
    public Transform playerPlace = default;

    [Tooltip("���g��Transform�̃L���b�V��")]
    private Transform _transform = default;

    private void Start()
    {
        _transform = this.transform;
    }

    private void Update()
    {
        // �{�Ԃ̓C�x���g�t���O�������͈ړ��t���O����������
        //if (Input.GetKeyDown(KeyCode.U))
        //{
            // �v���C���[�̕����֊p�x��ς���
            _transform.LookAt(playerPlace);
        //}
    }
}
