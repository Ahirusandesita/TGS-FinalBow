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
        //è∞Ç…êGÇÍÇƒÇ¢ÇÈÇ©
        //if (_containObject.IsContainObjectFloor(this.transform.position))
        //{
        //    //ñÑÇ‹ÇËÇ±Ç›ñhé~
        //    //this.transform.position = new Vector3(this.transform.position.x,_containObject.GetAdjustmentPosition_Floor(), this.transform.position.z);
        //    return;
        //}

        //ï«Ç…êGÇÍÇƒÇ¢ÇÈÇ©
        //if (_containObject.IsContainObjectWall(this.transform.position))
        //{
        //    //ñÑÇ‹ÇËÇ±Ç›ñhé~
        //    this.transform.position = new Vector3(_containObject.GetAdjustmentPosition_Wall(), this.transform.position.y, this.transform.position.z);
        //    return;
        //}

        transform.Translate(0f, -100f * Time.deltaTime, 0f);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(200f * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-200f * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, 200f * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, -200f * Time.deltaTime, 0f);
        }
        //transform.Translate(100f * Time.deltaTime, 0f, 0f);
    }
    #endregion
}