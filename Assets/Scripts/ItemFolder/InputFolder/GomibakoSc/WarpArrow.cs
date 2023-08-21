// --------------------------------------------------------- 
// WarpArrow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class WarpArrow : MonoBehaviour,IFCanTakeArrowButtonCantDestroy
{
    [SerializeField] Transform warpPosition = default;
    public void ButtonPush(Transform arrow)
    {
        ArrowWarp(arrow);
    }

    private void ArrowWarp(Transform arrow)
    {
        arrow.position = warpPosition.position;
    }
  
}