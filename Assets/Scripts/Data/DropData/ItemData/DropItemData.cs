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
    [Tooltip("ŒX‚«")]
    public int DropAngle;
    [Tooltip("Œü‚«‚Ì‰ºŒÀ")]
    public int DropVectorMin;
    [Tooltip("Œü‚«‚ÌãŒÀ")]
    public int DropVectorMax;
}