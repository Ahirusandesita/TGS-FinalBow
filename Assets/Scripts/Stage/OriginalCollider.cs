// --------------------------------------------------------- 
// Floor.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �X�e�[�W�Q�̏��Ɏg�p����N���X
/// </summary>
public class OriginalCollider : ColliderObjectBase
{
    #region method

    private void Awake()
    {
        //���N���X��Add����@�����̖�����������Lock�͂܂�
        ContainObject.originalColliders.Add(this);
    }

    protected override void HitScaleSizeSetting()
    {

        //�����_���[�ɍ��킹���R���C�_�[���쐬����
        Bounds bounds = GetComponent<Renderer>().bounds;
        Vector3 size = bounds.size;
        //_hitDistanceScale._hitDistanceX = 95.35f;
        //_hitDistanceScale._hitDistanceY = 23.3f;
        //_hitDistanceScale._hitDistanceZ = 96.41f;

        _hitDistanceScale._hitDistanceX = size.x / 2f;
        _hitDistanceScale._hitDistanceY = size.y / 2f;
        _hitDistanceScale._hitDistanceZ = size.z / 2f;

        Vector3 vector = default;
        vector.x = X;
        vector.y = Y;
        vector.z = Z;

        //�R���C�_�[���C���X�^���X
        _hitZone = new HitZone(_hitDistanceScale, this.transform.position + vector);
    }
    #endregion
}