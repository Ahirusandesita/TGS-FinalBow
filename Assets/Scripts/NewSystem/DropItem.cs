// --------------------------------------------------------- 
// DropItem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class DropItem : MonoBehaviour
{
    #region variable 
    private float Angle { get; set; }
    #endregion
    #region property
    #endregion
    #region method
    public void SetAngle(float angle) => Angle = angle;
    public void SetAngle(DropItemData dropItemData) => Angle = dropItemData.DropAngle;
    #endregion
}