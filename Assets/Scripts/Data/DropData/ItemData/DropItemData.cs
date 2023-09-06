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
    public DropItemStruct dropItemStruct;
}
[System.Serializable]
public class DropItemStruct
{
    [Tooltip("ŒX‚«")]
    public int DropAngle;
    [Tooltip("Œü‚«‚Ì‰ºŒÀ")]
    public int DropVectorMin;
    [Tooltip("Œü‚«‚ÌãŒÀ")]
    public int DropVectorMax;
    [Tooltip("‰º~‘¬“x‚Ì‰ºŒÀ")]
    public int DropSpeedMin;
    [Tooltip("‰º~‘¬“x‚ÌãŒÀ")]
    public int DropSpeedMax;
    [Tooltip("ˆÚ“®‘¬“x‚Ì‰ºŒÀ")]
    public int MoveSpeedMin;
    [Tooltip("ˆÚ“®‘¬“x‚ÌãŒÀ")]
    public int MoveSpeedMax;
}