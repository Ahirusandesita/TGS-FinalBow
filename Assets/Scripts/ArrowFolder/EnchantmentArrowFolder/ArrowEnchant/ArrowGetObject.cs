// --------------------------------------------------------- 
// ArrowGetObject.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;
public static class ArrowGetObject
{
    private static GameObject target;

    //‰¼
    private static GameObject _target_Button;
    private static GameObject _target_TitleObject;

    private static int _layerMask = 1 << 6;

    //‰¼
    private static int _layerMask_Button = 1 << 7;
    private static int _layerMask_TitleObject = 1 << 8;

    private static int _layerMask_BarrierObject = 1 << 9;

    private static float ARROW_THICK = 0.4f;
    private static int ARROW_END_INDEX = 0;
    private static ContainObject _containObject = new ContainObject();

    public static bool ArrowHit(Transform arrowTransform,Arrow arrow)
    {
        /*
        Ray ray = new Ray(arrowTransform.position, arrowTransform.forward * 0.01f);
        RaycastHit hitObject;
        if(Physics.Raycast(ray,out hitObject))
        {
            target = hitObject.transform.gameObject;
            return true;
        }
        */
        if (Physics.CheckCapsule(arrowTransform.position,arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK,_layerMask)) 
        {
            Collider[] co = Physics.OverlapCapsule(arrowTransform.position,arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK);
            if (co != null)
            {
                arrow._hitObject = co[0].gameObject;
                return true;
            }
        }
       
        return false;
    }


    public static bool ArrowHit_Object(Transform arrowTransform, Arrow arrow)
    {
        if (Physics.CheckCapsule(arrowTransform.position, arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK, _layerMask_Button))
        {
            Collider[] co = Physics.OverlapCapsule(arrowTransform.position, arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK);
            if (co != null)
            {
                arrow._hitObject = co[0].gameObject;
                return true;
            }
        }

        return false;
    }



    public static bool ArrowHit_TitleObject(Transform arrowTransform,Arrow arrow)
    {
        if (Physics.CheckCapsule(arrowTransform.position, arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK, _layerMask_TitleObject))
        {
            Collider[] co = Physics.OverlapCapsule(arrowTransform.position, arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK);
            if (co != null)
            {
                arrow._hitObject = co[0].gameObject;
                return true;
            }
        }

        return false;
    }

    public static bool ArrowHit_BarrierObject(Transform arrowTransfrom,Arrow arrow)
    {
        if (_containObject.IsContainObjectFloor(arrowTransfrom.position) && _containObject.GetHitObjectLayerNumber() == _layerMask_BarrierObject)
        { 
            return true;
        }
        return false;
    }


}
