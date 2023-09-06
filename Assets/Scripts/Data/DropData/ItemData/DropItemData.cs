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
    [Tooltip("傾き")]
    public int DropAngle;
    [Tooltip("向きの下限")]
    public int DropVectorMin;
    [Tooltip("向きの上限")]
    public int DropVectorMax;
    [Tooltip("下降速度の下限")]
    public int DropSpeedMin;
    [Tooltip("下降速度の上限")]
    public int DropSpeedMax;
    [Tooltip("移動速度の下限")]
    public int MoveSpeedMin;
    [Tooltip("移動速度の上限")]
    public int MoveSpeedMax;
}