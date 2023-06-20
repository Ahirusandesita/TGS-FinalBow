// --------------------------------------------------------- 
// PlayerDamageZone.cs 
// 
// CreateDay: 2023/06/18
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class PlayerHitZone : MonoBehaviour
{
    #region variable 
    //プレイヤーの座標
    public Transform _playerTransform = default;

    //立方体の座標　この中にいるとダメージを受ける
    private Vector3[] _playerHitZone = new Vector3[8];

    public float _hitDistance;

    private PlayerStats _playerStats;

    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("CanvasController").Length == 0)
        {
            enabled = false;
        }
        _playerStats = this.GetComponent<PlayerStats>();
        for (int i = 0; i < _playerHitZone.Length; i++)
        {
            _playerHitZone[i] = _playerTransform.position;
        }

        for(int i = 0; i < _playerHitZone.Length; i++)
        {
            switch (i)
            {
                case 0:                                 //立方体　＊の部分を計算する
                    SettingX(i, -_hitDistance);          //  .`  .`
                    SettingY(i, -_hitDistance);          //  *`  .`  
                    SettingZ(i, -_hitDistance);
                    break;
                case 1:
                    SettingX(i, -_hitDistance);          //  *`  .`
                    SettingY(i, -_hitDistance);          //  .`  .`
                    SettingZ(i, _hitDistance);
                    break;
                case 2:
                    SettingX(i, _hitDistance);           //  .`  *`
                    SettingY(i, -_hitDistance);          //  .`  .`
                    SettingZ(i, _hitDistance);
                    break;
                case 3:
                    SettingX(i, _hitDistance);           //  .`  .`
                    SettingY(i, -_hitDistance);          //  .`  *`
                    SettingZ(i, -_hitDistance);
                    break;
                case 4:
                    SettingX(i, -_hitDistance);          //  .`  .`
                    SettingY(i, _hitDistance);           //  .*  .`
                    SettingZ(i, -_hitDistance);
                    break;
                case 5:
                    SettingX(i, -_hitDistance);          //  .*  .`
                    SettingY(i, _hitDistance);           //  .`  .`
                    SettingZ(i, _hitDistance);
                    break;
                case 6:
                    SettingX(i, _hitDistance);           //  .`  .*
                    SettingY(i, _hitDistance);           //  .`  .`
                    SettingZ(i, _hitDistance);
                    break;
                case 7:
                    SettingX(i, _hitDistance);           //  .`  .`
                    SettingY(i, _hitDistance);           //  .`  .*
                    SettingZ(i, -_hitDistance);
                    break;
            }
        }


    }
    public void HitZone(Vector3 attackPosition)
    {
        if (IsHit(attackPosition))
        {
            _playerStats.PlayerDamage(1);
        }
    }

    /// <summary>
    /// 攻撃がヒット判定ゾーンにヒットしているか判断する
    /// </summary>
    /// <param name="attackPosition"></param>
    /// <returns></returns>
    public bool IsHit(Vector3 attackPosition)
    {
        if (
            CheckAttackPositionMinus(_playerHitZone[0].x, attackPosition.x) ||
            CheckAttackPositionMinus(_playerHitZone[0].y, attackPosition.y) ||
            CheckAttackPositionMinus(_playerHitZone[0].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionMinus(_playerHitZone[1].x, attackPosition.x) ||
            CheckAttackPositionMinus(_playerHitZone[1].y, attackPosition.y) ||
            CheckAttackPositionPlus(_playerHitZone[1].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionPlus(_playerHitZone[2].x, attackPosition.x) ||
            CheckAttackPositionMinus(_playerHitZone[2].y, attackPosition.y) ||
            CheckAttackPositionPlus(_playerHitZone[2].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionPlus(_playerHitZone[3].x, attackPosition.x) ||
            CheckAttackPositionMinus(_playerHitZone[3].y, attackPosition.y) ||
            CheckAttackPositionMinus(_playerHitZone[3].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionMinus(_playerHitZone[4].x, attackPosition.x) ||
            CheckAttackPositionPlus(_playerHitZone[4].y, attackPosition.y) ||
            CheckAttackPositionMinus(_playerHitZone[4].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionMinus(_playerHitZone[5].x, attackPosition.x) ||
            CheckAttackPositionPlus(_playerHitZone[5].y, attackPosition.y) ||
            CheckAttackPositionPlus(_playerHitZone[5].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionPlus(_playerHitZone[6].x, attackPosition.x) ||
            CheckAttackPositionPlus(_playerHitZone[6].y, attackPosition.y) ||
            CheckAttackPositionPlus(_playerHitZone[6].z, attackPosition.z)
            )
        {
            return false;
        }
        if (
            CheckAttackPositionPlus(_playerHitZone[7].x, attackPosition.x) ||
            CheckAttackPositionPlus(_playerHitZone[7].y, attackPosition.y) ||
            CheckAttackPositionMinus(_playerHitZone[7].z, attackPosition.z)
            )
        {
            return false;
        }
        return true;
    }





    private void SettingX(int index,float distance)
    {
        _playerHitZone[index].x = _playerHitZone[index].x + distance;
    }
    private void SettingY(int index,float distance)
    {
        _playerHitZone[index].y = _playerHitZone[index].y + distance;
    }
    private void SettingZ(int index,float distance)
    {
        _playerHitZone[index].z = _playerHitZone[index].z + distance;
    }
    



    private bool CheckAttackPositionMinus(float position,float attackPosition)
    {
        if (position > attackPosition)
        {
            return true;
        }
        return false;
    }
    private bool CheckAttackPositionPlus(float position,float attackPosition)
    {
        if (position < attackPosition)
        {
            return true;
        }
        return false;
    }



    #endregion
}