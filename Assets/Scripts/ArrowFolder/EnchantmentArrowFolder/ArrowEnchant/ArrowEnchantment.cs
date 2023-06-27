// --------------------------------------------------------- 
// ArrowEnchantment.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System;
using UnityEngine;

/// <summary>
/// 矢にエンチャント登録することができる
/// </summary>
interface IArrowEventSetting
{
    /// <summary>
    /// 矢にエンチャント登録する
    /// </summary>
    /// <param name="arrow">Arrowクラス</param>
    /// <param name="needMoveChenge">Move処理を更新するか</param>
    /// <param name="enchantmentState">エンチャントのEnum</param>
    void EventSetting(Arrow arrow, bool needMoveChenge, EnchantmentEnum.EnchantmentState enchantmentState);

    /// <summary>
    /// 矢のエンチャントをリセットする
    /// </summary>
    void EnchantmentStateReset();

    void ArrowEnchantPlusDamage();
}
interface IArrowPlusDamage
{
    void ArrowEnchantPlusDamage();
}


/// <summary>
/// 矢のエンチャントの組み合わせを作るクラス
/// </summary>
public sealed class ArrowEnchantment : MonoBehaviour, IArrowEventSetting
{

    private ArrowDelegate[] arrowActions;

    #region 変数宣言部

    public AudioClip _EnchantSound;

    public GameObject TestEnchantGetEffect;


    /// <summary>
    /// 矢の効果クラス
    /// </summary>
    private ArrowEnchant _arrowEnchant;
    /// <summary>
    /// 矢のエフェクトクラス
    /// </summary>
    private ArrowEnchantEffect _arrowEnchantEffect;

    private ArrowPassiveEffect _arrowPassiveEffect;

    private ArrowEnchantSound _arrowEnchantSound;

    private ArrowMove _arrowMove;
    /// <summary>
    /// EnchantmentState型の理想配列を代入する配列
    /// </summary>
    private EnchantmentEnum.EnchantmentState[] _enchantmentStateModels =
        new EnchantmentEnum.EnchantmentState[EnchantmentEnum.EnchantmentStateLength()];

    /// <summary>
    /// エンチャントを理想配列通りに代入する配列
    /// </summary>
    private EnchantmentEnum.EnchantmentState[] _enchantmentStates =
        new EnchantmentEnum.EnchantmentState[EnchantmentEnum.EnchantmentStateLength()];

    /// <summary>
    /// エンチャントがセットされるところがtrueになる配列
    /// </summary>
    private bool[] _isEnchantments = new bool[EnchantmentEnum.EnchantmentStateLength()];

    /// <summary>
    /// エンチャントの組み合わせ設計図
    /// </summary>
    private EnchantmentEnum.EnchantmentState[,] _enchantPreparationNumbers =
    {
        {EnchantmentEnum.EnchantmentState.nomal  ,EnchantmentEnum.EnchantmentState.bomb   ,EnchantmentEnum.EnchantmentState.thunder    ,EnchantmentEnum.EnchantmentState.knockBack       ,EnchantmentEnum.EnchantmentState.homing         ,EnchantmentEnum.EnchantmentState.penetrate          },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.bombThunder,EnchantmentEnum.EnchantmentState.bombKnockBack   ,EnchantmentEnum.EnchantmentState.bombHoming     ,EnchantmentEnum.EnchantmentState.bombPenetrate      },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.thunderKnockBack,EnchantmentEnum.EnchantmentState.thunderHoming  ,EnchantmentEnum.EnchantmentState.thunderPenetrate   },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.nothing         ,EnchantmentEnum.EnchantmentState.knockBackHoming,EnchantmentEnum.EnchantmentState.knockBackpenetrate },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.nothing         ,EnchantmentEnum.EnchantmentState.nothing        ,EnchantmentEnum.EnchantmentState.homingPenetrate    },
        {EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing,EnchantmentEnum.EnchantmentState.nothing    ,EnchantmentEnum.EnchantmentState.nothing         ,EnchantmentEnum.EnchantmentState.nothing        ,EnchantmentEnum.EnchantmentState.nothing            },
    };

    /// <summary>
    /// 現在のエンチャントを代入する変数
    /// </summary>
    private EnchantmentEnum.EnchantmentState _enchantmentStateNow = EnchantmentEnum.EnchantmentState.nomal;
    private EnchantmentEnum.EnchantmentState _enchantmentStateLast = EnchantmentEnum.EnchantmentState.nomal;

    #endregion


    private void Awake()
    {
        //エンチャントのEnumの理想配列を作る
        for (int i = 0; i < _enchantmentStateModels.Length; i++)
        {
            //理想配列作成
            _enchantmentStateModels[i] = (EnchantmentEnum.EnchantmentState)i;
            //初期はfalse 理想通りにEnumが代入されたらtrueにする
            _isEnchantments[i] = false;
        }
        //エンチャントをリセットする
        EnchantmentStateReset();

    }

    /// <summary>
    /// 矢のエンチャント処理を代入する
    /// </summary>
    /// <param name="arrow">Arrowクラス</param>
    /// <param name="needMoveChenge">Move処理を更新するか</param>
    /// <param name="enchantmentState">エンチャントのEnum</param>
    public void EventSetting(Arrow arrow, bool needMoveChenge, EnchantmentEnum.EnchantmentState enchantmentState)
    {
        //エンチャントができない状態ならエンチャントしない
        if (!arrow._needArrowEnchant)
        {
            return;
        }

        _arrowPassiveEffect = arrow.EnchantArrowPassiveEffect;
        _arrowMove = arrow.EnchantArrowMove;

        //Arrowクラスのデリゲートに代入する処理のActionデリゲート
        Action<
            Arrow.ArrowEnchantmentDelegateMethod,
            Arrow.ArrowEffectDelegateMethod,
            Arrow.ArrowEffectDelegateMethod,
            Arrow.ArrowEffectDestroyDelegateMethod,
            Arrow.ArrowEnchantSoundDeletgateMethod,
            Arrow.MoveDelegateMethod> ArrowEnchant = (
                arrowEnchantMethod,
                arrowEffectMethod,
                arrowPassiveEffectMethod,
                arrowPassiveEffectDestroyMethod,
                arrowEnchantSoundMethod,
                arrowMoveMethod) =>
        {
            arrow._EventArrow = arrowEnchantMethod;
            arrow._EventArrowEffect = arrowEffectMethod;
            arrow._EventArrowPassiveEffect = arrowPassiveEffectMethod;
            arrow._EventArrowEffectPassiveDestroy = arrowPassiveEffectDestroyMethod;
            arrow._ArrowEnchantSound = arrowEnchantSoundMethod;

            //移動を更新するか
            if (needMoveChenge)
            {
                arrow._MoveArrow = arrowMoveMethod;
            }
        };

        //取得したEnchantのEnumを掛け合わせて代入
        EnchantmentEnum.EnchantmentState enchantState = EnchantmentStateSetting(enchantmentState);
        if (enchantState != _enchantmentStateLast && enchantState != EnchantmentEnum.EnchantmentState.nomal)
        {
            _arrowEnchantSound.ArrowSound_EnchantSound(_EnchantSound);
            Instantiate(TestEnchantGetEffect, arrow.transform.position, Quaternion.identity);
        }

        //前回のEnum
        _enchantmentStateLast = enchantState;

        //矢にEnumをセット
        arrow.SetEnchantState(enchantState);

        ArrowEnchant(
        arrowActions[(int)enchantState].a,
        arrowActions[(int)enchantmentState].b,
        arrowActions[(int)enchantmentState].c,
        arrowActions[(int)enchantmentState].d,
        arrowActions[(int)enchantmentState].e,
        arrowActions[(int)enchantmentState].f
        );   
    }

    private void Start()
    {
        _arrowEnchant = this.GetComponent<ArrowEnchant>();
        _arrowEnchantEffect = this.GetComponent<ArrowEnchantEffect>();
        _arrowEnchantSound = this.GetComponent<ArrowEnchantSound>();

        //エンチャントの配列インスタンス
        arrowActions = new ArrowDelegate[]
        {
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Nomal),
                new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Nomal),
                new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_Nomal),
                new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_Nomal),
                new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Nomal),
                new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_Nomal)
                    ),
            new ArrowDelegate(
                 new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Bomb),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Bomb),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_Bomb),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_Bomb),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Bomb),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_Bomb)
                ),
            new ArrowDelegate(
                 new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Thunder),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Thunder),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_Thunder),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_Thunder),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Thunder),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_Thunder)
                    ),
            new ArrowDelegate(
                 new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_KnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_KnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_KnockBack),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_KnockBack),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_KnockBack),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_KnockBack)
            ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Homing),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Homing),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_Homing),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_Homing),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Homing),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_Homing)
                ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_Penetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_Penetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_Penetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_Penetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_Penetrate),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_Penetrate)
                ),
            new ArrowDelegate(
                 new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_BombThunder),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_BombThunder),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_BombThunder),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_BombThunder),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_BombThunder),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_BombThunder)
                    ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_BombKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_BombKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_BombKnockBack),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_BombKnockBack),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_BombKnockBack),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_BombKnockBack)
                ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_BombHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_BombHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_BombHoming),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_BombHoming),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_BombHoming),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_BombHoming)
                ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_BombPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_BombPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_BombPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_BombPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_BombPenetrate),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_BombPenetrate)
                ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_ThunderKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_ThunderKnockBack),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_ThunderKnockBack),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_ThunderKnockBack),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_ThunderKnockBack),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_ThunderKnockBack)
                ),
            new ArrowDelegate(
                 new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_ThunderHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_ThunderHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_ThunderHoming),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_ThunderHoming),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_ThunderHoming),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_ThunderHoming)
                ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_ThunderPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_ThunderPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_ThunderPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_ThunderPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_ThunderPenetrate),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_ThunderPenetrate)
                ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_KnockBackHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_KnockBackHoming),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_KnockBackHoming),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_KnockBackHoming),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_KnockBackHoming),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_KnockBackHoming)
                ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_KnockBackPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_KnockBackPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_KnockBackPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_KnockBackPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_KnockBackPenetrate),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_KnockBackPenetrate)
                ),
            new ArrowDelegate(
                new Arrow.ArrowEnchantmentDelegateMethod(_arrowEnchant.ArrowEnchantment_HomingPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowEnchantEffect.ArrowEffect_HomingPenetrate),
                    new Arrow.ArrowEffectDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffect_HomingPenetrate),
                    new Arrow.ArrowEffectDestroyDelegateMethod(_arrowPassiveEffect.ArrowPassiveEffectDestroy_HomingPenetrate),
                    new Arrow.ArrowEnchantSoundDeletgateMethod(_arrowEnchantSound.ArrowSound_HomingPenetrate),
                    new Arrow.MoveDelegateMethod(_arrowMove.ArrowMove_HomingPenetrate)
                )
        };
    }

    public void ArrowEnchantPlusDamage()
    {
        _arrowEnchant.SetAttackDamage();
    }

    /// <summary>
    /// エンチャントの理想配列との一致
    /// </summary>
    /// <param name="enchantmentState"></param>
    /// <returns></returns>
    private EnchantmentEnum.EnchantmentState EnchantmentStateSetting(EnchantmentEnum.EnchantmentState enchantmentState)
    {
        //エンチャントの理想配列を一致したらtrueを対応する添え字の配列に代入
        for (int i = 0; i < _enchantmentStateModels.Length; i++)
        {
            //理想配列と一致しているか判定
            if (_enchantmentStateModels[i] == enchantmentState)
            {
                //一致していればtrue
                _isEnchantments[i] = true;
            }
        }
        return EnchantmentPreparation();

    }
    /// <summary>
    /// エンチャントのEnumの組み合わせからEnum代入
    /// </summary>
    /// <returns>掛け合わせ完成EnchantState</returns>
    private EnchantmentEnum.EnchantmentState EnchantmentPreparation()
    {
        bool exitisEnchantment = false;
        for (int i = 0; i < _isEnchantments.Length - 1; i++)
        {
            for (int j = i + 1; j < _isEnchantments.Length; j++)
            {
                if (_isEnchantments[i] && _isEnchantments[j])
                {
                    //2つtrueのときにi,jに登録してあるEnumをNowに代入
                    _enchantmentStateNow = _enchantPreparationNumbers[i, j];
                    exitisEnchantment = true;
                }
            }
        }
        if (!exitisEnchantment)
        {
            _enchantmentStateNow = EnchantmentEnum.EnchantmentState.nomal;
        }
        return _enchantmentStateNow;
    }

    /// <summary>
    /// エンチャントのStateをリセットする　エンチャント対象の矢が変更される時に呼ぶ
    /// </summary>
    public void EnchantmentStateReset()
    {
        for (int i = 0; i < _isEnchantments.Length; i++)
        {
            _isEnchantments[i] = false;
        }
        _isEnchantments[0] = true;
    }

}
