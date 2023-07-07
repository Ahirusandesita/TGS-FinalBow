// --------------------------------------------------------- 
// SIne.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class SIne : MonoBehaviour
{
    #region variable 
    public Floor floor;

    private ContainObject _containObject;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {
        _containObject = new ContainObject();
    }

    private void Update()
    {
        //床に触れているか
        if (_containObject.IsContainObjectFloor(this.transform.position))
        {
            //埋まりこみ防止
            this.transform.position = new Vector3(this.transform.position.x,_containObject.GetAdjustmentPosition_Floor(), this.transform.position.z);
            return;
        }

        //壁に触れているか
        //if (_containObject.IsContainObjectWall(this.transform.position))
        //{
        //    //埋まりこみ防止
        //    this.transform.position = new Vector3(_containObject.GetAdjustmentPosition_Wall(), this.transform.position.y, this.transform.position.z);
        //    return;
        //}

        transform.Translate(0f, -100f * Time.deltaTime, 0f);
    }
    #endregion
}