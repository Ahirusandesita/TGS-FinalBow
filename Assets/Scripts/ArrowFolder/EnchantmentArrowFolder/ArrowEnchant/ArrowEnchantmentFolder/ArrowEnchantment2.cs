// --------------------------------------------------------- 
// ArrowEnchantment.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System;
using UnityEngine;

//プラスダメージがまだない

interface IArrowEnchantSet : IArrowEnchantReset
{

    /// <summary>
    /// エンチャントを代入する
    /// </summary>
    /// <param name="enchantmentState"></param>
    void EnchantSetting(EnchantmentEnum.EnchantmentState enchantmentState);
}
interface IArrowEnchantPlusSet : IArrowEnchantReset
{
    /// <summary>
    /// エンチャントをミックスする
    /// </summary>
    /// <param name="enchantmentState"></param>
    void EnchantMixSetting(EnchantmentEnum.EnchantmentState enchantmentState);
}

interface IArrowEventSet:IArrowEnchantPlusSet,IArrowEnchantSet,IArrowPlusDamage
{

    /// <summary>
    /// エンチャントを確定させる
    /// </summary>
    /// <param name="arrow"></param>
    void EventSetting(IArrowEnchant arrow);

    /// <summary>
    /// 連射エンチャントのサブエンチャントを取得
    /// </summary>
    /// <returns></returns>
    EnchantmentEnum.EnchantmentState GetSubEnchantment();

    /// <summary>
    /// エンチャントラピット
    /// </summary>
    /// <param name="enchantmentState"></param>
    void EnchantRapidSetting(EnchantmentEnum.EnchantmentState enchantmentState);
}

interface IArrowEnchantReset
{
    /// <summary>
    /// エンチャントをリセットする
    /// </summary>
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

    private IFPlayerManagerHave _playerManager;

    private delegate void EnchantStatePreparation();

    private void Start()
    {

        //矢の効果　エフェクト　サウンドを取得する
        _enchantEvents._arrowEnchant = this.GetComponent<ArrowEnchant>();
        _enchantEvents._arrowEnchantEffect = this.GetComponent<ArrowEnchantEffect>();
        _enchantEvents._arrowEnchantSound = this.GetComponent<ArrowEnchantSound>();
        _enchantEvents._arrowEnchantUI = this.GetComponent<ArrowEnchantUI>();
        _enchantEvents._atractEffect = this.GetComponent<AttractEffect>();

        _enchantEventParameter = new EnchantEventParameter(_enchantEvents);

        _playerManager = GameObject.FindWithTag(InhallLibTags.PlayerController).GetComponent<PlayerManager>();

        ActiveClass activeClass = new ActiveClass();
        activeClass.SetClass(this);
        activeClass.SetClass(_enchantEvents._arrowEnchant);
         PoolActiveSelect poolActiveSelect = GameObject.Find("TestActiveObjects").GetComponent<PoolActiveSelect>();
        poolActiveSelect.Enabled(activeClass);
    }

    /// <summary>
    /// エンチャントをミックスする
    /// </summary>
    /// <param name="enchantmentState"></param>
    public void EnchantMixSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        EnchantDecision(
            new EnchantStatePreparation(
                () => 
                { _enchantmentStateNow = _enchantMix.EnchantmentStateSetting(enchantmentState); }));
    }

    public void EnchantSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        EnchantDecision(
            new EnchantStatePreparation(
                () =>
                { _enchantmentStateNow = enchantmentState; }));
    }


    private void EnchantDecision(EnchantStatePreparation enchantStatePreparation)
    {
        if (_playerManager.CanRapid)
        {
            return;
        }
        enchantStatePreparation();
        NewEnchantState();
        _enchantmentStateLast = _enchantmentStateNow;

        if(_playerManager.GetOnlyArrow != default)
        {
            EventSetting(_playerManager.GetOnlyArrow);
        }

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
        _enchantmentStateLast = EnchantmentEnum.EnchantmentState.nothing;
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
