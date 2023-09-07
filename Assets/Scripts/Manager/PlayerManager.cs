// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
public interface IFPlayerManagerEnchantParameter
{
    /// <summary>
    /// ��ɃG���`�����g���Z�b�g����֐��@Enum�ɂ���ĕω�����
    /// </summary>
    /// <param name="enchantmentState"></param>
    void SetEnchantParameter(EnchantmentEnum.ItemAttributeState enchantmentState);
    void ArrowEnchantPlusDamage();
}
public interface IFPlayerManagerSetArrow
{
    void SetArrow(Arrow arrow);

}

public interface IFPlayerManagerShotArrow : IFPlayerManagerRapid
{
    void ShotArrow(Vector3 aim);
    void ResetArrow();

    void SetArrowMoveSpeed(float speed);
}

public interface IFPlayerManagerRapid
{
    bool CanRapid { get; set; }
}

public interface IFPlayerManagerHave : IFPlayerManagerRapid
{
    Arrow GetOnlyArrow { get; }
}



//�H
interface IFPlayerManager
{
    /// <summary>
    /// �z�����񂾂��̂�Arrow��Event��ǉ�����
    /// </summary>
    /// <param name="obj">�z�����񂾂���</param>
    public void AddEventArrow(GameObject obj);

}
public class PlayerManager : MonoBehaviour, IFPlayerManagerEnchantParameter, IFPlayerManagerSetArrow, IFPlayerManagerShotArrow, IFPlayerManagerHave
{
    #region �ϐ��錾��
    public bool CanRapid { get; set; }

    //public static bool AddTag = false;
    public RapidData _rapidData;
    [SerializeField]
    private int maxAttractCount = default;

    private int attractCount = 0;

    private Arrow _arrow;

    private GameObject _bowObject;

    //�f�o�b�N�p�e�X�gObject
    private GameObject _arrowEnchantObject;

    private IChargeMeterManager _chargeMeterManager;
    private GameObject chargeMater;

    private EnchantmentEnum.EnchantmentState _rapidSubEnchantment = default;


    private float rapidRandomAngle = 0f;

    //public GameObject testArrowObject;

    //IArrowEventSetting arrowEnchant;

    IArrowEventSet arrowEnchant2;


    IFBowManagerQue _bowManagerQue;


    /// <summary>
    /// �f�o�b�N�p
    /// </summary>
    private ClickInput _clickInput = new ClickInput();

    public Arrow GetOnlyArrow
    {

        get
        {
            return _arrow;
        }
    }
    #endregion

    private void Update()
    {
        //�e�X�g�p
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.knockBack);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.thunder);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.bomb);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.penetrate);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.homing);
        }


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ArrowEnchantPlusDamage();
        }
        //arrowEnchant.TestRapid = CanRapid ;

    }

    private void Start()
    {
        try
        {
            _bowObject = GameObject.FindWithTag(InhallLibTags.BowController);
            _bowManagerQue = _bowObject.GetComponent<BowManager>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("�|�I�u�W�F�N�g��������܂���ł����B");
        }
        try
        {
            _arrowEnchantObject = GameObject.FindWithTag(InhallLibTags.ArrowEnchantmentController);
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("ArrowEnchantController�^�O�̃I�u�W�F�N�g��������܂���ł����B");
        }
        try
        {
            _chargeMeterManager = GameObject.FindWithTag(InhallLibTags.ChargeMeterController).GetComponent<ChargeMeterManager>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("�`���[�W���[�^�I�u�W�F�N�g���́AChargeMeterManager�N���X��������܂���ł����B");
        }
        try
        {
            //arrowEnchant = _arrowEnchantObject.GetComponent<ArrowEnchantment>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("ArrowEnchantController�^�O�̃I�u�W�F�N�g��ArrowEnchantment�N���X���A�^�b�`����Ă��܂���");
        }
        try
        {
            arrowEnchant2 = GameObject.FindWithTag(InhallLibTags.ArrowEnchantmentController).GetComponent<ArrowEnchantment>();
            arrowEnchant2._playerManager = this;
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("Hello�[");
        }

    }

    /// <summary>
    /// �G���`�����g��t����p�����[�^�[
    /// </summary>
    /// <param name="ItemAttributeState"></param>
    public void SetEnchantParameter(EnchantmentEnum.ItemAttributeState ItemAttributeState)
    {
        //arrowEnchant.EventSetting(_arrow, true, (EnchantmentEnum.EnchantmentState)ItemAttributeState);
        arrowEnchant2.EnchantMixSetting((EnchantmentEnum.EnchantmentState)ItemAttributeState);
    }
    public void ArrowEnchantPlusDamage()
    {

        if (attractCount >= maxAttractCount)
        {
            attractCount = maxAttractCount;
            return;
        }
        arrowEnchant2.ArrowEnchantPlusDamage();
        _arrow.GetPassiveEffect().SetAttackDamage();
        _arrow.SetAttackDamage();

        //�`���[�W�摜
        if (_chargeMeterManager != null) _chargeMeterManager.Charging();

        _arrow.ArrowPowerColor();

        attractCount++;

    }
    /// <summary>
    /// ��𔭎˂��郁�\�b�h
    /// </summary>
    /// <param name="aim"></param>
    public void ShotArrow(Vector3 aim)
    {
        arrowEnchant2.ArrowEnchantDamage(_arrow.Damage);
        Quaternion arrowRotation = default;
        //�A��
        if (arrowEnchant2.GetSubEnchantment() != EnchantmentEnum.EnchantmentState.nothing && !CanRapid)
        {
            
            _rapidSubEnchantment = arrowEnchant2.GetSubEnchantment();
            int index = default;
            if (attractCount > _rapidData.rapids.rapidParams[_rapidData.rapids.rapidParams.Count - 1].rapidCheckPoint)
            {
                index = _rapidData.rapids.rapidParams.Count - 1;
            }
            else
            {
                for (int i = 0; i < _rapidData.rapids.rapidParams.Count; i++)
                {
                    RapidParam.RapidArrowIndex rapidCheckPoint = _rapidData.rapids.rapidParams[i];
                    if (rapidCheckPoint.rapidCheckPoint < attractCount)
                    {
                        index = i;
                        rapidRandomAngle += (float)index;
                        rapidRandomAngle /= 3f;
                       
                    }
                }
            }
            _bowManagerQue.SetArrowMachineGun(_rapidData.rapids.rapidParams[index].rapidIndex, _rapidData.rapidParam.rapidLate);
        }
        if (CanRapid)
        {
            float x = Random.Range(-rapidRandomAngle, rapidRandomAngle);
            float y = Random.Range(-rapidRandomAngle, rapidRandomAngle);
            float z = Random.Range(-rapidRandomAngle, rapidRandomAngle);
            Vector3 bowAngle = _bowObject.transform.rotation.eulerAngles;
            bowAngle += new Vector3(x, y, z);
            arrowRotation = Quaternion.Euler(bowAngle);
            arrowEnchant2.EnchantSetting((_rapidSubEnchantment));
        }
        else
        {
            arrowRotation =  _bowObject.transform.rotation;
        }

        //arrowEnchant.EventSetting(_arrow, true, (EnchantmentEnum.EnchantmentState.normal));
        arrowEnchant2.EnchantMixSetting((EnchantmentEnum.EnchantmentState.normal));

        arrowEnchant2.EventSetting(_arrow);

        _arrow.gameObject.transform.rotation = arrowRotation;

        _arrow.ArrowMoveStart();
        //arrowEnchant.EnchantmentStateReset();

        arrowEnchant2.EnchantmentReset();




        //arrowEnchant.EnchantUIReset();
        attractCount = 0;
        //�`���[�W�摜���Z�b�g
        if (_chargeMeterManager != null)
        {
            _chargeMeterManager.ChargeReset();
        }


        _arrow = default;
    }
    public void ResetArrow()
    {
        //arrowEnchant.EnchantmentStateReset();


        arrowEnchant2.EnchantmentReset();



        if (_chargeMeterManager != null)
        {
            //arrowEnchant.EnchantUIReset();
            //�`���[�W�摜���Z�b�g
            _chargeMeterManager.ChargeReset();
        }
        _arrow.ReturnQue();
    }



    /// <summary>
    /// ��̃N���X���擾����
    /// </summary>
    /// <param name="arrowObj"></param>
    public void SetArrow(Arrow arrow)
    {
        //if (arrowEnchant == null)
        //{
        //    return;
        //}
        _arrow = arrow;
        if (arrowEnchant2 == null)
        {
            return;
        }
        //arrowEnchant.EventSetting(_arrow, true, EnchantmentEnum.EnchantmentState.normal);
        arrowEnchant2.EnchantMixSetting(EnchantmentEnum.EnchantmentState.normal);
        arrowEnchant2.EventSetting(_arrow);
    }



    public void SetArrowMoveSpeed(float arrowMoveSpeed)
    {
        _arrow.SetArrowMoveSpeed(arrowMoveSpeed);
    }
}
