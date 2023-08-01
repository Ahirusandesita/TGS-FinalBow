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

interface IArrowEventSet:IArrowEnchantPlusSet,IArrowEnchantSet,IArrowPlusDamage
{
    void EventSetting(IArrowEnchant arrow);

    /// <summary>
    /// 連射エンチャントのサブエンチャントを取得
    /// </summary>
    /// <returns></returns>
    EnchantmentEnum.EnchantmentState GetSubEnchantment();

    void EnchantRapidSetting(EnchantmentEnum.EnchantmentState enchantmentState);
}

interface IArrowEnchantReset
{
    void EnchantmentReset();
}

/// <summary>
/// 矢のエンチャントの組み合わせを作るクラス
/// </summary>
public sealed class ArrowEnchantment2 : MonoBehaviour, IArrowEnchantSet, IArrowEnchantPlusSet, IArrowEventSet,IArrowPlusDamage
{


    private EnchantMix _enchantMix = new EnchantMix();

    private EnchantEventParameter _enchantEventParameter;

    private EnchantEventParameter.EnchantEvents _enchantEvents;

    //前回のエンチャント
    private EnchantmentEnum.EnchantmentState _enchantmentStateNow = EnchantmentEnum.EnchantmentState.normal;

    private EnchantmentEnum.EnchantmentState _enchantmentStateLast = default;



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
        _enchantmentStateNow = _enchantMix.EnchantmentStateSetting(enchantmentState);
        NewEnchantState();
        _enchantmentStateLast = _enchantmentStateNow;
    }

    public void EnchantSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        _enchantmentStateNow = enchantmentState;
        NewEnchantState();
        _enchantmentStateLast = _enchantmentStateNow;
    }

    public void EnchantRapidSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        _enchantmentStateNow = enchantmentState;
        _enchantmentStateLast = _enchantmentStateNow;
    }


    private void NewEnchantState()
    {
        if(_enchantmentStateNow != _enchantmentStateLast && _enchantmentStateNow != EnchantmentEnum.EnchantmentState.normal)
        {
            _enchantEvents._arrowEnchantSound.ArrowSound_EnchantSound();
            _enchantEventParameter.NewEnchantEvent(_enchantmentStateNow);
           //_arrowEnchantEffect.ArrowEffect_NewEnchantEffect(arrow.MyTransform);
        }
    }

    /// <summary>
    /// エンチャントを決定する
    /// </summary>
    /// <param name="arrow"></param>
    public void EventSetting(IArrowEnchant arrow)
    {
        if(_enchantEventParameter == null)
        {
            return;
        }
        _enchantEventParameter.EnchantEventAttribute(_enchantmentStateNow, arrow);
    }


    public void EnchantmentReset()
    {
        _enchantMix.EnchantmentStateReset();
        _enchantmentStateNow = EnchantmentEnum.EnchantmentState.nothing;
        _enchantEventParameter.EnchantEventReset();
    }

    public void ArrowEnchantPlusDamage()
    {
        _enchantEvents._arrowEnchant.SetAttackDamage();
    }

    public EnchantmentEnum.EnchantmentState GetSubEnchantment()
    {
        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.knockBack)
        {
            return EnchantmentEnum.EnchantmentState.normal;
        }

        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.bombKnockBack)
        {
            return EnchantmentEnum.EnchantmentState.bomb;
        }

        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.thunderKnockBack)
        {
            return EnchantmentEnum.EnchantmentState.thunder;
        }

        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.knockBackHoming)
        {
            return EnchantmentEnum.EnchantmentState.homing;
        }

        if (_enchantmentStateNow == EnchantmentEnum.EnchantmentState.knockBackpenetrate)
        {
            return EnchantmentEnum.EnchantmentState.penetrate;
        }

        //間違い
        return EnchantmentEnum.EnchantmentState.nothing;

    }

}
