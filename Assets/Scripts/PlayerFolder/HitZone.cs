// --------------------------------------------------------- 
// HitZone.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class HitZone
{
    #region variable

    private Vector3[] _hitZone = new Vector3[8];

    private HitDistanceScale hitDstanceScale;

    /// <summary>
    /// ìñÇΩÇËîªíËÇÃå`
    /// </summary>
    public struct HitDistanceScale
    {
        public float _hitDistanceX;
        public float _hitDistanceY;
        public float _hitDistanceZ;
    }


    #endregion
    #region property
    #endregion
    #region method
    public HitZone(float distance,Vector3 hitPointCenter) 
    {
        this.hitDstanceScale._hitDistanceX = distance;
        this.hitDstanceScale._hitDistanceY = distance;
        this.hitDstanceScale._hitDistanceZ = distance;

        CreateHitZone(hitPointCenter);
    }

    public HitZone(HitDistanceScale hitDistanceScale,Vector3 hitPointCenter)
    {
        this.hitDstanceScale = hitDistanceScale;
        CreateHitZone(hitPointCenter);
    }


    /// <summary>
    /// çUåÇÇ™ÉqÉbÉgîªíËÉ]Å[ÉìÇ…ÉqÉbÉgÇµÇƒÇ¢ÇÈÇ©îªífÇ∑ÇÈ
    /// </summary>
    /// <param name="attackPosition"></param>
    /// <returns></returns>
    public bool IsHit(Vector3 attackPosition)
    {
        if (
            CheckAttackPositionMinus(_hitZone[0].x, attackPosition.x) ||
            CheckAttackPositionMinus(_hitZone[0].y, attackPosition.y) ||
            CheckAttackPositionMinus(_hitZone[0].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionMinus(_hitZone[1].x, attackPosition.x) ||
            CheckAttackPositionMinus(_hitZone[1].y, attackPosition.y) ||
            CheckAttackPositionPlus(_hitZone[1].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionPlus(_hitZone[2].x, attackPosition.x) ||
            CheckAttackPositionMinus(_hitZone[2].y, attackPosition.y) ||
            CheckAttackPositionPlus(_hitZone[2].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionPlus(_hitZone[3].x, attackPosition.x) ||
            CheckAttackPositionMinus(_hitZone[3].y, attackPosition.y) ||
            CheckAttackPositionMinus(_hitZone[3].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionMinus(_hitZone[4].x, attackPosition.x) ||
            CheckAttackPositionPlus(_hitZone[4].y, attackPosition.y) ||
            CheckAttackPositionMinus(_hitZone[4].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionMinus(_hitZone[5].x, attackPosition.x) ||
            CheckAttackPositionPlus(_hitZone[5].y, attackPosition.y) ||
            CheckAttackPositionPlus(_hitZone[5].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionPlus(_hitZone[6].x, attackPosition.x) ||
            CheckAttackPositionPlus(_hitZone[6].y, attackPosition.y) ||
            CheckAttackPositionPlus(_hitZone[6].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionPlus(_hitZone[7].x, attackPosition.x) ||
            CheckAttackPositionPlus(_hitZone[7].y, attackPosition.y) ||
            CheckAttackPositionMinus(_hitZone[7].z, attackPosition.z)
            )
        {
            return false;
        }
        return true;
    }



    private void CreateHitZone(Vector3 hitPointCenter)
    {
        for (int i = 0; i < _hitZone.Length; i++)
        {
            _hitZone[i] = hitPointCenter;
        }


        for (int i = 0; i < _hitZone.Length; i++)
        {
            switch (i)
            {
                case 0:                                 //óßï˚ëÃÅ@ÅñÇÃïîï™ÇåvéZÇ∑ÇÈ
                    SettingX(i, -hitDstanceScale._hitDistanceX);          //  .`  .`
                    SettingY(i, -hitDstanceScale._hitDistanceY);          //  *`  .`  
                    SettingZ(i, -hitDstanceScale._hitDistanceZ);
                    break;
                case 1:
                    SettingX(i, -hitDstanceScale._hitDistanceX);          //  *`  .`
                    SettingY(i, -hitDstanceScale._hitDistanceY);          //  .`  .`
                    SettingZ(i, hitDstanceScale._hitDistanceZ);
                    break;
                case 2:
                    SettingX(i, hitDstanceScale._hitDistanceX);           //  .`  *`
                    SettingY(i, -hitDstanceScale._hitDistanceY);          //  .`  .`
                    SettingZ(i, hitDstanceScale._hitDistanceZ);
                    break;
                case 3:
                    SettingX(i, hitDstanceScale._hitDistanceX);           //  .`  .`
                    SettingY(i, -hitDstanceScale._hitDistanceY);          //  .`  *`
                    SettingZ(i, -hitDstanceScale._hitDistanceZ);
                    break;
                case 4:
                    SettingX(i, -hitDstanceScale._hitDistanceX);          //  .`  .`
                    SettingY(i, hitDstanceScale._hitDistanceY);           //  .*  .`
                    SettingZ(i, -hitDstanceScale._hitDistanceZ);
                    break;
                case 5:
                    SettingX(i, -hitDstanceScale._hitDistanceX);          //  .*  .`
                    SettingY(i, hitDstanceScale._hitDistanceY);           //  .`  .`
                    SettingZ(i, hitDstanceScale._hitDistanceZ);
                    break;
                case 6:
                    SettingX(i, hitDstanceScale._hitDistanceX);           //  .`  .*
                    SettingY(i, hitDstanceScale._hitDistanceY);           //  .`  .`
                    SettingZ(i, hitDstanceScale._hitDistanceZ);
                    break;
                case 7:
                    SettingX(i, hitDstanceScale._hitDistanceX);           //  .`  .`
                    SettingY(i, hitDstanceScale._hitDistanceY);           //  .`  .*
                    SettingZ(i, -hitDstanceScale._hitDistanceZ);
                    break;
            }
        }
    }

    private void SettingX(int index, float distance)
    {
        _hitZone[index].x = _hitZone[index].x + distance;
    }
    private void SettingY(int index, float distance)
    {
        _hitZone[index].y = _hitZone[index].y + distance;
    }
    private void SettingZ(int index, float distance)
    {
        _hitZone[index].z = _hitZone[index].z + distance;
    }




    private bool CheckAttackPositionMinus(float position, float attackPosition)
    {
        if (position > attackPosition)
        {
            return true;
        }
        return false;
    }
    private bool CheckAttackPositionPlus(float position, float attackPosition)
    {
        if (position < attackPosition)
        {
            return true;
        }
        return false;
    }

    #endregion
}