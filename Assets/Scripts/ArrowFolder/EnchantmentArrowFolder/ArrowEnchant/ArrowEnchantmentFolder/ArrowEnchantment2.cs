// --------------------------------------------------------- 
// ArrowEnchantment.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System;
using UnityEngine;

//プラスダメージがまだない

interface IArrowEnchantSet:IArrowEnchantReset
{
    void EnchantMixSetting(EnchantmentEnum.EnchantmentState enchantmentState);
}
interface IArrowEnchantPlusSet:IArrowEnchantReset
{
    void EnchantSetting(EnchantmentEnum.EnchantmentState enchantmentState);
}

interface IArrowEventSet:IArrowEnchantPlusSet,IArrowEnchantSet
{
    void EventSetting(IArrowEnchant arrow);
}

interface IArrowEnchantReset
{
    void EnchantmentReset();
}

/// <summary>
/// 矢のエンチャントの組み合わせを作るクラス
/// </summary>
public sealed class ArrowEnchantment2 : MonoBehaviour, IArrowEnchantSet, IArrowEnchantPlusSet, IArrowEventSet
{


    private EnchantMix _enchantMix = new EnchantMix();

    private EnchantEventParameter _enchantEventParameter;

    private EnchantEventParameter.EnchantEvents _enchantEvents;

    //前回のエンチャント
    private EnchantmentEnum.EnchantmentState _enchantmentStateLast = EnchantmentEnum.EnchantmentState.normal;



    private void Start()
    {

        //矢の効果　エフェクト　サウンドを取得する
        _enchantEvents._arrowEnchant = this.GetComponent<ArrowEnchant>();
        _enchantEvents._arrowEnchantEffect = this.GetComponent<ArrowEnchantEffect>();
        _enchantEvents._arrowEnchantSound = this.GetComponent<ArrowEnchantSound>();
        _enchantEvents._arrowEnchantUI = this.GetComponent<ArrowEnchantUI>();
        _enchantEvents._atractEffect = this.GetComponent<AttractEffect>();

        _enchantEventParameter = new EnchantEventParameter(_enchantEvents);

    }

    public void EnchantMixSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        _enchantmentStateLast = _enchantMix.EnchantmentStateSetting(enchantmentState);
    }

    public void EnchantSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        _enchantmentStateLast = enchantmentState;
    }

    /// <summary>
    /// エンチャントを決定する
    /// </summary>
    /// <param name="arrow"></param>
    public void EventSetting(IArrowEnchant arrow)
    {
        _enchantEventParameter.EnchantEventAttribute(_enchantmentStateLast, arrow);
    }


    public void EnchantmentReset()
    {
        _enchantMix.EnchantmentStateReset();
        _enchantmentStateLast = EnchantmentEnum.EnchantmentState.nothing;
        _enchantEventParameter.EnchantEventReset();
    }


}
