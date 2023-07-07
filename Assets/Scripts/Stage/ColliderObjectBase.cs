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
    #endregion
    #region property
    #endregion
    #region method
    public GameObject c;

    private void Start()
    {
        HitScaleSizeSetting();


        //�����蔻��]�[���̒��_�ɃI�u�W�F�N�g���o���ē����蔻��]�[������������
        Vector3[] vecs = _hitZone.GetHitZoneVertexPositions();

        for (int i = 0; i < vecs.Length; i++)
        {
            Instantiate(c, vecs[i], Quaternion.identity);
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
    /// �ђʂ▄�܂肱�݂�h�����߂̍��W���擾����
    /// </summary>
    /// <returns></returns>
    public virtual float PositionAdjustment()
    {
        return _hitZone.Y_Max();
    }
    #endregion
}