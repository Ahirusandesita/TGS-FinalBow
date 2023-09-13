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

    //仮
    private static GameObject _target_Button;
    private static GameObject _target_TitleObject;

    private static int _layerMask = 1 << 6;
    private static int _layerMaskピーポー = 6;
    //仮
    private static int _layerMask_Button = 1 << 7;
    private static int _layerMask_Buttonピーポー = 7;

    private static int _layerMask_TitleObject = 1 << 8;
    private static int _layerMask_TitleObjectピーポー = 8;


    private static int _layerMask_BarrierObject = 1 << 9;
    private static int _layerMask_BarrierObjectピーポー = 9;


    private static int _layerMask_ButtonGimmick = 1 << 10;
    private static int _layerMask_ButtonGimmickピーポー = 10;
    private static int _layerMask_CantDestroyButton = 1 << 11;
    private static int _layerMask_CantDestroyButtonピーポー = 11;

    private static int _layerMask_BlockObjectピーポー = 13;
    private static int _layerMask_Barrier = 12;

    private static float ARROW_THICK = 0.4f;
    private static int ARROW_END_INDEX = 0;
    private static ContainObject _containObject = new ContainObject();

    //private bool[] arrowHits = { false, false, false, false, false, false };

    public static bool ArrowHit(Transform arrowTransform, Arrow arrow)
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
        if (Physics.CheckCapsule(arrowTransform.position, arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK, _layerMask))
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



    public static bool ArrowHit_TitleObject(Transform arrowTransform, Arrow arrow)
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

    public static bool ArrowHit_BarrierObject(HitZone hitZone, Arrow arrow)
    {
        for (int i = 0; i < hitZone.GetHitZone().Length; i++)
        {
            if (_containObject.IsContainObjectFloor(hitZone.GetHitZone()[i]) && _containObject.GetHitObjectLayerNumber() == _layerMask_BarrierObject)
            {
                return true;
            }
        }

        if (_containObject.IsContainObjectFloor2(hitZone.GetHitZone()) && _containObject.GetHitObjectLayerNumber() == _layerMask_BarrierObject)
        {
            return true;
        }
        return false;
    }

    public static bool ArrowHit_ButtonObject(Transform arrowTransform, Arrow arrow)
    {
        if (Physics.CheckCapsule(arrowTransform.position, arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK, _layerMask_ButtonGimmick))
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

    public static bool ArrowHit_CantDestroyObject(Transform arrowTransform, Arrow arrow)
    {
        if (Physics.CheckCapsule(arrowTransform.position, arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK, _layerMask_CantDestroyButton))
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

    public static bool[] ArrowHitFinalultraピーポー(Transform arrowTransform, Arrow arrow)
    {

        bool[] isArrowHits = new bool[7];
        for (int i = 0; i < isArrowHits.Length; i++)
        {
            isArrowHits[i] = false;
        }


        Collider[] co = Physics.OverlapCapsule(arrowTransform.position, arrowTransform.GetChild(ARROW_END_INDEX).position, ARROW_THICK);
        for (int i = 0; i < co.Length; i++)
        {

            if (!isArrowHits[0] && co[i].gameObject.layer == _layerMaskピーポー)
            {
                isArrowHits[0] = true;
                arrow._hitObjects[0] = co[i].gameObject;
            }

            else if (!isArrowHits[1] && co[i].gameObject.layer == _layerMask_Buttonピーポー)
            {
                isArrowHits[1] = true;
                arrow._hitObjects[1] = co[i].gameObject;
            }

            else if (!isArrowHits[2] && co[i].gameObject.layer == _layerMask_TitleObjectピーポー)
            {
                isArrowHits[2] = true;
                arrow._hitObjects[2] = co[i].gameObject;
            }

            else if (!isArrowHits[3] && co[i].gameObject.layer == _layerMask_BarrierObjectピーポー)
            {
                isArrowHits[3] = true;
                arrow._hitObjects[3] = co[i].gameObject;
            }

            else if (!isArrowHits[4] && co[i].gameObject.layer == _layerMask_ButtonGimmickピーポー)
            {
                isArrowHits[4] = true;
                arrow._hitObjects[4] = co[i].gameObject;
            }

            else if (!isArrowHits[5] && co[i].gameObject.layer == _layerMask_CantDestroyButtonピーポー)
            {
                isArrowHits[5] = true;
                arrow._hitObjects[5] = co[i].gameObject;
            }

            else if (!isArrowHits[6] && co[i].gameObject.layer == _layerMask_BlockObjectピーポー)
            {
                isArrowHits[6] = true;
                arrow._hitObjects[6] = co[i].gameObject;
            }

        }


        return isArrowHits;
    }

}
