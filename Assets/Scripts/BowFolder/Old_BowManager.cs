// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using System;
using UnityEngine;

interface IFBowManagerGetDistance
{
    /// <summary>
    /// ���݂̋|���������������Ԃ����
    /// </summary>
    public float GetDrawDistance();
}



[RequireComponent(typeof(BowDraw), typeof(BowHold), typeof(BowShotAim))]
[RequireComponent(typeof(BowVibe), typeof(AttractEffectCustom), typeof(AttractZone))]
[RequireComponent(typeof(Inhall), typeof(BowSE))]
public class Old_BowManager : MonoBehaviour, IFBowManagerGetDistance, IFBowManagerQue,IFBowManager_GetStats
{

    #region ����public�������ϐ�
    [SerializeField] TagObject _InputTagName = default;

    [SerializeField] TagObject _poolTagName = default;

    [SerializeField] TagObject _playerManagerTagName = default;

    [SerializeField] Transform _handLeftPosition = default;

    [SerializeField] Transform _handRightPosition = default;
    /// <summary>
    /// �����I�u�W�F�N�g
    /// </summary>
    [SerializeField] GameObject _drawObject = default;

    [SerializeField] Transform _changeHandObjectTransform = default;

    /// <summary>
    /// Debug���[�h
    /// </summary>
    public bool _mouseMode = false;

    /// <summary>
    /// VR�Œl�ǂ����[�h
    /// </summary>
    public bool _traceValue = false;

    #endregion

    #region �N���X�A�\����

    IFBowDraw _draw = default;

    IFBowHold _hold = default;

    IFBowShotAim _aim = default;

    IFBowVibe _vibe = default;

    IFAttractEffectCustom _inhallCustom = default;

    AttractZone _attract = default;

    private InputManagement _mng = default;

    private ObjectPoolSystem _poolSystem = default;

    private BowSE _bowSE = default;
    //�f�o�b�N�p
    private PlayerManager _playerManager = default;
    //�f�o�b�O
    private VR_Trace_Value _trace = default;

    delegate bool HandUseDelegate();

    delegate Vector3 HandPositionDelegate();

    delegate void VibeEndDelegate();



    /// <summary>
    /// ��̏�ԊǗ��^
    /// </summary>
    enum HandStats
    {
        None,
        Hold,
    }

    HandUseDelegate _handTriggerDownDelegate = default;

    HandUseDelegate _handTriggerUpDelegate = default;

    HandPositionDelegate _handPositionDelegate = default;

    CashObjectInformation _arrow = default;

    HandStats _stats = HandStats.None;

    Vector3 _firstDrawObjectPositon = default;

    Vector3 _firstDrawObjectWorldPositon = default;

    Vector3 _directionMainCameraLookToBow = default;

    Quaternion _myQuaternion = default;

    #endregion

    #region �p�����[�^
    /// <summary>
    /// ���͂߂錷����̋���
    /// </summary>
    [SerializeField] float grapLimitDistance = 0.5f;

    /// <summary>
    /// ����������E�p�x
    /// </summary>
    [SerializeField] float drawLimitAngle = 90f;

    /// <summary>
    /// ����������E����
    /// </summary>
    [SerializeField] float drawLimitDistance = 5f;

    /// <summary>
    /// ���������p���[�Ɋ|����͂̋���
    /// </summary>
    [SerializeField] float add = 1f;

    /// <summary>
    /// �A�˖h�~
    /// </summary>
    [SerializeField] float cantShotDistance = 0.01f;

    /// <summary>
    /// ���������Ƀ}�b�N�X�p���[�ɂȂ鋗���̍ō������̊���
    /// </summary>
    [SerializeField] float drawDistancePercentMaxPower = 0.9f;

    #endregion

    float _distanceCameraToDrawObject = default;

    private void Start()
    {
        #region ����������

        _draw = GetComponent<BowDraw>();

        _hold = GetComponent<BowHold>();

        _aim = GetComponent<BowShotAim>();

        _attract = GetComponent<AttractZone>();

        _vibe = GetComponent<BowVibe>();

        _inhallCustom = GetComponent<AttractEffectCustom>();

        _bowSE = GetComponent<BowSE>();

        _mng = GameObject.FindGameObjectWithTag(_InputTagName.TagName).GetComponent<InputManagement>();

        _playerManager = GameObject.FindGameObjectWithTag(_playerManagerTagName.TagName).GetComponent<PlayerManager>();

        _poolSystem = GameObject.FindGameObjectWithTag(_poolTagName.TagName).GetComponent<ObjectPoolSystem>();

        _firstDrawObjectPositon = _drawObject.transform.localPosition;

        _firstDrawObjectWorldPositon = _drawObject.transform.position;

        _distanceCameraToDrawObject = Vector3.Distance(_drawObject.transform.position, Camera.main.transform.position);

        _directionMainCameraLookToBow = (_drawObject.transform.position - Camera.main.transform.position).normalized;

        _myQuaternion = transform.localRotation;

        #endregion

        // �C���v�b�g�̐ݒ�
        SetUsingHand();

        // �f�o�b�O�p
        if (_traceValue)
        {
            _trace = this.gameObject.AddComponent<VR_Trace_Value>();
        }
    }

    private void Update()
    {
        BowManagement();
    }

    /// <summary>
    /// ������Ă�ł���Ȃ��Ɠ����Ă���Ȃ���{�s��
    /// </summary>
    public void BowManagement()
    {
        // ��g���K�[�����āA�肪�󂢂Ă����ԂȂ�
        if (_handTriggerDownDelegate() && _stats == HandStats.None)
        {

            // ����͂߂�͈͓��Ȃ�͂�
            if (grapLimitDistance > Vector3.Distance(_drawObject.transform.position, _handPositionDelegate()))
            {
                Grap();
            }

            // �|���������鏈��
            if (grapLimitDistance > Vector3.Distance(_changeHandObjectTransform.position, _handPositionDelegate()) && !_mouseMode)
            {
                ChangeHand();
            }

        }
        // �|�̌���͂�ł����ԂȂ�
        else if (_stats == HandStats.Hold)
        {
            Holding();

        }
        else
        {
            //�����̂���߂���v���y�����~�߂�A���[�^�[���O�ɂ���
        }

    }


    /// <summary>
    /// ���݂̋|���������������Ԃ����
    /// </summary>
    public float GetDrawDistance()
    {
        return Vector3.Distance(_firstDrawObjectPositon, _drawObject.transform.localPosition);
    }

    /// <summary>
    /// ���݂̋|�̈���������(%)
    /// </summary>
    public float GetPercentDrawDistance()
    {
        return Vector3.Magnitude(_drawObject.transform.position - transform.position) / (drawLimitDistance * drawDistancePercentMaxPower);
    }

    /// <summary>
    /// ����L���[����
    /// </summary>
    /// <param name="arrow">�L���[����I�u�W�F�N�g</param>
    public void ArrowQue(CashObjectInformation arrow)
    {
        _poolSystem.ReturnObject(arrow);
    }

    public bool IsHolding
    {
        get
        { return _stats == HandStats.Hold; }
    }
    /// <summary>
    /// �͂ݒ�
    /// </summary>
    private void Holding()
    {

        // �h���[�I�u�W�F�N�g�����Ă����Ɉړ�
        _draw.BowDrawing(_handPositionDelegate(), _drawObject, _firstDrawObjectPositon);

        _vibe.StartDrawVibe(GetPercentDrawDistance());
        SetText(GetPercentDrawDistance().ToString());
        _inhallCustom.SetActive(true);

        _inhallCustom.SetEffectSize(GetPercentDrawDistance());

        _attract.SetAngle(GetPercentDrawDistance());

        _bowSE.CallDrawingSE(GetPercentDrawDistance());

        _bowSE.CallAttractSE(GetPercentDrawDistance());

        // �|�̃��[�e�[�V�����ύX
        TurnBow();
        // �|������������Ǝ肪�����痣��Ă���
        if (!ConeDecision.ConeInObject(transform, _drawObject.transform, drawLimitAngle, drawLimitDistance, -1))
        {
            ShotProcess();
        }

        // ��g���K�[�����ċ|�̌���͂�ł����ԂȂ炤��
        if (!_handTriggerUpDelegate())
        {
            ShotProcess();
        }
    }

    /// <summary>
    /// ��̐؂�ւ�
    /// </summary>
    private void ChangeHand()
    {
        // ����̏ꍇ
        if (_mng.P_EmptyHand == InputManagement.EmptyHand.Left)
        {
            _mng.P_EmptyHand = InputManagement.EmptyHand.Right;

            transform.parent = _handLeftPosition.parent;

            transform.localPosition = Vector3.zero;

            transform.localRotation = _myQuaternion;
        }
        // �E��̏ꍇ
        else
        {
            _mng.P_EmptyHand = InputManagement.EmptyHand.Left;

            transform.parent = _handRightPosition.parent;

            transform.localPosition = Vector3.zero;

            transform.localRotation = _myQuaternion;
        }

        SetUsingHand();
    }

    /// <summary>
    /// �͂�
    /// </summary>
    private void Grap()
    {
        _hold.BowHoldStart(_handPositionDelegate(), _drawObject);

        _bowSE.CallDrawStart();

        _bowSE.CallAttractStartSE();

        _arrow = _poolSystem.CallObject(PoolEnum.PoolObjectType.arrow, _drawObject.transform.position);

        _arrow.transform.rotation = this.transform.rotation;

        _arrow.transform.parent = _drawObject.transform;

        _arrow.transform.position -= _arrow.transform.GetChild(0).position - _arrow.transform.position;

        _stats = HandStats.Hold;
    }

    /// <summary>
    /// �����̂����܂Ƃ߂�����
    /// </summary>
    private void ShotProcess()
    {
        _bowSE.StopAttractSE();

        _vibe.EndDrawVibe();

        _vibe.StartShotVibe(GetPercentDrawDistance());

        _inhallCustom.SetActive(false);

        _playerManager.SetArrowMoveSpeed(GetPercentDrawDistance() * add);

        StartShotArrow(_aim.GetAim());

        // �����Ƌ|�����ĂȂ���΂����Ƀf�L���[
        if (GetDrawDistance() < cantShotDistance)
        {
            //Destroy(_arrow);
            _poolSystem.ReturnObject(_arrow);

        }
        else
        {
            // ���̂�SE�炷
            _bowSE.CallShotSE();

        }

        _drawObject.transform.localPosition = _firstDrawObjectPositon;

        transform.localRotation = _myQuaternion;

        _attract.SetAngle(0f);

        _stats = HandStats.None;
    }

    /// <summary>
    /// �|�ƌ��̕����x�N�g���ŋ|�̊p�x���߂�AZ�͉�
    /// </summary>
    private void TurnBow()
    {
        Vector3 bowPosition = transform.position;

        Vector3 drawPosition = _drawObject.transform.position;

        Vector3 directionDrawObjectToBow = bowPosition - drawPosition;

        float angleBowZ = transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.LookRotation(directionDrawObjectToBow);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y, angleBowZ);

    }

    /// <summary>
    /// �|�̎����Ă����ɂ��C���v�b�g�̐ݒ�
    /// </summary>
    private void SetUsingHand()
    {
        // �}�E�X���[�h
        if (_mouseMode)
        {
            _handTriggerDownDelegate = new HandUseDelegate(GetMouseDownButton);

            _handTriggerUpDelegate = new HandUseDelegate(GetMouseDownButton);

            _handPositionDelegate = new HandPositionDelegate(GetMousePos);

            return;
        }

        // �����
        if (_mng.P_EmptyHand == InputManagement.EmptyHand.Left)
        {
            _handTriggerDownDelegate = new HandUseDelegate(_mng.ButtonLeftUpTrigger);

            _handTriggerUpDelegate = new HandUseDelegate(_mng.ButtonLeftUpTrigger);

            _handPositionDelegate = new HandPositionDelegate(GetHandLeftPosition);

            _vibe.SetRightShotAction();




        }
        // �E���
        else
        {
            _handTriggerDownDelegate = new HandUseDelegate(_mng.ButtonRightUpTrigger);

            _handTriggerUpDelegate = new HandUseDelegate(_mng.ButtonRightUpTrigger);

            _handPositionDelegate = new HandPositionDelegate(GetHandRightPosition);

            _vibe.SetLeftShotAction();
        }

    }

    /// <summary>
    /// ����̃|�W�V�����Ԃ�
    /// </summary>
    private Vector3 GetHandLeftPosition()
    {
        return _handLeftPosition.position;
    }

    /// <summary>
    /// �E��̃|�W�V�����Ԃ�
    /// </summary>
    private Vector3 GetHandRightPosition()
    {
        return _handRightPosition.position;
    }

    /// <summary>
    /// �ˏo�J�n
    /// </summary>
    /// <param name="aim">�ˏo���������x�N�g��������LookRotation�g���Ȃ�transform.position������</param>
    private void StartShotArrow(Vector3 aim)
    {
        // �������Ȃ񂩂�����
        //GameObject obj=  Instantiate(debug, transform.position, Quaternion.LookRotation(aim + transform.position));
        _playerManager.ShotArrow(aim);
    }

    private void SetText(string text)
    {
        if (_traceValue)
        {
            _trace.SetText(text);
        }
        else
        {
            X_Debug.Log("�l�ǂ����[�h����");
        }

    }

    #region �}�E�X�֌W
    private Vector3 GetMousePos()
    {
        // �}�E�X�̓񎟌���̍��W
        Vector3 pos = Input.mousePosition;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        Vector3 finalPos = _directionMainCameraLookToBow * _distanceCameraToDrawObject + worldPos;

        return finalPos;

    }

    bool GetMouseDownButton() => Input.GetMouseButton(0);

    bool GetMouseUpButton() => Input.GetMouseButtonUp(0);

    #endregion
}
