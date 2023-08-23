// --------------------------------------------------------- 
// FakePlayerManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class FakePlayerManager : PlayerManager
{

    #region �ϐ��錾��
 

    private int attractCount = 0;

    private Arrow _arrow;

    private GameObject _bowObject;

    //�f�o�b�N�p�e�X�gObject
    private GameObject _arrowEnchantObject;

    private IChargeMeterManager _chargeMeterManager;

    private EnchantmentEnum.EnchantmentState _rapidSubEnchantment = default;

    //public GameObject testArrowObject;

    //IArrowEventSetting arrowEnchant;

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

        //arrowEnchant.TestRapid = CanRapid;

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


    }

  
    public void SetEnchant(EnchantmentEnum.ItemAttributeState enchantment)
    {
        SetEnchantParameter(enchantment);
    }
}

