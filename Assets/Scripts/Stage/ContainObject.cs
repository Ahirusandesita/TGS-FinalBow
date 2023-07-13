// --------------------------------------------------------- 
// ContainObject.cs 
// 
// CreateDay: 2023/07/07
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g���q�b�g���Ă��邩���f����N���X
/// </summary>
public class ContainObject
{
    #region variable 
    /// <summary>
    /// �S�t���A�N���X�̃I�u�W�F�N�g
    /// </summary>
    public static List<OriginalCollider> originalColliders = new List<OriginalCollider>();

    /// <summary>
    /// �q�b�g���Ă��邩�𔻒肷�邽�߂̃f���Q�[�g
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    private delegate bool Contain(Vector3 me);

    /// <summary>
    /// ���Ƀq�b�g���Ă��邩
    /// </summary>
    private Contain Contain_Collider;

    private delegate bool ContainAll(Vector3[] mes);

    private ContainAll Contains;

    /// <summary>
    /// ���܂肱�ݖh�~�p�ϐ��擾�f���Q�[�g
    /// </summary>
    /// <returns></returns>
    private delegate ColliderObjectBase.AdjustmentPosint Adjustment();

    /// <summary>
    /// ���ɖ��܂肱�܂Ȃ��悤�ɂ��邽�߂̕ϐ��擾
    /// </summary>
    private Adjustment Adjustment_Collider = default;

    private delegate float AdjustmentAll(Vector3 me);

    private AdjustmentAll AdjustmentY;
    private AdjustmentAll AdjustmentX;
    private AdjustmentAll AdjustmentZ;

    private delegate HitZone.HitDistanceScale ColliderScale();

    private ColliderScale Scale;

    private delegate int ColliderObjectLayer();
    ColliderObjectLayer ColliderObjectLayerNumber;

    #endregion
    #region property
    #endregion
    #region method

    /// <summary>
    /// ���ɐG��Ă��邩
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public bool IsContainObjectFloor(Vector3 me)
    {

        //Contain�^�̃f���Q�[�g��Null�ł͂Ȃ��Ƃ�
        if (Contain_Collider != null)
        {
            //�O��G��Ă�����ԂɐG��Ă��邩
            //�G��Ă����炻��ȊO�̋�Ԃ����m����K�v���Ȃ��̂�True��Ԃ��@�I�u�W�F�N�g����r�����ɍς�
            if (Contain_Collider(me))
            {
                return true;
            }
        }

        //�G��Ă��Ȃ������炷�ׂẴI�u�W�F�N�g�̒�����G��Ă�����̂����邩��������
        for (int i = 0; i < originalColliders.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (originalColliders[i].IsHit(me))
            {
                Contain_Collider = new Contain(originalColliders[i].IsHit);
                Adjustment_Collider = new Adjustment(originalColliders[i].PositionAdjustmentPoint);

                Contains = new ContainAll(originalColliders[i].IsHit2);
                AdjustmentY = new AdjustmentAll(originalColliders[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(originalColliders[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(originalColliders[i].PushOutFromColliderZ);
                Scale = new ColliderScale(originalColliders[i].GetDistanceScale);
                ColliderObjectLayerNumber = new ColliderObjectLayer(originalColliders[i].GetGameObjectLayer);

                return true;
            }
        }
        return false;
    }
    public bool IsContainObjectTrigger(Vector3 me)
    {
        for (int i = 0; i < originalColliders.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (originalColliders[i].IsHit(me))
            {
                return true;
            }
        }
        return false;
    }


    public bool IsContainObjectFloor2(Vector3[] mes)
    {
        if(Contains != null)
        {
            if (Contains(mes))
            {
                return true;
            }
        }

        for (int i = 0; i < originalColliders.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (originalColliders[i].IsHit2(mes))
            {
                Contain_Collider = new Contain(originalColliders[i].IsHit);
                Adjustment_Collider = new Adjustment(originalColliders[i].PositionAdjustmentPoint);

                Contains = new ContainAll(originalColliders[i].IsHit2);
                AdjustmentY = new AdjustmentAll(originalColliders[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(originalColliders[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(originalColliders[i].PushOutFromColliderZ);
                Scale = new ColliderScale(originalColliders[i].GetDistanceScale);
                ColliderObjectLayerNumber = new ColliderObjectLayer(originalColliders[i].GetGameObjectLayer);


                return true;
            }
        }
        return false;

    }

    public bool IsContainObjectFloor3(Vector3 point,Vector3 size)
    {

        for (int i = 0; i < originalColliders.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (originalColliders[i].IsHit3(point,size))
            {
                Contain_Collider = new Contain(originalColliders[i].IsHit);
                Adjustment_Collider = new Adjustment(originalColliders[i].PositionAdjustmentPoint);

                Contains = new ContainAll(originalColliders[i].IsHit2);
                AdjustmentY = new AdjustmentAll(originalColliders[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(originalColliders[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(originalColliders[i].PushOutFromColliderZ);
                Scale = new ColliderScale(originalColliders[i].GetDistanceScale);
                ColliderObjectLayerNumber = new ColliderObjectLayer(originalColliders[i].GetGameObjectLayer);


                return true;
            }
        }
        return false;
    }


    public bool IsNowContainAll(Vector3 me)
    {
        if (Contain_Collider != null)
        {
            //�O��G��Ă�����ԂɐG��Ă��邩
            //�G��Ă����炻��ȊO�̋�Ԃ����m����K�v���Ȃ��̂�True��Ԃ��@�I�u�W�F�N�g����r�����ɍς�
            if (Contain_Collider(me))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsContainObjectAll(Vector3[] mes)
    {
        //Contain�^�̃f���Q�[�g��Null�ł͂Ȃ��Ƃ�
        if (Contains != null)
        {
            //�O��G��Ă�����ԂɐG��Ă��邩
            //�G��Ă����炻��ȊO�̋�Ԃ����m����K�v���Ȃ��̂�True��Ԃ��@�I�u�W�F�N�g����r�����ɍς�
            if (Contains(mes))
            {
                return true;
            }
        }

        //�G��Ă��Ȃ������炷�ׂẴI�u�W�F�N�g�̒�����G��Ă�����̂����邩��������
        for (int i = 0; i < originalColliders.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (originalColliders[i].IsHit2(mes))
            {
                Contains = new ContainAll(originalColliders[i].IsHit2);
                AdjustmentY = new AdjustmentAll(originalColliders[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(originalColliders[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(originalColliders[i].PushOutFromColliderZ);
                return true;
            }
        }
        return false;
    }




   

    /// <summary>
    /// ���܂肱�ݖh�~�p�̕ϐ����擾����
    /// </summary>
    /// <returns></returns>
    public ColliderObjectBase.AdjustmentPosint GetAdjustmentPosition_Floor()
    {
        return Adjustment_Collider();
    }


    /// <summary>
    /// Y���̃R���C�_�[�T�C�Y
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public float GetAdjustmentY(Vector3 me)
    {
        return AdjustmentY(me);
    }

    /// <summary>
    /// X���̃R���C�_�[�T�C�Y
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public float GetAdjustmentX(Vector3 me)
    {
        return AdjustmentX(me);
    }

    /// <summary>
    /// Z���̃R���C�_�[�T�C�Y
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public float GetAdjustmentZ(Vector3 me)
    {
        return AdjustmentZ(me);
    }


    /// <summary>
    /// �X�P�[��
    /// </summary>
    /// <returns></returns>
    public HitZone.HitDistanceScale GetHitDistanceScale()
    {
        return Scale();
    }


    public int GetHitObjectLayerNumber()
    {
        if (ColliderObjectLayerNumber != null)
        {
            int layerNumber = 1;
            for(int i = 0; i < ColliderObjectLayerNumber(); i++)
            {
                layerNumber = layerNumber << 1;
            }
            return layerNumber;
        }
        return 0;
    }

    /// <summary>
    /// ���ʂɂ��ׂẴI�u�W�F�N�g���������Ĕ�r���Ȃ��悤�ɂ��邽�߂Ɏg���֐�
    /// </summary>
    /// <param name="contain"></param>
    /// <param name="me"></param>
    /// <param name="colliderObjectBases"></param>
    /// <returns></returns>
    private bool IsContain(ref Contain contain, Vector3 me, List<OriginalCollider> colliderObjectBases,ref Adjustment adjustment)
    {
        //Contain�^�̃f���Q�[�g��Null�ł͂Ȃ��Ƃ�
        if (contain != null)
        {
            //�O��G��Ă�����ԂɐG��Ă��邩
            //�G��Ă����炻��ȊO�̋�Ԃ����m����K�v���Ȃ��̂�True��Ԃ��@�I�u�W�F�N�g����r�����ɍς�
            if (contain(me))
            {
                return true;
            }
        }

        //�G��Ă��Ȃ������炷�ׂẴI�u�W�F�N�g�̒�����G��Ă�����̂����邩��������
        for (int i = 0; i < colliderObjectBases.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (colliderObjectBases[i].IsHit(me))
            {
                contain = new Contain(colliderObjectBases[i].IsHit);
                adjustment = new Adjustment(colliderObjectBases[i].PositionAdjustmentPoint);
                return true;
            }
        }
        return false;
    }




    #endregion
}