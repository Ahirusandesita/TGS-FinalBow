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
    public static List<Floor> floors = new List<Floor>();

    /// <summary>
    /// �S�ǃN���X�̃I�u�W�F�N�g
    /// </summary>
    public static List<Wall> walls = new List<Wall>();

    public static List<WallZ> wallZs = new List<WallZ>();

    /// <summary>
    /// �q�b�g���Ă��邩�𔻒肷�邽�߂̃f���Q�[�g
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    private delegate bool Contain(Vector3 me);

    /// <summary>
    /// ���Ƀq�b�g���Ă��邩
    /// </summary>
    private Contain Contain_Floor;

    /// <summary>
    /// �ǂɃq�b�g���Ă��邩
    /// </summary>
    private Contain Contain_Wall;

    private Contain Contain_WallZ;

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
    private Adjustment Adjustment_Floor = default;

    /// <summary>
    /// �ǂɖ��܂肱�܂Ȃ��悤�ɂ��邽�߂̕ϐ��擾
    /// </summary>
    private Adjustment Adjustment_Wall = default;


    private Adjustment Adjustment_WallZ = default;

    private delegate float AdjustmentAll(Vector3 me);

    private AdjustmentAll AdjustmentY;
    private AdjustmentAll AdjustmentX;
    private AdjustmentAll AdjustmentZ;

    private delegate HitZone.HitDistanceScale ColliderScale();

    private ColliderScale Scale;

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
        if (Contain_Floor != null)
        {
            //�O��G��Ă�����ԂɐG��Ă��邩
            //�G��Ă����炻��ȊO�̋�Ԃ����m����K�v���Ȃ��̂�True��Ԃ��@�I�u�W�F�N�g����r�����ɍς�
            if (Contain_Floor(me))
            {
                return true;
            }
        }

        //�G��Ă��Ȃ������炷�ׂẴI�u�W�F�N�g�̒�����G��Ă�����̂����邩��������
        for (int i = 0; i < floors.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (floors[i].IsHit(me))
            {
                Contain_Floor = new Contain(floors[i].IsHit);
                Adjustment_Floor = new Adjustment(floors[i].PositionAdjustmentPoint);

                Contains = new ContainAll(floors[i].IsHit2);
                AdjustmentY = new AdjustmentAll(floors[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(floors[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(floors[i].PushOutFromColliderZ);
                Scale = new ColliderScale(floors[i].GetDistanceScale);


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
        for (int i = 0; i < floors.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (floors[i].IsHit2(mes))
            {
                Contains = new ContainAll(floors[i].IsHit2);
                AdjustmentY = new AdjustmentAll(floors[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(floors[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(floors[i].PushOutFromColliderZ);
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// �ǂɐG��Ă��邩
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public bool IsContainObjectWall(Vector3 me)
    {
        //if (IsContain(ref Contain_Wall, me, walls, ref Adjustment_Wall))
        //{
        //    return true;
        //}
        //return false;

        if (Contain_Wall != null)
        {
            //�O��G��Ă�����ԂɐG��Ă��邩
            //�G��Ă����炻��ȊO�̋�Ԃ����m����K�v���Ȃ��̂�True��Ԃ��@�I�u�W�F�N�g����r�����ɍς�
            if (Contain_Wall(me))
            {
                return true;
            }
        }

        //�G��Ă��Ȃ������炷�ׂẴI�u�W�F�N�g�̒�����G��Ă�����̂����邩��������
        for (int i = 0; i < walls.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (walls[i].IsHit(me))
            {
                Contain_Wall = new Contain(walls[i].IsHit);
                Adjustment_Wall = new Adjustment(walls[i].PositionAdjustmentPoint);
                return true;
            }
        }
        return false;

    }

    public bool IsContainObjectWallZ(Vector3 me)
    {
        //if (IsContain(ref Contain_WallZ, me, wallZs, ref Adjustment_WallZ))
        //{
        //    return true;
        //}
        //return false;
        if (Contain_WallZ != null)
        {
            //�O��G��Ă�����ԂɐG��Ă��邩
            //�G��Ă����炻��ȊO�̋�Ԃ����m����K�v���Ȃ��̂�True��Ԃ��@�I�u�W�F�N�g����r�����ɍς�
            if (Contain_WallZ(me))
            {
                return true;
            }
        }

        //�G��Ă��Ȃ������炷�ׂẴI�u�W�F�N�g�̒�����G��Ă�����̂����邩��������
        for (int i = 0; i < wallZs.Count; i++)
        {
            //�G��Ă����Ԃ�����΂�����������r���邽�߂ɑ������@True��Ԃ�
            if (wallZs[i].IsHit(me))
            {
                Contain_WallZ = new Contain(wallZs[i].IsHit);
                Adjustment_WallZ = new Adjustment(wallZs[i].PositionAdjustmentPoint);
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
        return Adjustment_Floor();
    }

    /// <summary>
    /// ���܂肱�ݖh�~�p�̕ϐ����擾����
    /// </summary>
    /// <returns></returns>
    public ColliderObjectBase.AdjustmentPosint GetAdjustmentPosition_Wall()
    {
        return Adjustment_Wall();
    }

    public ColliderObjectBase.AdjustmentPosint GetAdjustmentPosition_WallZ()
    {
        return Adjustment_WallZ();
    }

    public float GetAdjustmentY(Vector3 me)
    {
        return AdjustmentY(me);
    }
    public float GetAdjustmentX(Vector3 me)
    {
        return AdjustmentX(me);
    }
    public float GetAdjustmentZ(Vector3 me)
    {
        return AdjustmentZ(me);
    }

    public HitZone.HitDistanceScale GetHitDistanceScale()
    {
        return Scale();
    }


    /// <summary>
    /// ���ʂɂ��ׂẴI�u�W�F�N�g���������Ĕ�r���Ȃ��悤�ɂ��邽�߂Ɏg���֐�
    /// </summary>
    /// <param name="contain"></param>
    /// <param name="me"></param>
    /// <param name="colliderObjectBases"></param>
    /// <returns></returns>
    private bool IsContain(ref Contain contain, Vector3 me, List<Floor> colliderObjectBases,ref Adjustment adjustment)
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
    private bool IsContain(ref Contain contain, Vector3 me, List<Wall> colliderObjectBases, ref Adjustment adjustment)
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

    private bool IsContain(ref Contain contain, Vector3 me, List<WallZ> colliderObjectBases, ref Adjustment adjustment)
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