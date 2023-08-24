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
    [SerializeField] bool changeAngle_Z = false;
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
            Vector3 setAxis = warpedRotation;
            if (!changeAngle_Z)
            {
                setAxis.z = arrow.rotation.eulerAngles.z;
            }
            arrow.rotation = Quaternion.Euler(setAxis);
            arrow.GetComponent<ArrowMove>().ReSetNormalSetting();
            print(arrow.rotation.eulerAngles);
        }
    }

}