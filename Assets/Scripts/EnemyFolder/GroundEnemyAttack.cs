// --------------------------------------------------------- 
// GroundEnemyAttack.cs 
// 
// CreateDay: 2023/07/13
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
public class GroundEnemyAttack : EnemyAttack
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    /// <summary>
    /// �n��G��
    /// </summary>
    /// <param name="spawnObjectType">�Ăяo���I�u�W�F�N�g�̎��</param>
    /// <param name="spawnPlace">�X�|�[��������I�u�W�F�N�g��Transform���</param>
    public void Attack(PoolEnum.PoolObjectType spawnObjectType, Transform spawnPlace)
    {
        SpawnEAttackOne(spawnObjectType, spawnPlace);
    }
    #endregion
}