// --------------------------------------------------------- 
// DropItemData.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "DropItemData", menuName = "Scriptables/CreateDropItemTable")]
public class DropItemData : ScriptableObject
{
    [Tooltip("�X��")]
    public int DropAngle;
    [Tooltip("�����̉���")]
    public int DropVectorMin;
    [Tooltip("�����̏��")]
    public int DropVectorMax;
}