// --------------------------------------------------------- 
// DropItem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System;

public static class DropFinalPositon
{
    public const float DROP_Y_FINALPOSITION = 0f;
}


public class DropItem : MonoBehaviour
{
    #region variable 
    private float Angle { get; set; }

    private Transform myTransform = default;

    [SerializeField]
    private float y_FinalPosition_Local = 0f;

    #endregion
    #region property
    #endregion
    #region method
    public void SetAngle(float angle) => Angle = angle;
    public void SetAngle(DropItemData dropItemData) => Angle = dropItemData.DropAngle;

    private void Start()
    {
        myTransform = this.transform;
        y_FinalPosition_Local = myTransform.position.y - y_FinalPosition_Local;
    }

    private void Update()
    {
        if (myTransform.position.y < DropFinalPositon.DROP_Y_FINALPOSITION) return;

        //if (myTransform.position.y < y_FinalPosition_Local) return;
    }



    #endregion
}