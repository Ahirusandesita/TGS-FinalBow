// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class InputManagement : MonoBehaviour
{
    #region パブリック変数
    /// <summary>
    /// カメラについているトラッキングスペース
    /// </summary>
    /// 変更した所
    private Transform trackingSpace;

    public TagObject _TrackingSpaceTagData;

    public GlobalHandStats stats;

    public enum EmptyHand
    {
        Left,
        Right
    }
    /// <summary>
    /// 弓を引く手
    /// </summary>
    [SerializeField] EmptyHand emptyHand = EmptyHand.Left;
    #endregion

    #region 変数
    /// <summary>
    /// 前回の左コントローラーポジション
    /// </summary>
    Vector3 beforeLeftHandPos = Vector3.zero;
    /// <summary>
    /// 前回の右コントローラーポジション
    /// </summary>
    Vector3 beforeRightHandPos = Vector3.zero;
    /// <summary>
    /// 今回の左コントローラーポジション
    /// </summary>
    Vector3 afterLeftHandPos = Vector3.zero;
    /// <summary>
    /// 今回の右コントローラーポジション
    /// </summary>
    Vector3 afterRightHandPos = Vector3.zero;
    /// <summary>
    /// 前回の手の位置計測した時間
    /// </summary>
    float beforeHandTime = 0f;
    /// <summary>
    /// 今回の手の位置計測した時間
    /// </summary>
    float afterHandTime = 0f;

    #endregion

    public EmptyHand P_EmptyHand
    {
        set
        {
            emptyHand = value;
            stats.SaveHands = emptyHand;
        }
        get
        {
            return emptyHand;
        }
    }

    private void Awake()
    {
        X_Debug.Log("aaa" + stats.SaveHands);
        emptyHand = stats.SaveHands;
    }
    private void Start()
    {
        //変更した所
        trackingSpace = GameObject.FindGameObjectWithTag(_TrackingSpaceTagData.TagName).transform;
        
        SetBeforeHandPosition();
    }

    private void Update()
    {
        OVRInput.Update();        
    }

    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();

        SetAfterHandPosition();

        SetBeforeHandPosition(afterLeftHandPos,afterRightHandPos,afterHandTime);     
    }

    #region 左ボタンGet

    /// <summary>
    /// 左のXボタンを押しているか
    /// </summary>
    /// <returns>押していたらtrue</returns>
    public bool ButtonX()
    {

        return OVRInput.Get(OVRInput.Button.Three);
    }

    /// <summary>
    /// 左のYボタンを押しているか
    /// </summary>
    /// <returns>押していたらtrue</returns>
    public bool ButtonY()
    {
        return OVRInput.Get(OVRInput.Button.Four);
    }

    public bool ButtonLeftDownTrigger()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryHandTrigger);
    }

    public bool ButtonLeftUpTrigger()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
    }

    public bool ButtonLeftStickPush()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryThumbstick);
    }

    public bool ButtonLeftStickDown()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown);
    }

    public bool ButtonLeftStickUp()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp);
    }

    public bool ButtonLeftStickLeft()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft);
    }

    public bool ButtonLeftStickRight()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight);
    }

    public bool ButtonStart()
    {
        return OVRInput.Get(OVRInput.Button.Start);
    }

    public bool ButtonLeftShoulder()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryShoulder);
    }

    #endregion

    #region 左ボタンGetUp

    /// <summary>
    /// 左のAボタンを離したか
    /// </summary>
    /// <returns>離したらtrue</returns>
    public bool ButtonUpX()
    {

        return OVRInput.GetUp(OVRInput.Button.Three);
    }

    /// <summary>
    /// 左のBボタンを離したか
    /// </summary>
    /// <returns>離したらtrue</returns>
    public bool ButtonUpY()
    {
        return OVRInput.GetUp(OVRInput.Button.Four);
    }

    public bool ButtonUpLeftDownTrigger()
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger);
    }

    public bool ButtonUpLeftUpTrigger()
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger);
    }

    public bool ButtonUpLeftStickPush()
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick);
    }

    public bool ButtonUpLeftStickDown()
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickDown);
    }

    public bool ButtonUpLeftStickUp()
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickUp);
    }

    public bool ButtonUpLeftStickLeft()
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickLeft);
    }

    public bool ButtonUpLeftStickRight()
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickRight);
    }

    public bool ButtonUpStart()
    {
        return OVRInput.GetUp(OVRInput.Button.Start);
    }

    public bool ButtonUpLeftShoulder()
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryShoulder);
    }

    #endregion

    #region 左ボタンGetDown

    /// <summary>
    /// 左のXボタンを押しているか
    /// </summary>
    /// <returns>押していたらtrue</returns>
    public bool ButtonDownX()
    {

        return OVRInput.GetDown(OVRInput.Button.Three);
    }

    /// <summary>
    /// 左のYボタンを押しているか
    /// </summary>
    /// <returns>押していたらtrue</returns>
    public bool ButtonDownY()
    {
        return OVRInput.GetDown(OVRInput.Button.Four);
    }

    public bool ButtonDownLeftDownTrigger()
    {
        return OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger);
    }

    public bool ButtonDownLeftUpTrigger()
    {
        return OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
    }

    public bool ButtonDownLeftStickPush()
    {
        return OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick);
    }

    public bool ButtonDownLeftStickDown()
    {
        return OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown);
    }

    public bool ButtonDownLeftStickUp()
    {
        return OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp);
    }

    public bool ButtonDownLeftStickLeft()
    {
        return OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft);
    }

    public bool ButtonDownLeftStickRight()
    {
        return OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight);
    }

    public bool ButtonDownStart()
    {
        return OVRInput.GetDown(OVRInput.Button.Start);
    }

    public bool ButtonDownLeftShoulder()
    {
        return OVRInput.GetDown(OVRInput.Button.PrimaryShoulder);
    }

    #endregion

    #region 左タッチGet

    public bool TouchX()
    {
        return OVRInput.Get(OVRInput.Touch.Three);
    }

    public bool TouchY()
    {
        return OVRInput.Get(OVRInput.Touch.Four);
    }

    public bool TouchLeftUpTrigger()
    {
        return OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger);
    }

    public bool TouchLeftStick()
    {
        return OVRInput.Get(OVRInput.Touch.PrimaryThumbstick);
    }

    public bool TouchLeftRest()
    {
        return OVRInput.Get(OVRInput.Touch.PrimaryThumbRest);
    }
    #endregion

    #region 左タッチGetUp

    public bool TouchUpX()
    {
        return OVRInput.GetUp(OVRInput.Touch.Three);
    }

    public bool TouchUpY()
    {
        return OVRInput.GetUp(OVRInput.Touch.Four);
    }

    public bool TouchUpLeftUpTrigger()
    {
        return OVRInput.GetUp(OVRInput.Touch.PrimaryIndexTrigger);
    }

    public bool TouchUpLeftStick()
    {
        return OVRInput.GetUp(OVRInput.Touch.PrimaryThumbstick);
    }

    public bool TouchUpLeftRest()
    {
        return OVRInput.GetUp(OVRInput.Touch.PrimaryThumbRest);
    }
    #endregion

    #region 左タッチGetDown

    public bool TouchDownX()
    {
        return OVRInput.GetDown(OVRInput.Touch.Three);
    }

    public bool TouchDownY()
    {
        return OVRInput.GetDown(OVRInput.Touch.Four);
    }

    public bool TouchDownLeftUpTrigger()
    {
        return OVRInput.GetDown(OVRInput.Touch.PrimaryIndexTrigger);
    }

    public bool TouchDownLeftStick()
    {
        return OVRInput.Get(OVRInput.Touch.PrimaryThumbstick);
    }

    public bool TouchDownLeftRest()
    {
        return OVRInput.Get(OVRInput.Touch.PrimaryThumbRest);
    }
    #endregion

    #region 左二アタッチGet

    public bool NearTouchLeftTrigger()
    {
        return OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger);
    }

    public bool NearTouchLeftButton()
    {
        return OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons);
    }
    #endregion

    #region 左二アタッチGetUp

    public bool NearTouchLeftUpTrigger()
    {
        return OVRInput.GetUp(OVRInput.NearTouch.PrimaryIndexTrigger);
    }

    public bool NearTouchLeftUpButton()
    {
        return OVRInput.GetUp(OVRInput.NearTouch.PrimaryThumbButtons);
    }
    #endregion

    #region 左二アタッチGetDown

    public bool NearTouchLeftDownTrigger()
    {
        return OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger);
    }

    public bool NearTouchLeftDownButton()
    {
        return OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons);
    }
    #endregion

    #region 左Axis1Get
    public float Axis1LeftDownTrigger()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
    }

    public float Axis1LeftUpTrigger()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
    }

    public float Axis1LeftTriggerCurl()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTriggerCurl);
    }

    public float Axis1LeftTriggerSlide()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTriggerSlide);
    }

    public float Axis1LeftTriggerStylus()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryStylusForce);
    }

    public float Axis1LeftTriggerRest()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryThumbRestForce);
    }
    #endregion

    #region 左Axis1GetUp

    public float Axis1UpLeftTriggerCurl()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTriggerCurl);
    }

    public float Axis1UpLeftTriggerSlide()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTriggerSlide);
    }

    public float Axis1UpLeftTriggerStylus()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryStylusForce);
    }

    public float Axis1UpLeftTriggerRest()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryThumbRestForce);
    }
    #endregion

    #region 左Axis1GetDown

    // なにもなし

    #endregion

    #region 左Axis2Get
    public Vector2 Axis2LeftStick()
    {
        return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
    }

    #endregion

    #region 右ボタンGet

    /// <summary>
    /// 右のBボタンを押しているか
    /// </summary>
    /// <returns>押していたらtrue</returns>
    public bool ButtonB()
    {
        return OVRInput.Get(OVRInput.Button.Two);
    }

    /// <summary>
    /// 右のAボタンを押しているか
    /// </summary>
    /// <returns></returns>
    public bool ButtonA()
    {
        return OVRInput.Get(OVRInput.Button.One);
    }

    public bool ButtonRightDownTrigger()
    {
        return OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
    }

    public bool ButtonRightUpTrigger()
    {
        return OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
    }

    public bool ButtonRightStickPush()
    {
        return OVRInput.Get(OVRInput.Button.SecondaryThumbstick);
    }

    public bool ButtonRightStickDown()
    {
        return OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown);
    }

    public bool ButtonRightStickUp()
    {
        return OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp);
    }

    public bool ButtonRightStickLeft()
    {
        return OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft);
    }

    public bool ButtonRightStickRight()
    {
        return OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight);
    }

    public bool ButtonRightShoulder()
    {
        return OVRInput.Get(OVRInput.Button.SecondaryShoulder);
    }

    #endregion

    #region 右ボタンGetUp

    /// <summary>
    /// 右のBボタンが離されたか
    /// </summary>
    /// <returns>離されたらtrue</returns>
    public bool ButtonUpB()
    {
        return OVRInput.GetUp(OVRInput.Button.Two);
    }

    /// <summary>
    /// 右のAボタンが離されたか
    /// </summary>
    /// <returns>離されたらtrue</returns>
    public bool ButtonUpA()
    {
        return OVRInput.GetUp(OVRInput.Button.One);
    }

    public bool ButtonUpRightDownTrigger()
    {
        return OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger);
    }

    public bool ButtonUpRightUpTrigger()
    {
        return OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger);
    }

    public bool ButtonUpRightStickPush()
    {
        return OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick);
    }

    public bool ButtonUpRightStickDown()
    {
        return OVRInput.GetUp(OVRInput.Button.SecondaryThumbstickDown);
    }

    public bool ButtonUpRightStickUp()
    {
        return OVRInput.GetUp(OVRInput.Button.SecondaryThumbstickUp);
    }

    public bool ButtonUpRightStickLeft()
    {
        return OVRInput.GetUp(OVRInput.Button.SecondaryThumbstickLeft);
    }

    public bool ButtonUpRightStickRight()
    {
        return OVRInput.GetUp(OVRInput.Button.SecondaryThumbstickRight);
    }


    public bool ButtonUpRightShoulder()
    {
        return OVRInput.GetUp(OVRInput.Button.SecondaryShoulder);
    }

    #endregion
    
    #region 右ボタンGetDown

    /// <summary>
    /// 右のBボタンが押されたか
    /// </summary>
    /// <returns>押した瞬間true</returns>
    public bool ButtonDownB()
    {
        return OVRInput.GetDown(OVRInput.Button.Two);
    }

    /// <summary>
    /// 右のAボタンが押されたか
    /// </summary>
    /// <returns>押された瞬間true</returns>
    public bool ButtonDownA()
    {
        return OVRInput.GetDown(OVRInput.Button.One);
    }

    public bool ButtonDownRightDownTrigger()
    {
        return OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger);
    }

    public bool ButtonDownRightUpTrigger()
    {
        return OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger);
    }

    public bool ButtonDownRightStickPush()
    {
        return OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick);
    }

    public bool ButtonDownRightStickDown()
    {
        return OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown);
    }

    public bool ButtonDownRightStickUp()
    {
        return OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp);
    }

    public bool ButtonDownRightStickLeft()
    {
        return OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft);
    }

    public bool ButtonDownRightStickRight()
    {
        return OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight);
    }


    public bool ButtonDownRightShoulder()
    {
        return OVRInput.GetDown(OVRInput.Button.SecondaryShoulder);
    }

    #endregion

    #region 右タッチGet

    public bool TouchB()
    {
        return OVRInput.Get(OVRInput.Touch.Two);
    }

    public bool TouchA()
    {
        return OVRInput.Get(OVRInput.Touch.One);
    }

    public bool TouchRightUpTrigger()
    {
        return OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger);
    }

    public bool TouchRightStick()
    {
        return OVRInput.Get(OVRInput.Touch.SecondaryThumbstick);
    }

    public bool TouchRightRest()
    {
        return OVRInput.Get(OVRInput.Touch.SecondaryThumbRest);
    }
    #endregion

    #region 右タッチGetUp

    public bool TouchUpB()
    {
        return OVRInput.GetUp(OVRInput.Touch.Two);
    }

    public bool TouchUpA()
    {
        return OVRInput.GetUp(OVRInput.Touch.One);
    }

    public bool TouchUpRightUpTrigger()
    {
        return OVRInput.GetUp(OVRInput.Touch.SecondaryIndexTrigger);
    }

    public bool TouchUpRightStick()
    {
        return OVRInput.GetUp(OVRInput.Touch.SecondaryThumbstick);
    }

    public bool TouchUpRightRest()
    {
        return OVRInput.GetUp(OVRInput.Touch.SecondaryThumbRest);
    }
    #endregion

    #region 右タッチGetDown

    public bool TouchDownB()
    {
        return OVRInput.GetDown(OVRInput.Touch.Two);
    }

    public bool TouchDownA()
    {
        return OVRInput.GetDown(OVRInput.Touch.One);
    }

    public bool TouchDownRightUpTrigger()
    {
        return OVRInput.GetDown(OVRInput.Touch.SecondaryIndexTrigger);
    }

    public bool TouchDownRightStick()
    {
        return OVRInput.Get(OVRInput.Touch.SecondaryThumbstick);
    }

    public bool TouchDownRightRest()
    {
        return OVRInput.Get(OVRInput.Touch.SecondaryThumbRest);
    }
    #endregion

    #region 右二アタッチGet

    public bool NearTouchRightTrigger()
    {
        return OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger);
    }

    public bool NearTouchRightButton()
    {
        return OVRInput.Get(OVRInput.NearTouch.SecondaryThumbButtons);
    }
    #endregion

    #region 右二アタッチGetUp

    public bool NearTouchRightUpTrigger()
    {
        return OVRInput.GetUp(OVRInput.NearTouch.SecondaryIndexTrigger);
    }

    public bool NearTouchRightUpButton()
    {
        return OVRInput.GetUp(OVRInput.NearTouch.SecondaryThumbButtons);
    }
    #endregion

    #region 右二アタッチGetDown

    public bool NearTouchRightDownTrigger()
    {
        return OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger);
    }

    public bool NearTouchRightDownButton()
    {
        return OVRInput.Get(OVRInput.NearTouch.SecondaryThumbButtons);
    }
    #endregion

    #region 右Axis1Get
    public float Axis1RightDownTrigger()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
    }

    public float Axis1RightUpTrigger()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
    }

    public float Axis1RightTriggerCurl()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTriggerCurl);
    }

    public float Axis1RightTriggerSlide()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTriggerSlide);
    }

    public float Axis1RightTriggerStylus()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryStylusForce);
    }

    public float Axis1RightTriggerRest()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryThumbRestForce);
    }
    #endregion

    #region 右Axis1GetUp

    // なにもなし

    #endregion

    #region 右Axis1GetDown

    // なにもなし

    #endregion

    #region 右Axis2Get
    public Vector2 Axis2RightStick()
    {
        return OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
    }

    #endregion]

    #region 左Controller
    /// <summary>
    /// 左手のワールド位置
    /// </summary>
    /// <returns></returns>
    public Vector3 GetLeftHandPosition()
    {
        return trackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
    }

    /// <summary>
    /// 左手のワールドローテーション
    /// </summary>
    /// <returns></returns>
    public Vector3 GetLeftHandRotation()
    {
        return trackingSpace.TransformPoint(OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch).eulerAngles);
    }

    #endregion

    #region 右Controller
    /// <summary>
    /// 右手ワールドポジション
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRightHandPosition()
    {
        return trackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
    }

    /// <summary>
    /// 右手ワールドローテーション
    /// </summary>
    /// <returns>ローカルローテーション</returns>
    public Vector3 GetRightHandRotation()
    {
        return trackingSpace.TransformPoint(OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch).eulerAngles);
    }

    #endregion

    #region 便利計算

    /// <summary>
    /// fixedUpdateで変数変化してるの注意
    /// </summary>
    /// <returns>速さ</returns>
    public float CalcLeftHandSpeed()
    {
        Vector3 difPos = afterLeftHandPos - beforeLeftHandPos;
        float difTime = afterHandTime - beforeHandTime;
        return difPos.magnitude / difTime;              
    }
    /// <summary>
    /// fixedUpdateで変数変化してるの注意
    /// </summary>
    /// <returns>速さ</returns>
    public float CalcRightHandSpeed()
    {
        Vector3 difPos = afterRightHandPos - beforeRightHandPos;
        float difTime = afterHandTime - beforeHandTime;
        return difPos.magnitude / difTime;
    }
    #endregion


    /// <summary>
    /// 前の手の位置と時間記録
    /// </summary>
    private void SetBeforeHandPosition()
    {
        beforeLeftHandPos = GetLeftHandPosition();
        beforeRightHandPos = GetRightHandPosition();
        beforeHandTime = Time.time;
        
    }

    /// <summary>
    /// 前の手の位置と時間記録
    /// </summary>
    private void SetBeforeHandPosition(Vector3 reloadLeftPos,Vector3 reloadRightPos,float reloadTime)
    {
        beforeLeftHandPos = reloadLeftPos;
        beforeRightHandPos = reloadRightPos;
        beforeHandTime = reloadTime;

    }

    /// <summary>
    /// 今回の手の位置と時間記録
    /// </summary>
    private void SetAfterHandPosition()
    {
        afterLeftHandPos = GetLeftHandPosition();
        afterRightHandPos = GetRightHandPosition();
        afterHandTime = Time.time;
    }


}