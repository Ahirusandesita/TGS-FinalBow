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

interface IFPlayerManagerShotArrow
{
    void ShotArrow(Vector3 aim);
    void ResetArrow();
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
    //public static bool AddTag = false;
    public TagObject _BowControllerTagData;

    public TagObject _ArrowEnchantmentControllerTagData;

    public TagObject _ChargeMeterControllerTagData;

    private Arrow _arrow;

    private GameObject _bowObject;

    //�f�o�b�N�p�e�X�gObject
    private GameObject _arrowEnchantObject;

    private IChargeMeterManager _chargeMeterManager;




    //public GameObject testArrowObject;

    IArrowEventSetting arrowEnchant;
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
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.bomb);
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.thunder);
        }
    }

    private void Start()
    {

        try
        {
            _bowObject = GameObject.FindWithTag(_BowControllerTagData.TagName);
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("�|�I�u�W�F�N�g��������܂���ł����B");
        }
        try
        {
            _arrowEnchantObject = GameObject.FindWithTag(_ArrowEnchantmentControllerTagData.TagName);
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("ArrowEnchantController�^�O�̃I�u�W�F�N�g��������܂���ł����B");
        }
        try
        {
            _chargeMeterManager = GameObject.FindWithTag(_ChargeMeterControllerTagData.TagName).GetComponent<ChargeMeterManager>();
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
        _chargeMeterManager.Charging();

        _arrow.ArrowPowerColor();
    }
    /// <summary>
    /// ��𔭎˂��郁�\�b�h
    /// </summary>
    /// <param name="aim"></param>
    public void ShotArrow(Vector3 aim)
    {
        arrowEnchant.EventSetting(_arrow, true, (EnchantmentEnum.EnchantmentState.normal));
        _arrow.gameObject.transform.rotation = _bowObject.transform.rotation;
        _arrow.ArrowMoveStart();
        arrowEnchant.EnchantmentStateReset();
        arrowEnchant.EnchantUIReset();

        //�`���[�W�摜���Z�b�g
        _chargeMeterManager.ChargeReset();
    }
    public void ResetArrow()
    {
        arrowEnchant.EnchantmentStateReset();
        arrowEnchant.EnchantUIReset();
        //�`���[�W�摜���Z�b�g
        _chargeMeterManager.ChargeReset();
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
