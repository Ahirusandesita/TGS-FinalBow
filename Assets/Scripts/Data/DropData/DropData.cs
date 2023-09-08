// --------------------------------------------------------- 
// DropData.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "DropData", menuName = "Scriptables/CreateDropTable")]
public class DropData : ScriptableObject
{
    public DropItem DropItem;
    public int DropValue;
    public PoolEnum.PoolObjectType PoolObjectType;
}
