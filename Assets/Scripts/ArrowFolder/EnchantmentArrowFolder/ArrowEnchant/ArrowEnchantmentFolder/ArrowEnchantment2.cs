// --------------------------------------------------------- 
// ArrowEnchantment.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System;
using UnityEngine;

//プラスダメージがまだない

public interface IArrowEnchantLevelable<T>
{
    public delegate void EnchantDelegate(T t);
    EnchantDelegate EnchantLevel(EnchantmentEnum.EnchantmentState enchantmentState);
}
public interface IArrowEnchantLevelable<T1, T2>
{
    public delegate void EnchantDelegate(T1 t1, T2 t2);
    EnchantDelegate EnchantLevel(EnchantmentEnum.EnchantmentState enchantmentState);
}

public interface IArrowEnchantable
{
    void EnchantLevel(EnchantmentEnum.EnchantmentState enchantmentState)
    {

        switch (enchantmentState)
        {
            case EnchantmentEnum.EnchantmentState.normal:
                Normal();
                break;
            case EnchantmentEnum.EnchantmentState.bomb:
                Bomb();
                break;
            case EnchantmentEnum.EnchantmentState.thunder:
                Thunder();
                break;
            case EnchantmentEnum.EnchantmentState.knockBack:
                KnockBack();
                break;
            case EnchantmentEnum.EnchantmentState.penetrate:
                Penetrate();
                break;
            case EnchantmentEnum.EnchantmentState.homing:
                Homing();
                break;
            case EnchantmentEnum.EnchantmentState.bombThunder:
                BombThunder();
                break;
            case EnchantmentEnum.EnchantmentState.bombKnockBack:
                BombKnockBack();
                break;
            case EnchantmentEnum.EnchantmentState.bombPenetrate:
                BombPenetrate();
                break;
            case EnchantmentEnum.EnchantmentState.bombHoming:
                BombHoming();
                break;
            case EnchantmentEnum.EnchantmentState.thunderKnockBack:
                ThunderKnockBack();
                break;
            case EnchantmentEnum.EnchantmentState.thunderPenetrate:
                ThunderPenetrate();
                break;
            case EnchantmentEnum.EnchantmentState.thunderHoming:
                ThunderHoming();
                break;
            case EnchantmentEnum.EnchantmentState.knockBackpenetrate:
                KnockBackPenetrate();
                break;
            case EnchantmentEnum.EnchantmentState.knockBackHoming:
                KnockBackHoming();
                break;
            case EnchantmentEnum.EnchantmentState.homingPenetrate:
                PenetrateHoming();
                break;
        }

    }

    void Normal();
    void Bomb();
    void Thunder();
    void KnockBack();
    void Penetrate();
    void Homing();
    void BombThunder();
    void BombKnockBack();
    void BombPenetrate();
    void BombHoming();
    void ThunderKnockBack();
    void ThunderPenetrate();
    void ThunderHoming();
    void KnockBackPenetrate();
    void KnockBackHoming();
    void PenetrateHoming();

}

public interface IArrowEnchantable<T>
{
    public delegate void EnchantDelegate(T t);
    public EnchantDelegate EnchantLevel(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        switch (enchantmentState)
        {
            case EnchantmentEnum.EnchantmentState.normal:
                return new EnchantDelegate(Normal);
            case EnchantmentEnum.EnchantmentState.bomb:
                return new EnchantDelegate(Bomb);
            case EnchantmentEnum.EnchantmentState.thunder:
                return new EnchantDelegate(Thunder);
            case EnchantmentEnum.EnchantmentState.knockBack:
                return new EnchantDelegate(KnockBack);
            case EnchantmentEnum.EnchantmentState.penetrate:
                return new EnchantDelegate(Penetrate);
            case EnchantmentEnum.EnchantmentState.homing:
                return new EnchantDelegate(Homing);
            case EnchantmentEnum.EnchantmentState.bombThunder:
                return new EnchantDelegate(BombThunder);
            case EnchantmentEnum.EnchantmentState.bombKnockBack:
                return new EnchantDelegate(BombKnockBack);
            case EnchantmentEnum.EnchantmentState.bombPenetrate:
                return new EnchantDelegate(BombPenetrate);
            case EnchantmentEnum.EnchantmentState.bombHoming:
                return new EnchantDelegate(BombHoming);
            case EnchantmentEnum.EnchantmentState.thunderKnockBack:
                return new EnchantDelegate(ThunderKnockBack);
            case EnchantmentEnum.EnchantmentState.thunderPenetrate:
                return new EnchantDelegate(ThunderPenetrate);
            case EnchantmentEnum.EnchantmentState.thunderHoming:
                return new EnchantDelegate(ThunderHoming);
            case EnchantmentEnum.EnchantmentState.knockBackpenetrate:
                return new EnchantDelegate(KnockBackPenetrate);
            case EnchantmentEnum.EnchantmentState.knockBackHoming:
                return new EnchantDelegate(KnockBackHoming);
            case EnchantmentEnum.EnchantmentState.homingPenetrate:
                return new EnchantDelegate(PenetrateHoming);
            case EnchantmentEnum.EnchantmentState.nothing:
                Debug.LogError("Nothing!");
                break;
        }
        return null;
    }

    void Normal(T t);
    void Bomb(T t);
    void Thunder(T t);
    void KnockBack(T t);
    void Penetrate(T t);
    void Homing(T t);
    void BombThunder(T t);
    void BombKnockBack(T t);
    void BombPenetrate(T t);
    void BombHoming(T t);
    void ThunderKnockBack(T t);
    void ThunderPenetrate(T t);
    void ThunderHoming(T t);
    void KnockBackPenetrate(T t);
    void KnockBackHoming(T t);
    void PenetrateHoming(T t);
}
public interface IArrowEnchantable<T1, T2>
{

    public delegate void EnchantDelegate(T1 t, T2 t2);
    public EnchantDelegate EnchantLevel(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        switch (enchantmentState)
        {
            case EnchantmentEnum.EnchantmentState.normal:
                return new EnchantDelegate(Normal);
            case EnchantmentEnum.EnchantmentState.bomb:
                return new EnchantDelegate(Bomb);
            case EnchantmentEnum.EnchantmentState.thunder:
                return new EnchantDelegate(Thunder);
            case EnchantmentEnum.EnchantmentState.knockBack:
                return new EnchantDelegate(KnockBack);
            case EnchantmentEnum.EnchantmentState.penetrate:
                return new EnchantDelegate(Penetrate);
            case EnchantmentEnum.EnchantmentState.homing:
                return new EnchantDelegate(Homing);
            case EnchantmentEnum.EnchantmentState.bombThunder:
                return new EnchantDelegate(BombThunder);
            case EnchantmentEnum.EnchantmentState.bombKnockBack:
                return new EnchantDelegate(BombKnockBack);
            case EnchantmentEnum.EnchantmentState.bombPenetrate:
                return new EnchantDelegate(BombPenetrate);
            case EnchantmentEnum.EnchantmentState.bombHoming:
                return new EnchantDelegate(BombHoming);
            case EnchantmentEnum.EnchantmentState.thunderKnockBack:
                return new EnchantDelegate(ThunderKnockBack);
            case EnchantmentEnum.EnchantmentState.thunderPenetrate:
                return new EnchantDelegate(ThunderPenetrate);
            case EnchantmentEnum.EnchantmentState.thunderHoming:
                return new EnchantDelegate(ThunderHoming);
            case EnchantmentEnum.EnchantmentState.knockBackpenetrate:
                return new EnchantDelegate(KnockBackPenetrate);
            case EnchantmentEnum.EnchantmentState.knockBackHoming:
                return new EnchantDelegate(KnockBackHoming);
            case EnchantmentEnum.EnchantmentState.homingPenetrate:
                return new EnchantDelegate(PenetrateHoming);
            case EnchantmentEnum.EnchantmentState.nothing:
                Debug.LogError("Nothing!");
                break;
        }
        return null;
    }
    void Normal(T1 t1, T2 t2);
    void Bomb(T1 t1, T2 t2);
    void Thunder(T1 t1, T2 t2);
    void KnockBack(T1 t1, T2 t2);
    void Penetrate(T1 t1, T2 t2);
    void Homing(T1 t1, T2 t2);
    void BombThunder(T1 t1, T2 t2);
    void BombKnockBack(T1 t1, T2 t2);
    void BombPenetrate(T1 t1, T2 t2);
    void BombHoming(T1 t1, T2 t2);
    void ThunderKnockBack(T1 t1, T2 t2);
    void ThunderPenetrate(T1 t1, T2 t2);
    void ThunderHoming(T1 t1, T2 t2);
    void KnockBackPenetrate(T1 t1, T2 t2);
    void KnockBackHoming(T1 t1, T2 t2);
    void PenetrateHoming(T1 t1, T2 t2);
}


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

interface IArrowEventSet : IArrowEnchantPlusSet, IArrowEnchantSet, IArrowPlusDamage
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

    IFPlayerManagerHave _playerManager { get; set; }
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
public sealed class ArrowEnchantment2 : MonoBehaviour, IArrowEnchantSet, IArrowEnchantPlusSet, IArrowEventSet, IArrowPlusDamage
{

    public IFPlayerManagerHave _playerManager { get; set; }

    private EnchantMix _enchantMix = new EnchantMix();

    private EnchantEventParameter _enchantEventParameter;

    private EnchantEventParameter.EnchantEvents _enchantEvents;

    //前回のエンチャント
    private EnchantmentEnum.EnchantmentState _enchantmentStateNow = EnchantmentEnum.EnchantmentState.normal;

    private EnchantmentEnum.EnchantmentState _enchantmentStateLast = default;


    private delegate void EnchantStatePreparation();

    private IArrowSound arrowSound;

    private IArrowEnchantDamageable arrowEnchant;

    private bool canMix = true;
    private void Start()
    {

        //矢の効果　エフェクト　サウンドを取得する

        _enchantEvents._arrowEnchantUI = this.GetComponent<ArrowEnchantUI>();
        _enchantEvents._atractEffect = this.GetComponent<AttractEffect>();

        arrowSound = this.GetComponent<ArrowEnchantSound>();

        _enchantEvents._arrowEnchantSound2 = this.GetComponent<ArrowEnchantSound>();
        arrowEnchant = this.GetComponent<ArrowEnchant>();
        _enchantEvents._arrowEnchant2 = this.GetComponent<ArrowEnchant>();
        _enchantEvents._arrowEnchantEffect2 = this.GetComponent<ArrowEnchantEffect>();

        _enchantEventParameter = new EnchantEventParameter(_enchantEvents);




    }

    /// <summary>
    /// エンチャントをミックスする
    /// </summary>
    /// <param name="enchantmentState"></param>
    public void EnchantMixSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        if (!canMix) return;

        EnchantDecision(
            new EnchantStatePreparation(
                () =>
                { 
                    _enchantmentStateNow = _enchantMix.EnchantmentStateSetting(enchantmentState);
                }
                ));
    }

    public void EnchantSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        EnchantDecision(
            new EnchantStatePreparation(
                () =>
                { _enchantmentStateNow = enchantmentState;
                    canMix = false;
                }));
    }


    private void EnchantDecision(EnchantStatePreparation enchantStatePreparation)
    {

        enchantStatePreparation();
        _enchantmentStateLast = _enchantmentStateNow;
        if (_playerManager.GetOnlyArrow != default)
        {
            EventSetting(_playerManager.GetOnlyArrow);
        }

        if (_playerManager.CanRapid)
        {
            return;
        }
        NewEnchantState();



    }


    public void EnchantRapidSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {

        EnchantDecision(
            new EnchantStatePreparation(
                () =>
                { _enchantmentStateNow = enchantmentState; }));
    }


    private void NewEnchantState()
    {
        if (_enchantmentStateNow != _enchantmentStateLast && _enchantmentStateNow != EnchantmentEnum.EnchantmentState.normal)
        {
            arrowSound.ArrowSound_EnchantSound();
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
        if (_enchantEventParameter == null)
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
        canMix = true;
    }

    public void ArrowEnchantPlusDamage()
    {
        arrowEnchant.SetAttackDamage();

        //_enchantEvents._arrowEnchantPassiveEffect

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
