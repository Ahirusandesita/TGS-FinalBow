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

interface IFPlayerManagerShotArrow
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
public class PlayerManager : MonoBehaviour, IFPlayerManagerEnchantParameter, IFPlayerManagerSetArrow,IFPlayerManagerShotArrow
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

    IArrowEventSetting arrowEnchant;

    RapidParam a;
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
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.bomb);
            SetEnchantParameter(EnchantmentEnum.ItemAttributeState.thunder);
        }
    }

    private void Start()
    {
        try
        {
            _bowObject = GameObject.FindWithTag(InhallLibTags.BowController);
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
            arrowEnchant = _arrowEnchantObject.GetComponent<ArrowEnchantment>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("ArrowEnchantControllerタグのオブジェクトにArrowEnchantmentクラスがアタッチされていません");
        }


    }

    /// <summary>
    /// エンチャントを付けるパラメーター
    /// </summary>
    /// <param name="ItemAttributeState"></param>
    public void SetEnchantParameter(EnchantmentEnum.ItemAttributeState ItemAttributeState)
    {
        arrowEnchant.EventSetting(_arrow, true, (EnchantmentEnum.EnchantmentState)ItemAttributeState);
    }
    public void ArrowEnchantPlusDamage()
    {
        arrowEnchant.ArrowEnchantPlusDamage();

        //チャージ画像
        _chargeMeterManager.Charging();

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
        if (arrowEnchant.GetSubEnchantment() != EnchantmentEnum.EnchantmentState.nothing)
        {
            _rapidSubEnchantment = arrowEnchant.GetSubEnchantment();
            int index = default;
            if (attractCount > 5)
            {
                index = _rapidData.rapids.rapidParams.Count - 1;
            }
            else
            {
                for (int i = 0; i < _rapidData.rapids.rapidParams.Count; i++)
                {
                    RapidParam.RapidArrowIndex rapidCheckPoint = _rapidData.rapids.rapidParams[i];
                    if (rapidCheckPoint.rapidCheckPoint == attractCount)
                    {
                        index = i;
                        break;
                    }
                }
            }

            //Bowの連射に数を渡す
        }

        if (CanRapid)
        {
            arrowEnchant.EventSetting(_arrow, true, (_rapidSubEnchantment));
        }

        arrowEnchant.EventSetting(_arrow, true, (EnchantmentEnum.EnchantmentState.normal));
        _arrow.gameObject.transform.rotation = _bowObject.transform.rotation;
        _arrow.ArrowMoveStart();
        arrowEnchant.EnchantmentStateReset();
        arrowEnchant.EnchantUIReset();
        attractCount = 0;
        //チャージ画像リセット
        _chargeMeterManager.ChargeReset();

    }
    public void ResetArrow()
    {
        arrowEnchant.EnchantmentStateReset();
        arrowEnchant.EnchantUIReset();
        //チャージ画像リセット
        _chargeMeterManager.ChargeReset();
        _arrow.ReturnQue();
    }



    /// <summary>
    /// 矢のクラスを取得する
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
