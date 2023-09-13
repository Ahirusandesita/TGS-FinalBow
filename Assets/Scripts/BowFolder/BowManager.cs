// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using System;
using UnityEngine;
using System.Collections;

public interface IFBowManagerQue
{
    /// <summary>
    /// ����L���[�ɂ����
    /// </summary>
    /// <param name="arrow">�L���[�ɂ����I�u�W�F�N�g</param>
    public void ArrowQue(CashObjectInformation arrow);

    /// <summary>
    /// ��̘A�˃C�x���g�ʒm
    /// </summary>
    public void SetArrowMachineGun(int arrowValue, float delayTime);
}

public interface IFBowManagerUpdate
{
    /// <summary>
    /// �|�𓮂������߂�update�ł��
    /// ProcessOf����n�܂閼�O�̃��\�b�h�̂����ꂩ�����
    /// </summary>
    public void BowUpdateCallProcess();
}

public interface IFBowManager_GetStats
{
    public bool IsHolding { get; }
}
[RequireComponent(typeof(Inhall), typeof(AttractEffectCustom), typeof(AttractZone))]

public abstract class BowManager : MonoBehaviour, IFBowManagerQue, IFBowManagerUpdate, IFBowManager_GetStats
{
    #region ����public�������ϐ�
    [SerializeField] protected TagObject _InputTagName = default;

    [SerializeField] protected TagObject _poolTagName = default;

    [SerializeField] protected TagObject _playerManagerTagName = default;

    #endregion

    #region �N���X�A�\����

    protected IFAttractEffectCustom _inhallCustom = default;

    protected AttractZone _attract = default;

    protected ObjectPoolSystem _poolSystem = default;

    protected IFPlayerManagerShotArrow _playerManager = default;

    protected CashObjectInformation _arrow = default;

    protected IFLockOnSystem _lockOnSystem = default;

    protected IFBowSE_CallToBow _bowSE = default;

    protected Func<bool> _grapTriggerInput = default;

    protected Func<bool> _releaseTriggerInput = default;

    protected bool _canMachineGun;

    protected int _valueMachineGun;

    protected WaitForSeconds _delayTime;

    protected ReticleSystem _reticleSystem;

    protected Transform _bowTransform = default;

    float _setedArrowSpeed = 0f;



    /// <summary>
    /// ��̏�ԊǗ��^
    /// </summary>
    protected enum HandStats
    {
        None,
        Hold,
    }

    protected HandStats _handStats = HandStats.None;

    #endregion

    #region �p�����[�^

    /// <summary>
    /// ���������p���[�Ɋ|����͂̋���
    /// </summary>
    [SerializeField] float arrowSpeed = 2000f;

    protected float _percentDrawPower = 0f;

    Vector3 _shotStartRoteCache = Vector3.zero;

    #endregion

    public bool IsHolding
    {
        get
        {
            return _handStats == HandStats.Hold;
        }
    }

    protected abstract Transform GetSpawnPosition { get; }

    protected abstract Vector3 GetShotDirection { get; }


    /// <summary>
    /// �K�{�ϐ��̃Q�b�g�R���|�[�l���g��SetInputDelegate()���s��
    /// </summary>
    protected virtual void Start()
    {
        #region ����������

        _attract = GetComponent<AttractZone>();

        _inhallCustom = GetComponent<AttractEffectCustom>();

        _playerManager = GameObject.FindGameObjectWithTag(_playerManagerTagName.TagName).GetComponent<PlayerManager>();

        _poolSystem = GameObject.FindGameObjectWithTag(_poolTagName.TagName).GetComponent<ObjectPoolSystem>();

        _bowSE = GetComponent<BowSE>();

        _lockOnSystem = GetComponent<LockOnSystem>();

        _bowTransform = GameObject.FindGameObjectWithTag(InhallLibTags.BowController).GetComponent<Transform>();

        _reticleSystem = GameObject.FindGameObjectWithTag("ReticleController").GetComponent<ReticleSystem>();

        #endregion

        // �C���v�b�g�̐ݒ�
        SetInputDelegate();
    }

    /// <summary>
    /// ������Ă�ł���Ȃ��Ɠ����Ă���Ȃ���{�s��
    /// </summary>
    public void BowUpdateCallProcess()
    {
        // ����͂ޓ��͂��������ꍇ
        if ((_grapTriggerInput() && _handStats == HandStats.None) && !_canMachineGun)
        {
            // �͂�
            ProcessOfGrapObject();
        }
        // ��������͂�ł����ꍇ
        else if (_handStats == HandStats.Hold)
        {
            // �������p���[(%)�Ŏ擾(0-1)
            _percentDrawPower = GetShotPercentPower();

            if (!_releaseTriggerInput())
            {
                // ����͂ݑ�����
                ProcessOfHoldObject();

                return;
            }
            // ���𗣂�
            ProcessOfReleaseObjcect();
        }

    }


    /// <summary>
    /// ����L���[�ɂ����
    /// </summary>
    /// <param name="arrow">�L���[�ɂ����I�u�W�F�N�g</param>
    public void ArrowQue(CashObjectInformation arrow)
    {
        _poolSystem.ReturnObject(arrow);
    }

    /// <summary>
    /// ����͂ޏ���
    /// �͂ޑ�������ĉ��������ĂȂ��ƌĂ΂��
    /// </summary>
    protected abstract void ProcessOfGrapObject();

    /// <summary>
    /// ����͂ݒ��̎��̏���
    /// �͂ޑ���𑱂��Ă���ԌĂ΂ꑱ����
    /// </summary>
    protected abstract void ProcessOfHoldObject();


    /// <summary>
    /// ���𗣂����̏���
    /// �͂ޑ�����~�߂�ƌĂ΂��
    /// </summary>
    protected abstract void ProcessOfReleaseObjcect();

    /// <summary>
    /// �|�̌���͂݊J�n���̕K�{�����A��𐶐�
    /// </summary>
    protected void BowGrapCreateArrow(Vector3 spawnPosition)
    {

        _bowSE.CallDrawStart();

        _bowSE.CallAttractStartSE();

        // ��𐶐�
        _arrow = _poolSystem.CallObject(PoolEnum.PoolObjectType.arrow, spawnPosition);

        _handStats = HandStats.Hold;
    }

    /// <summary>
    /// �|�̌���͂ݒ��K�{�����A�z���݂�L����
    /// </summary>
    protected void BowHoldingSetAttract()
    {
        _bowSE.CallAttractSE(_percentDrawPower);

        _bowSE.CallDrawingSE(_percentDrawPower);

        // �G�t�F�N�g���Đ�
        _inhallCustom.SetActive(true);

        // �G�t�F�N�g�̓��I���H
        _inhallCustom.SetEffectSize(_percentDrawPower);

        // �z���ݔ�����|���������ʂɂ���ĕς���
        _attract.SetAngle(_percentDrawPower);

        // �z�[�~���O�̃^�[�Q�b�g�I�胁�\�b�h���Ăяo��
        _lockOnSystem.TargetLockOn(_bowTransform);

        if (_percentDrawPower > 0)
        {
            _reticleSystem.CreateReticleSystem(_percentDrawPower * arrowSpeed);
        }
        else
        {
            _reticleSystem.EndCreate();
        }
    }


    /// <summary>
    /// �ˌ����̕K�{����
    /// </summary>
    protected void BowShotSetting(Vector3 shotDirection)
    {

        // �Đ�����Ă����G�t�F�N�g�𖳌�
        _inhallCustom.SetActive(false);
        _setedArrowSpeed = _percentDrawPower * arrowSpeed;
        _shotStartRoteCache = shotDirection;
        BowShotArrow(shotDirection);

        // �z���ݔ��菉����
        _attract.SetAngle(0f);

        _lockOnSystem.DestroyUI();

        _reticleSystem.EndCreate();

        _handStats = HandStats.None;
    }

    /// <summary>
    /// ���Ă�Ȃ猂��
    /// </summary>
    /// <param name="shotDirection"></param>
    protected virtual void BowShotArrow(Vector3 shotDirection)
    {
        _bowSE.CallShotSE();

        // ��̃X�s�[�h�Z�b�g�A�|���������ʂɂ���ĕς���
        _playerManager.SetArrowMoveSpeed(_setedArrowSpeed);

        // �������
        _playerManager.ShotArrow(shotDirection);

        if (_canMachineGun && --_valueMachineGun > 0)
        {
            
            StartCoroutine(MachineGun());

        }
        else if (_valueMachineGun <= 0)
        {
            _playerManager.CanRapid = false;
            _canMachineGun = false;
        }


    }

    protected virtual IEnumerator MachineGun()
    {
        yield return _delayTime;
        Transform arrow = _poolSystem.CallObject(PoolEnum.PoolObjectType.arrow, GetSpawnPosition.position).transform;

        arrow.LookAt(transform.forward + transform.position);

        yield return null;

        BowShotArrow(GetShotDirection);
    }

    /// <summary>
    /// �C���v�b�g�̐ݒ�A����͂ށA�����̑����ݒ肷��
    /// �ŏ��ɌĂ΂��
    /// </summary>
    protected abstract void SetInputDelegate();

    /// <summary>
    /// �|�̈������p���[(%)���擾
    /// ����͂ݒ��ɌĂ΂ꑱ����
    /// </summary>
    /// <returns></returns>
    protected abstract float GetShotPercentPower();

    public void SetArrowMachineGun(int arrowValue, float delayTime)
    {
        float _rapidDirayCoefficient = default;
        float _rapidDirayCoefficient_default = 1f;
        float _rapidDirayDownCoefficient = 0.05f;
        float _rapidDirayMinimun = 0.2f;
        int _defaultArrowValue = 3;
        _canMachineGun = true;
        _valueMachineGun = arrowValue;
        _rapidDirayCoefficient = _rapidDirayCoefficient_default - _rapidDirayDownCoefficient * (arrowValue * _defaultArrowValue);
        if(_rapidDirayCoefficient < _rapidDirayMinimun)
        {
            _rapidDirayCoefficient = _rapidDirayMinimun;
        }
        _delayTime = new WaitForSeconds(delayTime * _rapidDirayCoefficient);
        _playerManager.CanRapid = true;
    }

}
