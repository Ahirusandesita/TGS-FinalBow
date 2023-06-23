// --------------------------------------------------------- 
// ArrowEnchantEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections;
using UnityEngine;

public class ArrowEnchantEffect : MonoBehaviour
{
    private ObjectPoolSystem _objectPoolSystem;

    private WaitForSeconds _waitSeconds = default;

    private void Start()
    {
        _objectPoolSystem = GameObject.FindGameObjectWithTag("PoolSystem").GetComponent<ObjectPoolSystem>();
        _waitSeconds = new WaitForSeconds(1f);
    }


    public void ArrowEffect_Nomal(Transform spawnPosition)
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



    /// <summary>
    /// エフェクト生成
    /// </summary>
    /// <param name="effectState"></param>
    /// <param name="spawnTransform"></param>
    private void EffectCall(EffectPoolEnum.EffectPoolState effectState,Transform spawnTransform)
    {
        StartCoroutine(EffectTime(_objectPoolSystem.CallObject(effectState, spawnTransform.position, spawnTransform.rotation), effectState));
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

        _objectPoolSystem.ReturnObject(effectPoolState,effectObject);
    }
}
