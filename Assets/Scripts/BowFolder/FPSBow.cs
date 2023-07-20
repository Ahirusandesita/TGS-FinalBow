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

    // �������Q
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

    // ���͎�t&����
    private void Update()
    {
        BowUpdateCallProcess();

        //wasd�ňړ� 
        _transformControl.WASDMove((_arrowPosition.position - _myTransform.position).normalized, _moveSpeed);

        // �J�����̉�]
        _rotationControl.CameraRote();
    }

    // �N���b�N�����u�Ԃ̏���
    protected override void ProcessOfGrapObject()
    {
        BowGrapCreateArrow(_arrowPosition.position);
        // ��̃g�����X�t�H�[������
        _transformControl.SetArrowFirstTransform(_arrow.transform, _arrowPosition);
    }

    // �N���b�N�������̎��̏���
    protected override void ProcessOfHoldObject()
    {
        // �z�����ݗL��
        BowHoldingSetAttract();
    }

    // �N���b�N���������̏���
    protected override void ProcessOfReleaseObjcect()
    {
        // �O���ɖ���ˌ�
        BowShotSetting(_myTransform.forward - _myTransform.position);

    }

    // ��̑��x�̐ݒ�(�Œ�)
    protected override float GetShotPercentPower()
    {
        return _arrowOriginPercentPower;
    }

    // �C���v�b�g�̐ݒ�
    protected override void SetInputDelegate()
    {
        // ���N���b�N��������
        _grapTriggerInput = () => Input.GetMouseButton(LEFT_MOUSE_BUTTON);

        // ���N���b�N��������
        _releaseTriggerInput = () => !Input.GetMouseButton(LEFT_MOUSE_BUTTON);
    }


    #endregion
}