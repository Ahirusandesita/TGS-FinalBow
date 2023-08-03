// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;
public class ArrowPassiveEffect : MonoBehaviour,IArrowEnchantable<Transform>,IArrowEnchantable<GameObject>
{
    private ObjectPoolSystem _objectPoosSystem;


    private GameObject _workEffect;
    private EffectPoolEnum.EffectPoolState _workEnchantState;
    private void Start()
    {
        _objectPoosSystem = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
    }
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    public void ArrowPassiveEffect_Normal(Transform spawnPosition)
    {
        //_objectPoosSystem.CallObject(PoolEnum.PoolObjectType.nomal, spawnPosition.position, spawnPosition.rotation);
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.nomalPassive);
    }

    public void ArrowPassiveEffectDestroy_Normal(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.nomalPassive);
    }


    public void ArrowPassiveEffect_Bomb(Transform spawnPosition)
    {
        //_objectPoosSystem.CallObject(PoolEnum.PoolObjectType.nomal, spawnPosition.position, spawnPosition.rotation);
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.bombPassive);
    }
    public void ArrowPassiveEffectDestroy_Bomb(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.bombPassive);
    }




    public void ArrowPassiveEffect_Thunder(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.thunderPassive);
    }

    public void ArrowPassiveEffectDestroy_Thunder(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.thunderPassive);
    }




    public void ArrowPassiveEffect_KnockBack(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.knockBackPassive);
    }

    public void ArrowPassiveEffectDestroy_KnockBack(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.knockBackPassive);
    }




    public void ArrowPassiveEffect_Homing(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.homingPassive);
    }

    public void ArrowPassiveEffectDestroy_Homing(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.homingPassive);
    }




    public void ArrowPassiveEffect_Penetrate(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.penetratePassive);
    }

    public void ArrowPassiveEffectDestroy_Penetrate(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.penetratePassive);
    }



    public void ArrowPassiveEffect_BombThunder(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.bombThunderPassive);
    }

    public void ArrowPassiveEffectDestroy_BombThunder(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.bombThunderPassive);
    }




    public void ArrowPassiveEffect_BombKnockBack(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.bombKnockBackPassive);
    }

    public void ArrowPassiveEffectDestroy_BombKnockBack(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.bombKnockBackPassive);
    }




    public void ArrowPassiveEffect_BombHoming(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.bombHomingPassive);
    }

    public void ArrowPassiveEffectDestroy_BombHoming(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.bombHomingPassive);
    }




    public void ArrowPassiveEffect_BombPenetrate(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.bombPenetratePassive);
    }

    public void ArrowPassiveEffectDestroy_BombPenetrate(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.bombPenetratePassive);
    }




    public void ArrowPassiveEffect_ThunderKnockBack(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.thunderKnockBackPassive);
    }

    public void ArrowPassiveEffectDestroy_ThunderKnockBack(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.thunderKnockBackPassive);
    }




    public void ArrowPassiveEffect_ThunderHoming(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.thunderHomingPassive);
    }

    public void ArrowPassiveEffectDestroy_ThunderHoming(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.thunderHomingPassive);
    }




    public void ArrowPassiveEffect_ThunderPenetrate(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.thunderPenetratePassive);
    }

    public void ArrowPassiveEffectDestroy_ThunderPenetrate(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.thunderPenetratePassive);
    }




    public void ArrowPassiveEffect_KnockBackHoming(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.knockBackHomingPassive);
    }

    public void ArrowPassiveEffectDestroy_KnockBackHoming(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.knockBackHomingPassive);
    }




    public void ArrowPassiveEffect_KnockBackPenetrate(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.knockBackpenetratePassive);
    }

    public void ArrowPassiveEffectDestroy_KnockBackPenetrate(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.knockBackpenetratePassive);
    }



    public void ArrowPassiveEffect_HomingPenetrate(Transform spawnPosition)
    {
        PassiveEffectCreate(spawnPosition, EffectPoolEnum.EffectPoolState.homingPenetratePassive);
    }

    public void ArrowPassiveEffectDestroy_HomingPenetrate(GameObject arrowObject)
    {
        PassiveEffectDestroy(arrowObject, EffectPoolEnum.EffectPoolState.homingPenetratePassive);
    }


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
            
        }
        //エフェクトが存在していて新しいEnumが違ったら
        else if (_workEnchantState != checkArrowState)
        {
            //現在のエフェクトをReturnして新しいエフェクトを生成
            _workEffect.transform.parent = null;
            _objectPoosSystem.ReturnObject(_workEnchantState, _workEffect);
            _workEnchantState = checkArrowState;
            _workEffect = _objectPoosSystem.CallObject(checkArrowState, spawnPosition.position, spawnPosition.rotation);
            _workEffect.transform.parent = spawnPosition.transform;
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
        _workEffect.transform.parent = null;
        _objectPoosSystem.ReturnObject(_workEnchantState, _workEffect);
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