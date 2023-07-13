// --------------------------------------------------------- 
// GroundEnemyDataTable.cs 
// 
// CreateDay: 2023/07/13
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections.Generic;

// Assets > Create > Scriptables > CreateGroundEnemyDataTable�ŃA�Z�b�g��
[CreateAssetMenu(fileName = "GroundEnemyData", menuName = "Scriptables/CreateGroundEnemyDataTable")]
[System.Serializable]
public class GroundEnemyDataTable : EnemyDataTable
{
    [Header("�n��G���̃X�|�[���ʒu"), Tooltip("�n��G���̃X�|�[���ʒu")]
    public Transform _groundEnemySpawnPlace;
}