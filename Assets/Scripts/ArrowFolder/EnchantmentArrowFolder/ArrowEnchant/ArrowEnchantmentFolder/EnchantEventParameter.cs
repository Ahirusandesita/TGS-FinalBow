// 81-C# NomalScript-NewScript.cs
//
//CreateDay:
//Creator  :
//
using System;

interface IEnchantEventParameter
{

}
public class EnchantEventParameter : IEnchantEventParameter
{

    public struct EnchantEvents
    {
        public delegate void NewEnchantEffect();


        public NewEnchantEffect _newEnchantEffect;


        /// <summary>
        /// 矢の効果クラス
        /// </summary>
        public ArrowEnchant _arrowEnchant;

        /// <summary>
        /// 矢のエフェクトクラス
        /// </summary>
        public ArrowEnchantEffect _arrowEnchantEffect;

        /// <summary>
        /// 矢の常時エフェクトクラス
        /// </summary>
        public ArrowPassiveEffect _arrowEnchantPassiveEffect;

        /// <summary>
        /// 矢の効果音クラス
        /// </summary>
        public ArrowEnchantSound _arrowEnchantSound;

        /// <summary>
        /// 矢の移動クラス
        /// </summary>
        public ArrowMove arrowMove;

        /// <summary>
        /// 矢のエンチャントUIクラス
        /// </summary>
        public ArrowEnchantUI _arrowEnchantUI;

        /// <summary>
        /// 吸い込みエフェクト
        /// </summary>
        public AttractEffect _atractEffect;
    }

    private EnchantEvents _enchantEvents;

    public struct EnchantAttribute
    {
        public Arrow.ArrowEnchantmentDelegateMethod arrowEnchantmentMethod;
        public Arrow.ArrowEffectDelegateMethod arrowEffectMethod;
        public Arrow.ArrowEffectDelegateMethod arrowPassiveEffectMethod;
        public Arrow.ArrowEffectDestroyDelegateMethod arrowPassiveEffectDestroyMethod;
        public Arrow.ArrowEnchantSoundDeletgateMethod arrowEnchantSoundMethod;
        public Arrow.MoveDelegateMethod arrowMoveMethod;
        public Arrow arrow;
    }

    private EnchantAttribute _enchantAttribute;


    public EnchantEventParameter(EnchantEvents enchantEvents)
    {
        _enchantEvents = enchantEvents;
    }


    public void EnchantEventReset()
    {
        _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Normal();
        _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Normal);
        _enchantEvents._newEnchantEffect();
    }


    //エンチャントの関数をArrowに代入するためのデリゲート
    Action<
       Arrow.ArrowEnchantmentDelegateMethod,
       Arrow.ArrowEffectDelegateMethod,
       Arrow.ArrowEffectDelegateMethod,
       Arrow.ArrowEffectDestroyDelegateMethod,
       Arrow.ArrowEnchantSoundDeletgateMethod,
       Arrow.MoveDelegateMethod,
       IArrowEnchant> ArrowEnchant = (
           arrowEnchantMethod,
           arrowEffectMethod,
           arrowPassiveEffectMethod,
           arrowPassiveEffectDestroyMethod,
           arrowEnchantSoundMethod,
           arrowMoveMethod,
           arrow) =>
       {
           arrow.EventArrow = arrowEnchantMethod;
           arrow.EventArrowEffect = arrowEffectMethod;
           arrow.EventArrowPassiveEffect = arrowPassiveEffectMethod;
           arrow.EventArrowEffectPassiveDestroy = arrowPassiveEffectDestroyMethod;
           arrow.ArrowEnchantSound = arrowEnchantSoundMethod;

       };

    public void EnchantEventAttribute(EnchantmentEnum.EnchantmentState enchantState, IArrowEnchant arrow)
    {
        _enchantEvents.arrowMove = arrow.EnchantArrowMove;
        _enchantEvents._arrowEnchantPassiveEffect = arrow.EnchantArrowPassiveEffect;
        switch (enchantState)
        {
            case EnchantmentEnum.EnchantmentState.normal:
                //デリゲート
                ArrowEnchant(
                    //エンチャント処理関数代入
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_Normal),
                    //エンチャントエフェクト関数代入
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_Normal),
                    //エンチャント常時発動エフェクト関数代入
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_Normal),
                    //エンチャント常時発動エフェクト削除関数代入
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Normal),
                    //エンチャントサウンド関数代入
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_Normal),
                    //移動関数代入
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_Normal),

                    arrow
                    );
                //エンチャントのUI表示
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Normal();

                //吸い込みのエフェクト
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Normal);

                break;

            case EnchantmentEnum.EnchantmentState.bomb:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_Bomb),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_Bomb),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_Bomb),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Bomb),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_Bomb),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_Bomb),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Bomb();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Bomb);
                break;

            case EnchantmentEnum.EnchantmentState.thunder:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_Thunder),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_Thunder),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_Thunder),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Thunder),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_Thunder),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_Thunder),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Thunder();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Thunder);
                break;

            case EnchantmentEnum.EnchantmentState.knockBack:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_KnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_KnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_KnockBack),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_KnockBack),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_KnockBack),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_KnockBack),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_KnockBack();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_KnockBack);
                break;

            case EnchantmentEnum.EnchantmentState.homing:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_Homing),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_Homing),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_Homing),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Homing),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_Homing),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_Homing),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Homing();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Homing);
                break;

            case EnchantmentEnum.EnchantmentState.penetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_Penetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_Penetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_Penetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_Penetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_Penetrate),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_Penetrate),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Penetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Penetrate);
                break;

            case EnchantmentEnum.EnchantmentState.bombThunder:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_BombThunder),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_BombThunder),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_BombThunder),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_BombThunder),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_BombThunder),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_BombThunder),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_BombThunder();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_BombThunder);
                break;

            case EnchantmentEnum.EnchantmentState.bombKnockBack:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_BombKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_BombKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_BombKnockBack),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_BombKnockBack),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_BombKnockBack),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_BombKnockBack),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_BombKnockBack();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_BombKnockBack);
                break;

            case EnchantmentEnum.EnchantmentState.bombHoming:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_BombHoming),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_BombHoming),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_BombHoming),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_BombHoming),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_BombHoming),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_BombHoming),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_BombHoming();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_BombHoming);
                break;

            case EnchantmentEnum.EnchantmentState.bombPenetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_BombPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_BombPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_BombPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_BombPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_BombPenetrate),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_BombPenetrate),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_BombPenetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_BombPenetrate);
                break;

            case EnchantmentEnum.EnchantmentState.thunderKnockBack:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_ThunderKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_ThunderKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_ThunderKnockBack),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_ThunderKnockBack),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_ThunderKnockBack),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_ThunderKnockBack),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_ThunderKnockBack();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_ThunderKnockBack);
                break;

            case EnchantmentEnum.EnchantmentState.thunderHoming:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_ThunderHoming),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_ThunderHoming),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_ThunderHoming),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_ThunderHoming),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_ThunderHoming),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_ThunderHoming),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_ThunderHoming();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_ThunderHoming);
                break;

            case EnchantmentEnum.EnchantmentState.thunderPenetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_ThunderPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_ThunderPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_ThunderPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_ThunderPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_ThunderPenetrate),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_ThunderPenetrate),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_ThunderPenetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_ThunderPenetrate);
                break;

            case EnchantmentEnum.EnchantmentState.knockBackHoming:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_KnockBackHoming),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_KnockBackHoming),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_KnockBackHoming),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_KnockBackHoming),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_KnockBackHoming),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_KnockBackHoming),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_KnockBackHoming();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_KnockBackHoming);
                break;

            case EnchantmentEnum.EnchantmentState.knockBackpenetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_KnockBackPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_KnockBackPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_KnockBackPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_KnockBackPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_KnockBackPenetrate),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_KnockBackPenetrate),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_KnockBackPenetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_KnockBackPenetrate);
                break;

            case EnchantmentEnum.EnchantmentState.homingPenetrate:
                ArrowEnchant(
                    new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant.ArrowEnchantment_HomingPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect.ArrowEffect_HomingPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffect_HomingPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect.ArrowPassiveEffectDestroy_HomingPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound.ArrowSound_HomingPenetrate),
                    new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove.ArrowMove_HomingPenetrate),
                    arrow);
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_HomingPenetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_HomingPenetrate);
                break;
        }
    }

}