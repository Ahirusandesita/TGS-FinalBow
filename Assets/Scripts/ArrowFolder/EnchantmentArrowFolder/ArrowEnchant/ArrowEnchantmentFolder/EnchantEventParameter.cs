// 81-C# NomalScript-NewScript.cs
//
//CreateDay:
//Creator  :
//
using System;
using UnityEngine;

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


        public IArrowEnchantable<GameObject, EnchantmentEnum.EnchantmentState> _arrowEnchant2;

        /// <summary>
        /// 矢のエフェクトクラス
        /// </summary>
        public IArrowEnchantable<Transform> _arrowEnchantEffect2;
        /// <summary>
        /// 矢の常時エフェクトクラス
        /// </summary>


        public IArrowEnchantable<Transform> _arrowEnchantPassiveEffect2;

        public IArrowEnchantable<GameObject> _arrowEnchantPassiveEffectDestroy;

        /// <summary>
        /// 矢の効果音クラス
        /// </summary>


        public IArrowEnchantable<AudioSource> _arrowEnchantSound2;

        /// <summary>
        /// 矢の移動クラス
        /// </summary>


        public IArrowEnchantable<Transform> arrowMove2;

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


    private struct EnchantmentDelegateData
    {
        public Arrow.ArrowEffectDelegateMethod arrowEffectDelegateMethod;
        public IArrowEnchantable<Transform> arrowPassiveEffect;

        public EnchantmentDelegateData(Arrow.ArrowEffectDelegateMethod arrowEffectDelegateMethod, IArrowEnchantable<Transform> arrowEnchantable)
        {
            this.arrowEffectDelegateMethod = arrowEffectDelegateMethod;
            arrowPassiveEffect = arrowEnchantable;
        }
    }

    private struct EnchantDelegateData
    {
        public Arrow.ArrowEffectDelegateMethod arrowEffectPassiveDelegateMethod;
        public Arrow.ArrowEffectDestroyDelegateMethod arrowEffectPassiveDestroyDelegateMethod;
        public Arrow.MoveDelegateMethod arrowMoveDelegateMethod;
        public Arrow.ArrowEnchantSoundDeletgateMethod arrowSoundDelegateMethod;
        public Arrow.ArrowEnchantmentDelegateMethod arrowEnchantDelegateMethod;
        public Arrow.ArrowEffectDelegateMethod arrowEffectDelegateMethod;
    }


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




    Action<EnchantDelegateData, IArrowEnchant> ArrowEnchant2 = (
          enchantDelegateDeta,
          arrow) =>
      {
          arrow.EventArrowPassiveEffect = enchantDelegateDeta.arrowEffectPassiveDelegateMethod;
          arrow.EventArrowEffectDestroy = enchantDelegateDeta.arrowEffectPassiveDestroyDelegateMethod;
          arrow.MoveArrow = enchantDelegateDeta.arrowMoveDelegateMethod;
          arrow.ArrowEnchantSound = enchantDelegateDeta.arrowSoundDelegateMethod;
          arrow.EventArrow = enchantDelegateDeta.arrowEnchantDelegateMethod;
          arrow.EventArrowEffect = enchantDelegateDeta.arrowEffectDelegateMethod;
      };



    public void EnchantEventAttribute(EnchantmentEnum.EnchantmentState enchantState, IArrowEnchant arrow)
    {
        arrow.SetEnchantState(enchantState);


        _enchantEvents.arrowMove2 = arrow.EnchantArrowMove;
        _enchantEvents._arrowEnchantPassiveEffect2 = arrow.EnchantArrowPassiveEffect;
        _enchantEvents._arrowEnchantPassiveEffectDestroy = arrow.EnchantArrowPassiveEffect;

        EnchantDelegateData enchantDelegateData = default;


        enchantDelegateData.arrowEffectDelegateMethod = new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantPassiveEffect2.EnchantLevel(enchantState));
        enchantDelegateData.arrowEffectPassiveDestroyDelegateMethod = new Arrow.ArrowEffectDestroyDelegateMethod(_enchantEvents._arrowEnchantPassiveEffectDestroy.EnchantLevel(enchantState));
        enchantDelegateData.arrowMoveDelegateMethod = new Arrow.MoveDelegateMethod(_enchantEvents.arrowMove2.EnchantLevel(enchantState));
        enchantDelegateData.arrowEnchantDelegateMethod = new Arrow.ArrowEnchantmentDelegateMethod(_enchantEvents._arrowEnchant2.EnchantLevel(enchantState));
        enchantDelegateData.arrowEffectDelegateMethod = new Arrow.ArrowEffectDelegateMethod(_enchantEvents._arrowEnchantEffect2.EnchantLevel(enchantState));
        enchantDelegateData.arrowSoundDelegateMethod = new Arrow.ArrowEnchantSoundDeletgateMethod(_enchantEvents._arrowEnchantSound2.EnchantLevel(enchantState));
        ArrowEnchant2(enchantDelegateData, arrow);

    }

    public void NewEnchantEvent(EnchantmentEnum.EnchantmentState enchantState)
    {
        switch (enchantState)
        {
            case EnchantmentEnum.EnchantmentState.normal:

                //エンチャントのUI表示
                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Normal();

                //吸い込みのエフェクト
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Normal);

                break;

            case EnchantmentEnum.EnchantmentState.bomb:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Bomb();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Bomb);
                break;

            case EnchantmentEnum.EnchantmentState.thunder:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Thunder();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Thunder);
                break;

            case EnchantmentEnum.EnchantmentState.knockBack:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_KnockBack();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_KnockBack);
                break;

            case EnchantmentEnum.EnchantmentState.homing:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Homing();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Homing);
                break;

            case EnchantmentEnum.EnchantmentState.penetrate:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_Penetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_Penetrate);
                break;

            case EnchantmentEnum.EnchantmentState.bombThunder:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_BombThunder();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_BombThunder);
                break;

            case EnchantmentEnum.EnchantmentState.bombKnockBack:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_BombKnockBack();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_BombKnockBack);
                break;

            case EnchantmentEnum.EnchantmentState.bombHoming:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_BombHoming();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_BombHoming);
                break;

            case EnchantmentEnum.EnchantmentState.bombPenetrate:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_BombPenetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_BombPenetrate);
                break;

            case EnchantmentEnum.EnchantmentState.thunderKnockBack:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_ThunderKnockBack();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_ThunderKnockBack);
                break;

            case EnchantmentEnum.EnchantmentState.thunderHoming:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_ThunderHoming();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_ThunderHoming);
                break;

            case EnchantmentEnum.EnchantmentState.thunderPenetrate:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_ThunderPenetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_ThunderPenetrate);
                break;

            case EnchantmentEnum.EnchantmentState.knockBackHoming:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_KnockBackHoming();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_KnockBackHoming);
                break;

            case EnchantmentEnum.EnchantmentState.knockBackpenetrate:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_KnockBackPenetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_KnockBackPenetrate);
                break;

            case EnchantmentEnum.EnchantmentState.homingPenetrate:

                _enchantEvents._arrowEnchantUI.ArrowEnchantUI_HomingPenetrate();
                _enchantEvents._newEnchantEffect = new EnchantEvents.NewEnchantEffect(_enchantEvents._atractEffect.AttractEffectEffect_HomingPenetrate);
                break;
        }
    }

}