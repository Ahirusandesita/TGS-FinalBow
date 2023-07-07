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

    private HitDistanceScale hitDistanceScale;

    private HitDistanceScaleAllPoint hitDistanceScaleAllPoint;

    /// <summary>
    /// 当たり判定の形
    /// </summary>
    public struct HitDistanceScale
    {
        public float _hitDistanceX;
        public float _hitDistanceY;
        public float _hitDistanceZ;
    }

    private HitDistanceScale[] hitDistanceScales = new HitDistanceScale[8];


    public struct HitDistanceScaleAllPoint
    {
        public HitDistanceScale hitDistanceScale1;
        public HitDistanceScale hitDistanceScale2;
        public HitDistanceScale hitDistanceScale3;
        public HitDistanceScale hitDistanceScale4;
        public HitDistanceScale hitDistanceScale5;
        public HitDistanceScale hitDistanceScale6;
        public HitDistanceScale hitDistanceScale7;
        public HitDistanceScale hitDistanceScale8;

    }


    #endregion
    #region property
    #endregion
    #region method
    /// <summary>
    /// 正方形の作成
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="hitPointCenter"></param>
    public HitZone(float distance, Vector3 hitPointCenter)
    {
        this.hitDistanceScale._hitDistanceX = distance;
        this.hitDistanceScale._hitDistanceY = distance;
        this.hitDistanceScale._hitDistanceZ = distance;

        CreateHitDistanceAllPoint(this.hitDistanceScale, hitPointCenter);
    }
    /// <summary>
    /// 長方形の作成
    /// </summary>
    /// <param name="hitDistanceScale"></param>
    /// <param name="hitPointCenter"></param>
    public HitZone(HitDistanceScale hitDistanceScale, Vector3 hitPointCenter)
    {
        this.hitDistanceScale = hitDistanceScale;
        CreateHitDistanceAllPoint(this.hitDistanceScale, hitPointCenter);
    }
    /// <summary>
    /// ８点で斜めの形を作成できる
    /// </summary>
    /// <param name="hitDistanceScale1"></param>
    /// <param name="hitDistanceScale2"></param>
    /// <param name="hitDistanceScale3"></param>
    /// <param name="hitDistanceScale4"></param>
    /// <param name="hitDistanceScale5"></param>
    /// <param name="hitDistanceScale6"></param>
    /// <param name="hitDistanceScale7"></param>
    /// <param name="hitDistanceScale8"></param>
    public HitZone(HitDistanceScale hitDistanceScale1, HitDistanceScale hitDistanceScale2, HitDistanceScale hitDistanceScale3, HitDistanceScale hitDistanceScale4, HitDistanceScale hitDistanceScale5, HitDistanceScale hitDistanceScale6, HitDistanceScale hitDistanceScale7, HitDistanceScale hitDistanceScale8)
    {
        this.hitDistanceScales[0] = hitDistanceScale1;
        this.hitDistanceScales[1] = hitDistanceScale2;
        this.hitDistanceScales[2] = hitDistanceScale3;
        this.hitDistanceScales[3] = hitDistanceScale4;
        this.hitDistanceScales[4] = hitDistanceScale5;
        this.hitDistanceScales[5] = hitDistanceScale6;
        this.hitDistanceScales[6] = hitDistanceScale7;
        this.hitDistanceScales[7] = hitDistanceScale8;
    }
    /// <summary>
    /// ８点で斜めの形を作成できる
    /// </summary>
    /// <param name="hitDistanceScales"></param>
    public HitZone(HitDistanceScale[] hitDistanceScales)
    {
        if (this.hitDistanceScales.Length == hitDistanceScales.Length)
        {

            for (int i = 0; i < hitDistanceScales.Length; i++)
            {
                this.hitDistanceScales[i] = hitDistanceScales[i];
            }
        }
        else
        {
            //エラー
        }

    }

    /// <summary>
    /// これは使用できない
    /// </summary>
    /// <param name="hitDistanceScaleAllPoint"></param>
    /// <param name="hitPointCenter"></param>
    public HitZone(HitDistanceScaleAllPoint hitDistanceScaleAllPoint, Vector3 hitPointCenter)
    {
        this.hitDistanceScaleAllPoint = hitDistanceScaleAllPoint;
        CreateHitZone(hitPointCenter);
    }



    private void CreateHitDistanceAllPoint(HitDistanceScale hitDistanceScale, Vector3 hitPointCeneter)
    {
        //未使用
        this.hitDistanceScaleAllPoint.hitDistanceScale1._hitDistanceX = -this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScaleAllPoint.hitDistanceScale1._hitDistanceY = -this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScaleAllPoint.hitDistanceScale1._hitDistanceZ = -this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScaleAllPoint.hitDistanceScale2._hitDistanceX = -this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScaleAllPoint.hitDistanceScale2._hitDistanceY = -this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScaleAllPoint.hitDistanceScale2._hitDistanceZ = this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScaleAllPoint.hitDistanceScale3._hitDistanceX = this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScaleAllPoint.hitDistanceScale3._hitDistanceY = -this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScaleAllPoint.hitDistanceScale3._hitDistanceZ = this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScaleAllPoint.hitDistanceScale4._hitDistanceX = this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScaleAllPoint.hitDistanceScale4._hitDistanceY = -this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScaleAllPoint.hitDistanceScale4._hitDistanceZ = -this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScaleAllPoint.hitDistanceScale5._hitDistanceX = -this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScaleAllPoint.hitDistanceScale5._hitDistanceY = this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScaleAllPoint.hitDistanceScale5._hitDistanceZ = -this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScaleAllPoint.hitDistanceScale6._hitDistanceX = -this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScaleAllPoint.hitDistanceScale6._hitDistanceY = this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScaleAllPoint.hitDistanceScale6._hitDistanceZ = this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScaleAllPoint.hitDistanceScale7._hitDistanceX = this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScaleAllPoint.hitDistanceScale7._hitDistanceY = this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScaleAllPoint.hitDistanceScale7._hitDistanceZ = this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScaleAllPoint.hitDistanceScale8._hitDistanceX = this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScaleAllPoint.hitDistanceScale8._hitDistanceY = this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScaleAllPoint.hitDistanceScale8._hitDistanceZ = -this.hitDistanceScale._hitDistanceZ;


            //for (int x = 0; x < this.hitDistanceScales.Length / 2; x++)
            //{
            //    this.hitDistanceScales[x]._hitDistanceX = this.hitDistanceScale._hitDistanceX;
            //}

            //for (int y = 0; y < this.hitDistanceScales.Length / 2; y++)
            //{
            //    this.hitDistanceScales[y]._hitDistanceY = this.hitDistanceScale._hitDistanceY;
            //}

            //for (int z = 0; z < this.hitDistanceScales.Length / 2; z++)
            //{
            //    this.hitDistanceScales[z]._hitDistanceZ = this.hitDistanceScale._hitDistanceZ;
            //}


        this.hitDistanceScales[0]._hitDistanceX = -this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScales[0]._hitDistanceY = -this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScales[0]._hitDistanceZ = -this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScales[1]._hitDistanceX = -this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScales[1]._hitDistanceY = -this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScales[1]._hitDistanceZ = this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScales[2]._hitDistanceX = this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScales[2]._hitDistanceY = -this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScales[2]._hitDistanceZ = this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScales[3]._hitDistanceX = this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScales[3]._hitDistanceY = -this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScales[3]._hitDistanceZ = -this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScales[4]._hitDistanceX = -this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScales[4]._hitDistanceY = this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScales[4]._hitDistanceZ = -this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScales[5]._hitDistanceX = -this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScales[5]._hitDistanceY = this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScales[5]._hitDistanceZ = this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScales[6]._hitDistanceX = this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScales[6]._hitDistanceY = this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScales[6]._hitDistanceZ = this.hitDistanceScale._hitDistanceZ;

        this.hitDistanceScales[7]._hitDistanceX = this.hitDistanceScale._hitDistanceX;
        this.hitDistanceScales[7]._hitDistanceY = this.hitDistanceScale._hitDistanceY;
        this.hitDistanceScales[7]._hitDistanceZ = -this.hitDistanceScale._hitDistanceZ;

        CreateHitZone(hitPointCeneter);
    }


    /// <summary>
    /// 攻撃がヒット判定ゾーンにヒットしているか判断する
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

            //立方体作成
            SettingX(i, this.hitDistanceScales[i]._hitDistanceX);
            SettingY(i, this.hitDistanceScales[i]._hitDistanceY);          
            SettingZ(i, this.hitDistanceScales[i]._hitDistanceZ);



        //    switch (i)
        //    {
        //        case 0:                                 //立方体　＊の部分を計算する
        //            SettingX(i, hitDistanceScaleAllPoint.hitDistanceScale1._hitDistanceX);          //  .`  .`
        //            SettingY(i, hitDistanceScaleAllPoint.hitDistanceScale1._hitDistanceY);          //  *`  .`  
        //            SettingZ(i, hitDistanceScaleAllPoint.hitDistanceScale1._hitDistanceZ);
        //            break;
        //        case 1:
        //            SettingX(i, hitDistanceScaleAllPoint.hitDistanceScale2._hitDistanceX);          //  *`  .`
        //            SettingY(i, hitDistanceScaleAllPoint.hitDistanceScale2._hitDistanceY);          //  .`  .`
        //            SettingZ(i, hitDistanceScaleAllPoint.hitDistanceScale2._hitDistanceZ);
        //            break;
        //        case 2:
        //            SettingX(i, hitDistanceScaleAllPoint.hitDistanceScale3._hitDistanceX);           //  .`  *`
        //            SettingY(i, hitDistanceScaleAllPoint.hitDistanceScale4._hitDistanceY);          //  .`  .`
        //            SettingZ(i, hitDistanceScale._hitDistanceZ);
        //            break;
        //        case 3:
        //            SettingX(i, hitDistanceScale._hitDistanceX);           //  .`  .`
        //            SettingY(i, -hitDistanceScale._hitDistanceY);          //  .`  *`
        //            SettingZ(i, -hitDistanceScale._hitDistanceZ);
        //            break;
        //        case 4:
        //            SettingX(i, -hitDistanceScale._hitDistanceX);          //  .`  .`
        //            SettingY(i, hitDistanceScale._hitDistanceY);           //  .*  .`
        //            SettingZ(i, -hitDistanceScale._hitDistanceZ);
        //            break;
        //        case 5:
        //            SettingX(i, -hitDistanceScale._hitDistanceX);          //  .*  .`
        //            SettingY(i, hitDistanceScale._hitDistanceY);           //  .`  .`
        //            SettingZ(i, hitDistanceScale._hitDistanceZ);
        //            break;
        //        case 6:
        //            SettingX(i, hitDistanceScale._hitDistanceX);           //  .`  .*
        //            SettingY(i, hitDistanceScale._hitDistanceY);           //  .`  .`
        //            SettingZ(i, hitDistanceScale._hitDistanceZ);
        //            break;
        //        case 7:
        //            SettingX(i, hitDistanceScale._hitDistanceX);           //  .`  .`
        //            SettingY(i, hitDistanceScale._hitDistanceY);           //  .`  .*
        //            SettingZ(i, -hitDistanceScale._hitDistanceZ);
        //            break;
        //    }
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