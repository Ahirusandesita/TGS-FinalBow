// --------------------------------------------------------- 
// FPSBow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;

[RequireComponent(typeof(BowTransformControl), typeof(CamaraRoteNotMainCamera), typeof(BowSE))]
public sealed class FPSBow : BowManager
{
    #region variable 

    [SerializeField] float _moveSpeed = 10f;

    [SerializeField] float _arrowOriginPercentPower = 1f;

    [SerializeField] Transform _arrowPosition = default;

    [SerializeField] CamaraRoteNotMainCamera _rotationControl = default;

    IFBowTransformControl_FPS _transformControl = default;

    const int LEFT_MOUSE_BUTTON = 0;

    Transform _myTransform;

    #endregion
    #region property
    #endregion
    #region method

    // 初期化群
    protected override void Start()
    {
        _myTransform = transform;

        _transformControl = GetComponent<BowTransformControl>();

        base.Start();

        GameObject[] deleteObjects = GameObject.FindGameObjectsWithTag(InhallLibTags.FPSDelete);

        if (deleteObjects.Length > 0)
        {
            foreach(GameObject obj in deleteObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    // 入力受付&処理
    private void Update()
    {
        BowUpdateCallProcess();

        //wasdで移動 
        _transformControl.WASDMove((_arrowPosition.position - _myTransform.position).normalized, _moveSpeed);

        // カメラの回転
        _rotationControl.CameraRote();
    }

    // クリックした瞬間の処理
    protected override void ProcessOfGrapObject()
    {
        BowGrapCreateArrow(_arrowPosition.position);
        // 矢のトランスフォーム調整
        _transformControl.SetArrowFirstTransform(_arrow.transform, _arrowPosition);
    }

    // クリック長押しの時の処理
    protected override void ProcessOfHoldObject()
    {
        // 吸い込み有効
        BowHoldingSetAttract();
    }

    // クリック離した時の処理
    protected override void ProcessOfReleaseObjcect()
    {
        // 前方に矢を射撃
        BowShotSetting(_myTransform.forward - _myTransform.position);

    }

    // 矢の速度の設定(固定)
    protected override float GetShotPercentPower()
    {
        return _arrowOriginPercentPower;
    }

    // インプットの設定
    protected override void SetInputDelegate()
    {
        // 左クリック押した時
        _grapTriggerInput = () => Input.GetMouseButton(LEFT_MOUSE_BUTTON);

        // 左クリック離した時
        _releaseTriggerInput = () => !Input.GetMouseButton(LEFT_MOUSE_BUTTON);
    }


    #endregion
}