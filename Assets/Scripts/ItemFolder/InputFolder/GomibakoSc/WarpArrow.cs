// --------------------------------------------------------- 
// WarpArrow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class WarpArrow : MonoBehaviour, IFCanTakeArrowButtonCantDestroy
{
    [SerializeField] Transform warpPosition = default;
    [SerializeField] Vector3 warpedRotation = default;
    [SerializeField] bool changeAngle_Y = false;
    [SerializeField] bool changeAngle = true;
    public void ButtonPush(Transform arrow)
    {
        ArrowWarp(arrow);
    }

    private void ArrowWarp(Transform arrow)
    {
        arrow.position = warpPosition.position;
        if (changeAngle)
        {

            if (changeAngle_Y)
            {
                arrow.rotation = Quaternion.Euler(warpedRotation.x, arrow.rotation.eulerAngles.y, warpedRotation.z); ;
                return;
            }
            arrow.rotation = Quaternion.Euler(warpedRotation);
            print(arrow.rotation);
        }
    }

}