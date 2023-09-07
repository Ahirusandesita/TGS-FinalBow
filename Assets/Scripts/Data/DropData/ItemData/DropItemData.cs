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
    public enum ItemType { Fall = 0, Fly = 1 }
    [Tooltip("アイテムの挙動の種類")]
    public ItemType itemType;
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
    [Tooltip("速度の減衰速度（Flyのみ）")]
    public float DownSpeedValue;
}