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
    /// 矢にエンチャントをセットする関数　Enumによって変化する
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



//？
interface IFPlayerManager
{
    /// <summary>
    /// 吸い込んだものでArrowにEventを追加する
    /// </summary>
    /// <param name="obj">吸い込んだもの</param>
    public void AddEventArrow(GameObject obj);

}
public class PlayerManager : MonoBehaviour, IFPlayerManagerEnchantParameter, IFPlayerManagerSetArrow, IFPlayerManagerShotArrow
{
    #region 変数宣言部
    public bool CanRapid { get; set; }

    //public static bool AddTag = false;
    public RapidData _rapidData;

    private int attractCount = 0;

    private Arrow _arrow;

    private GameObject _bowObject;

    //デバック用テストObject
    private GameObject _arrowEnchantObject;

    private IChargeMeterManager _chargeMeterManager;

    private EnchantmentEnum.EnchantmentState _rapidSubEnchantment = default;

    //public GameObject testArrowObject;

    //IArrowEventSetting arrowEnchant;

    IArrowEventSet arrowEnchant2;


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
        try
        {
            arrowEnchant2 = GameObject.FindWithTag(InhallLibTags.ArrowEnchantmentController).GetComponent<ArrowEnchantment2>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("Helloー");
        }

    }

    /// <summary>
    /// エンチャントを付けるパラメーター
    /// </summary>
    /// <param name="ItemAttributeState"></param>
    public void SetEnchantParameter(EnchantmentEnum.ItemAttributeState ItemAttributeState)
    {
        //arrowEnchant.EventSetting(_arrow, true, (EnchantmentEnum.EnchantmentState)ItemAttributeState);
        arrowEnchant2.EnchantMixSetting((EnchantmentEnum.EnchantmentState)ItemAttributeState);
    }
    public void ArrowEnchantPlusDamage()
    {
        //arrowEnchant.ArrowEnchantPlusDamage();


        //チャージ画像
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
    /// 矢を発射するメソッド
    /// </summary>
    /// <param name="aim"></param>
    public void ShotArrow(Vector3 aim)
    {
        //連射
        //if (arrowEnchant.GetSubEnchantment() != EnchantmentEnum.EnchantmentState.nothing)
        //{
        //    _rapidSubEnchantment = arrowEnchant.GetSubEnchantment();
        //    int index = default;
        //    if (attractCount > _rapidData.rapids.rapidParams[_rapidData.rapids.rapidParams.Count - 1].rapidCheckPoint)
        //    {
        //        index = _rapidData.rapids.rapidParams.Count - 1;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < _rapidData.rapids.rapidParams.Count; i++)
        //        {
        //            RapidParam.RapidArrowIndex rapidCheckPoint = _rapidData.rapids.rapidParams[i];
        //            if (rapidCheckPoint.rapidCheckPoint < attractCount)
        //            {
        //                index = i;
        //            }
        //        }
        //    }
        //    _bowManagerQue.SetArrowMachineGun(_rapidData.rapids.rapidParams[index].rapidIndex, _rapidData.rapidParam.rapidLate);
        //}

        //if (CanRapid)
        //{
        //    arrowEnchant.EventSetting(_arrow, true, (_rapidSubEnchantment));
        //}

        //arrowEnchant.EventSetting(_arrow, true, (EnchantmentEnum.EnchantmentState.normal));
        arrowEnchant2.EnchantMixSetting((EnchantmentEnum.EnchantmentState.normal));
        arrowEnchant2.EventSetting(_arrow);


        _arrow.gameObject.transform.rotation = _bowObject.transform.rotation;
        _arrow.ArrowMoveStart();
        //arrowEnchant.EnchantmentStateReset();
        try
        {
            arrowEnchant2.EnchantmentReset();
        }
        catch
        {

        }


        try
        {
            //arrowEnchant.EnchantUIReset();
            attractCount = 0;
            //チャージ画像リセット
            _chargeMeterManager.ChargeReset();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("UInai");
        }

    }
    public void ResetArrow()
    {
        //arrowEnchant.EnchantmentStateReset();

        try
        {
            arrowEnchant2.EnchantmentReset();
        }
        catch
        {

        }


        try
        {
            //arrowEnchant.EnchantUIReset();
            //チャージ画像リセット
            _chargeMeterManager.ChargeReset();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("UInai");
        }
        _arrow.ReturnQue();
    }



    /// <summary>
    /// 矢のクラスを取得する
    /// </summary>
    /// <param name="arrowObj"></param>
    public void SetArrow(Arrow arrow)
    {
        //if (arrowEnchant == null)
        //{
        //    return;
        //}
        _arrow = arrow;
        //arrowEnchant.EventSetting(_arrow, true, EnchantmentEnum.EnchantmentState.normal);
        arrowEnchant2.EnchantMixSetting(EnchantmentEnum.EnchantmentState.normal);
        arrowEnchant2.EventSetting(_arrow);
    }



    public void SetArrowMoveSpeed(float arrowMoveSpeed)
    {
        _arrow.SetArrowMoveSpeed(arrowMoveSpeed);
    }
}
