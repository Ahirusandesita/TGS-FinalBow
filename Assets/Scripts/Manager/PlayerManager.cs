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

public interface IFPlayerManagerShotArrow
{
    void ShotArrow(Vector3 aim);
    void ResetArrow();

    void SetArrowMoveSpeed(float speed);
    bool CanRapid { get; set; }
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
public class PlayerManager : MonoBehaviour, IFPlayerManagerEnchantParameter, IFPlayerManagerSetArrow,IFPlayerManagerShotArrow
{
    #region �ϐ��錾��
    public bool CanRapid { get; set; }

    //public static bool AddTag = false;
    public RapidData _rapidData;

    private int attractCount = 0;

    private Arrow _arrow;

    private GameObject _bowObject;

    //�f�o�b�N�p�e�X�gObject
    private GameObject _arrowEnchantObject;

    private IChargeMeterManager _chargeMeterManager;

    private EnchantmentEnum.EnchantmentState _rapidSubEnchantment = default;

    //public GameObject testArrowObject;

    IArrowEventSetting arrowEnchant;

    IFBowManagerQue _bowManagerQue;

    /// <summary>
    /// �f�o�b�N�p
    /// </summary>
    private ClickInput _clickInput = new ClickInput();
    #endregion

    private void Update()
    {
        //�e�X�g�p
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.knockBack);
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.thunder);
        }
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
            arrowEnchant = _arrowEnchantObject.GetComponent<ArrowEnchantment>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("ArrowEnchantController�^�O�̃I�u�W�F�N�g��ArrowEnchantment�N���X���A�^�b�`����Ă��܂���");
        }


    }

    /// <summary>
    /// �G���`�����g��t����p�����[�^�[
    /// </summary>
    /// <param name="ItemAttributeState"></param>
    public void SetEnchantParameter(EnchantmentEnum.ItemAttributeState ItemAttributeState)
    {
        arrowEnchant.EventSetting(_arrow, true, (EnchantmentEnum.EnchantmentState)ItemAttributeState);
    }
    public void ArrowEnchantPlusDamage()
    {
        arrowEnchant.ArrowEnchantPlusDamage();

        //�`���[�W�摜
        try
        {
            _chargeMeterManager.Charging();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("UInai");
        }
        _arrow.ArrowPowerColor();

        attractCount++;

    }
    /// <summary>
    /// ��𔭎˂��郁�\�b�h
    /// </summary>
    /// <param name="aim"></param>
    public void ShotArrow(Vector3 aim)
    {
        //�A��
        if (arrowEnchant.GetSubEnchantment() != EnchantmentEnum.EnchantmentState.nothing)
        {
            _rapidSubEnchantment = arrowEnchant.GetSubEnchantment();
            int index = default;
            if (attractCount > _rapidData.rapids.rapidParams[_rapidData.rapids.rapidParams.Count-1].rapidCheckPoint)
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
                    }
                }
            }
            _bowManagerQue.SetArrowMachineGun(_rapidData.rapids.rapidParams[index].rapidIndex, _rapidData.rapidParam.rapidLate);            
        }

        if (CanRapid)
        {
            arrowEnchant.EventSetting(_arrow, true, (_rapidSubEnchantment));
        }

        arrowEnchant.EventSetting(_arrow, true, (EnchantmentEnum.EnchantmentState.normal));
        _arrow.gameObject.transform.rotation = _bowObject.transform.rotation;
        _arrow.ArrowMoveStart();
        try
        {
            arrowEnchant.EnchantmentStateReset();
            arrowEnchant.EnchantUIReset();
            attractCount = 0;
            //�`���[�W�摜���Z�b�g
            _chargeMeterManager.ChargeReset();
        }
        catch(System.NullReferenceException)
        {
            Debug.LogError("UInai");
        }

    }
    public void ResetArrow()
    {
        try
        {
            arrowEnchant.EnchantmentStateReset();
            arrowEnchant.EnchantUIReset();
            //�`���[�W�摜���Z�b�g
            _chargeMeterManager.ChargeReset();
        }
        catch(System.NullReferenceException)
        {
            Debug.LogError("UInai");
        }
        _arrow.ReturnQue();
    }



    /// <summary>
    /// ��̃N���X���擾����
    /// </summary>
    /// <param name="arrowObj"></param>
    public void SetArrow(Arrow arrow)
    {
        if (arrowEnchant == null)
        {
            return;
        }
        _arrow = arrow;
        arrowEnchant.EventSetting(_arrow, true, EnchantmentEnum.EnchantmentState.normal);
    }

   

    public void SetArrowMoveSpeed(float arrowMoveSpeed)
    {
        _arrow.SetArrowMoveSpeed(arrowMoveSpeed);
    }
}
