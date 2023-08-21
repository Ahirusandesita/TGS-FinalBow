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
    [Tooltip("傾き")]
    public int DropAngle;
    [Tooltip("向きの下限")]
    public int DropVectorMin;
    [Tooltip("向きの上限")]
    public int DropVectorMax;
}