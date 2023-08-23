// --------------------------------------------------------- 
// ArrowEnchantEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections;
using UnityEngine;

public struct Size
{
    public Vector3 firstSize;
    public Vector3 plusSize;
    public int plusCount;
}

public class SizeParamator
{

}


public class ArrowEnchantEffect : MonoBehaviour,IArrowEnchantable<Transform>,IArrowEnchantDamageable
{
    private ObjectPoolSystem _objectPoolSystem;

    private WaitForSeconds _waitSeconds = default;

    private Size size;

    private void Start()
    {
        _objectPoolSystem = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        _waitSeconds = new WaitForSeconds(1f);
    }


    public void ArrowEffect_Normal(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.nomal, spawnPosition);
    }


    public void ArrowEffect_Bomb(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bomb, spawnPosition);
    }



    public void ArrowEffect_Thunder(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.thunder, spawnPosition);
    }


    public void ArrowEffect_KnockBack(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.knockBack, spawnPosition);
    }




    public void ArrowEffect_Homing(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.homing, spawnPosition);
    }




    public void ArrowEffect_Penetrate(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.penetrate, spawnPosition);
    }


    public void ArrowEffect_BombThunder(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bombThunder, spawnPosition);
    }

    public void ArrowEffect_BombKnockBack(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bombKnockBack, spawnPosition);
    }


    public void ArrowEffect_BombHoming(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bombHoming, spawnPosition);
    }


    public void ArrowEffect_BombPenetrate(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bombPenetrate, spawnPosition);
    }


    public void ArrowEffect_ThunderKnockBack(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.thunderKnockBack, spawnPosition);
    }


    public void ArrowEffect_ThunderHoming(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.thunderHoming, spawnPosition);
    }

  


    public void ArrowEffect_ThunderPenetrate(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.thunderPenetrate, spawnPosition);
    }

  


    public void ArrowEffect_KnockBackHoming(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.knockBackHoming, spawnPosition);
    }




    public void ArrowEffect_KnockBackPenetrate(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.knockBackpenetrate, spawnPosition);
    }



    public void ArrowEffect_HomingPenetrate(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.homingPenetrate, spawnPosition);
    }

    public void ArrowEffect_NewEnchantEffect(Transform spawnPosition)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.newEnchantEffect, spawnPosition);
    }





    /// <summary>
    /// エフェクト生成
    /// </summary>
    /// <param name="effectState"></param>
    /// <param name="spawnTransform"></param>
    private void EffectCall(EffectPoolEnum.EffectPoolState effectState,Transform spawnTransform)
    {
        StartCoroutine(EffectTime(_objectPoolSystem.CallObject(effectState, spawnTransform.position, spawnTransform.rotation), effectState));
    }
    private void EffectCall(EffectPoolEnum.EffectPoolState effectState,Transform spawnTransform,ref Size size)
    {

        GameObject effect = _objectPoolSystem.CallObject(effectState, spawnTransform.position, spawnTransform.rotation);
        size.firstSize = effect.transform.localScale/ 5f;
        effect.transform.localScale = size.firstSize;
        while(size.plusCount >= 0) 
        { 
            Debug.LogError("Attra" + size.plusCount);
            effect.transform.localScale += size.firstSize;
            size.plusCount--;
        }
        
        StartCoroutine(EffectTime(effect, effectState));
    }

    /// <summary>
    /// エフェクトをリターンするコルーチン
    /// </summary>
    /// <param name="effectObject"></param>
    /// <param name="effectPoolState"></param>
    /// <returns></returns>
    private IEnumerator EffectTime(GameObject effectObject,EffectPoolEnum.EffectPoolState effectPoolState)
    {
        yield return _waitSeconds;

        effectObject.transform.localPosition = size.firstSize * 8f;
        _objectPoolSystem.ReturnObject(effectPoolState,effectObject);
    }

    public void Normal(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.nomal, t);
    }

    public void Bomb(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bomb, t,ref size);
    }

    public void Thunder(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.thunder, t);
    }

    public void KnockBack(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.knockBack, t);
    }

    public void Penetrate(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.penetrate, t);
    }

    public void Homing(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.homing, t);
    }

    public void BombThunder(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bombThunder, t);
    }

    public void BombKnockBack(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bombKnockBack, t);
    }

    public void BombPenetrate(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bombPenetrate, t);
    }

    public void BombHoming(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.bombHoming, t);
    }

    public void ThunderKnockBack(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.thunderKnockBack, t);
    }

    public void ThunderPenetrate(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.thunderPenetrate, t);
    }

    public void ThunderHoming(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.thunderHoming, t);
    }

    public void KnockBackPenetrate(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.knockBackpenetrate, t);
    }

    public void KnockBackHoming(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.knockBackHoming, t);
    }

    public void PenetrateHoming(Transform t)
    {
        EffectCall(EffectPoolEnum.EffectPoolState.homingPenetrate, t);
    }

    public void SetAttackDamage()
    {
        size.plusCount++;
    }
}
