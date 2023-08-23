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

    #region 変数宣言部
 

    private int attractCount = 0;

    private Arrow _arrow;

    private GameObject _bowObject;

    //デバック用テストObject
    private GameObject _arrowEnchantObject;

    private IChargeMeterManager _chargeMeterManager;

    private EnchantmentEnum.EnchantmentState _rapidSubEnchantment = default;

    //public GameObject testArrowObject;

    //IArrowEventSetting arrowEnchant;

    IFBowManagerQue _bowManagerQue;

    /// <summary>
    /// デバック用
    /// </summary>
    private ClickInput _clickInput = new ClickInput();
    #endregion

    private void Update()
    {
        //テスト用
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
            Debug.LogError("弓オブジェクトが見つかりませんでした。");
        }
        try
        {
            _arrowEnchantObject = GameObject.FindWithTag(InhallLibTags.ArrowEnchantmentController);
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("ArrowEnchantControllerタグのオブジェクトが見つかりませんでした。");
        }
        try
        {
            _chargeMeterManager = GameObject.FindWithTag(InhallLibTags.ChargeMeterController).GetComponent<ChargeMeterManager>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("チャージメータオブジェクト又は、ChargeMeterManagerクラスが見つかりませんでした。");
        }
        try
        {
            //arrowEnchant = _arrowEnchantObject.GetComponent<ArrowEnchantment>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("ArrowEnchantControllerタグのオブジェクトにArrowEnchantmentクラスがアタッチされていません");
        }


    }

  
    public void SetEnchant(EnchantmentEnum.ItemAttributeState enchantment)
    {
        SetEnchantParameter(enchantment);
    }
}

