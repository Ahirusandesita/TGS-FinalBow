// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;
public class ArrowPassiveEffect : MonoBehaviour, IArrowEnchantable<Transform>, IArrowEnchantable<GameObject>, IArrowEnchantDamageable
{
    private ObjectPoolSystem _objectPoosSystem;

    private GameObject _workEffect;
    private EffectPoolEnum.EffectPoolState _workEnchantState;
    private SizeAdjustmentToVector3 sizeAdjustmentToVector3;
    private void Start()
    {
        _objectPoosSystem = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
    }
    #region variable 
    #endregion
    #region property
    #endregion
    #region method



    /// <summary>
    /// エフェクトを切り替えるか生成するか
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name="checkArrowState"></param>
    private void PassiveEffectCreate(Transform spawnPosition, EffectPoolEnum.EffectPoolState checkArrowState)
    {
        //まだエフェクトが存在しなければ
        if (_workEffect == null)
        {
            //エフェクトを生成して代入する
            _workEnchantState = checkArrowState;
            _workEffect = _objectPoosSystem.CallObject(checkArrowState, spawnPosition.position, spawnPosition.rotation);
            _workEffect.transform.parent = spawnPosition.transform;

            sizeAdjustmentToVector3 = new SizeAdjustmentToVector3(
                _workEffect.transform.localScale.x,
                _workEffect.transform.localScale.y,
                _workEffect.transform.localScale.z
                );

            _workEffect.transform.localScale = sizeAdjustmentToVector3.GetMinimumSizeToVector3/2.5f;
        }
        //エフェクトが存在していて新しいEnumが違ったら
        else if (_workEnchantState != checkArrowState)
        {
            //現在のエフェクトをReturnして新しいエフェクトを生成
            _workEffect.transform.localScale = sizeAdjustmentToVector3.GetFirstSizeToVector3;
            _workEffect.transform.parent = null;

            _objectPoosSystem.ReturnObject(_workEnchantState, _workEffect);

            _workEnchantState = checkArrowState;


            _workEffect = _objectPoosSystem.CallObject(checkArrowState, spawnPosition.position, spawnPosition.rotation);
            _workEffect.transform.parent = spawnPosition.transform;

            sizeAdjustmentToVector3 = new SizeAdjustmentToVector3(
                _workEffect.transform.localScale.x,
                _workEffect.transform.localScale.y,
                _workEffect.transform.localScale.z,
                sizeAdjustmentToVector3.plusCount
                );

            _workEffect.transform.localScale = sizeAdjustmentToVector3.GetMinimumSizeToVector3 / 2.5f;

            int plusCount = sizeAdjustmentToVector3.plusCount;
            while (plusCount > 0)
            {

                _workEffect.transform.localScale += sizeAdjustmentToVector3.GetMinimumSizeToVector3 / 2.5f;
                plusCount--;
            }
        }

    }

    /// <summary>
    /// エフェクトを消す
    /// </summary>
    /// <param name="arrowObject"></param>
    /// <param name="checkEffectState"></param>
    private void PassiveEffectDestroy(GameObject arrowObject, EffectPoolEnum.EffectPoolState checkEffectState)
    {
        //エフェクトを消す
        _workEffect.transform.localScale = sizeAdjustmentToVector3.GetFirstSizeToVector3;
        _workEffect.transform.parent = null;
        _objectPoosSystem.ReturnObject(_workEnchantState, _workEffect);
    }

    public void SetAttackDamage()
    {
        if (_workEffect == null) return;
        sizeAdjustmentToVector3.plusCount++;
        _workEffect.transform.localScale += sizeAdjustmentToVector3.GetMinimumSizeToVector3 / 2.5f;
    }

    public void Normal(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.nomalPassive);
    }

    public void Bomb(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.bombPassive);
    }

    public void Thunder(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.thunderPassive);
    }

    public void KnockBack(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.knockBackPassive);
    }

    public void Penetrate(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.penetratePassive);
    }

    public void Homing(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.homingPassive);
    }

    public void BombThunder(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.bombThunderPassive);
    }

    public void BombKnockBack(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.bombKnockBackPassive);
    }

    public void BombPenetrate(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.bombPenetratePassive);
    }

    public void BombHoming(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.bombHomingPassive);
    }

    public void ThunderKnockBack(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.thunderKnockBackPassive);
    }

    public void ThunderPenetrate(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.thunderPenetratePassive);
    }

    public void ThunderHoming(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.thunderHomingPassive);
    }

    public void KnockBackPenetrate(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.knockBackpenetratePassive);
    }

    public void KnockBackHoming(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.knockBackHomingPassive);
    }

    public void PenetrateHoming(Transform t)
    {
        PassiveEffectCreate(t, EffectPoolEnum.EffectPoolState.homingPenetratePassive);
    }




    public void Normal(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.nomalPassive);
    }

    public void Bomb(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.bombPassive);
    }

    public void Thunder(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.thunderPassive);
    }

    public void KnockBack(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.knockBackPassive);
    }

    public void Penetrate(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.penetratePassive);
    }

    public void Homing(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.homingPassive);
    }

    public void BombThunder(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.bombThunderPassive);
    }

    public void BombKnockBack(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.bombKnockBackPassive);
    }

    public void BombPenetrate(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.bombPenetratePassive);
    }

    public void BombHoming(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.bombHomingPassive);
    }

    public void ThunderKnockBack(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.thunderKnockBackPassive);
    }

    public void ThunderPenetrate(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.thunderPenetratePassive);
    }

    public void ThunderHoming(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.thunderHomingPassive);
    }

    public void KnockBackPenetrate(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.knockBackpenetratePassive);
    }

    public void KnockBackHoming(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.knockBackHomingPassive);
    }

    public void PenetrateHoming(GameObject t)
    {
        PassiveEffectDestroy(t, EffectPoolEnum.EffectPoolState.homingPenetratePassive);
    }



    #endregion

}