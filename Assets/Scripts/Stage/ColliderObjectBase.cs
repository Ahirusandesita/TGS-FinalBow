// --------------------------------------------------------- 
// ColliderObjectBase.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

/// <summary>
/// �X�e�[�W�Ȃǂ̓����蔻��]�[�����쐬���邽�߂̒��ۃN���X
/// </summary>
public abstract class ColliderObjectBase : MonoBehaviour
{
    #region variable 
    protected HitZone _hitZone;
    protected HitZone.HitDistanceScale _hitDistanceScale = default;

    public float Y = 0f; //25f
    public float X = 0f;
    public float Z = 0f;
    public bool needVertexPoint = false;
    private Transform _myTransform;
    public bool _isDynamic = false;
    public struct AdjustmentPosint
    {
        /// <summary>
        /// Up Right Forward
        /// </summary>
        public float onePoint;
        /// <summary>
        /// Down Left Back
        /// </summary>
        public float twoPoint;
    }

    #endregion
    #region property
    #endregion
    #region method
    public GameObject c;

    private void Start()
    {
        HitScaleSizeSetting();

        //���_���������邩�ǂ���
        if (needVertexPoint)
        {
            //�����蔻��]�[���̒��_�ɃI�u�W�F�N�g���o���ē����蔻��]�[������������
            Vector3[] vecs = _hitZone.GetHitZoneVertexPositions();

            for (int i = 0; i < vecs.Length; i++)
            {
                Instantiate(c, vecs[i], Quaternion.identity);
            }
        }
        _myTransform = this.transform;

    }

    private void Update()
    {
        if (_isDynamic)
        {
            _hitZone.SetPosition(_myTransform.position);
        }
    }

    /// <summary>
    /// �����蔻��]�[�����쐬����
    /// </summary>
    protected abstract void HitScaleSizeSetting();

    /// <summary>
    /// �����蔻��]�[���Ƀq�b�g���Ă��邩
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public bool IsHit(Vector3 a)
    {
        if (_hitZone.IsHit(a))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// �܂��g���ĂȂ������蔻��
    /// </summary>
    /// <param name="mes"></param>
    /// <returns></returns>
    public bool IsHit2(Vector3[] mes)
    {
        if (_hitZone.IsHit2(mes))
        {
            return true;
        }
        return false;
    }

    public bool IsHit3(Vector3 point,Vector3 size)
    {
        if (_hitZone.IsHit3(point, size))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// �ђʂ▄�܂肱�݂�h�����߂̍��W���擾����
    /// </summary>
    /// <returns></returns>
    public virtual float PositionAdjustment()
    {
        return _hitZone.Y_Up();
    }

    public virtual float PositionAdjustment2()
    {
        return _hitZone.Y_Down();
    }

    public virtual AdjustmentPosint PositionAdjustmentPoint()
    {
        AdjustmentPosint adjustmentPosint = default;
        adjustmentPosint.onePoint = _hitZone.Y_Up();
        adjustmentPosint.twoPoint = _hitZone.Y_Down();
        return adjustmentPosint;
    }

    public HitZone.HitDistanceScale GetDistanceScale()
    {
        return _hitDistanceScale;
    }

    public float PushOutFromColliderX(Vector3 playerPosition)
    {
        if (_hitZone.X_Left() > playerPosition.x)
        {
            return _hitZone.X_Left();
        }

        if (_hitZone.X_Right() < playerPosition.x)
        {
            return _hitZone.X_Right();
        }
        return 0;
    }


    public float PushOutFromColliderY(Vector3 playerPosition)
    {
        if(_hitZone.Y_Up() < playerPosition.y)
        {
            return _hitZone.Y_Up();
        }

        if (_hitZone.Y_Down() > playerPosition.y)
        {
            return _hitZone.Y_Down();
        }
        return 0f;
    }

    public float PushOutFromColliderZ(Vector3 playerPosition)
    {
        if(_hitZone.Z_Back() > playerPosition.z)
        {
            return _hitZone.Z_Back();
        }

        if (_hitZone.Z_Forward() < playerPosition.z)
        {
            return _hitZone.Z_Forward();
        }
        return 0;
    }

    public int GetGameObjectLayer()
    {
        return this.gameObject.layer;
    }
    public string GetGameObjectLayerString()
    {
        return LayerMask.LayerToName(this.gameObject.layer);
    }

    #endregion
}