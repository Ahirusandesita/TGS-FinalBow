// --------------------------------------------------------- 
// ParticleCallbackPool.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ParticleCallbackPool : MonoBehaviour
{
    ObjectPoolSystem pool = default;
    ParticleSystem particle = default;
    [SerializeField] EffectPoolEnum.EffectPoolState state = EffectPoolEnum.EffectPoolState.thunderBlast;
    [SerializeField] GameObject rootObject;
    //Transform _transform = default;

    //int particles = 0;
    private void Awake()
    {
        pool = FindObjectOfType<ObjectPoolSystem>();


        particle = GetComponent<ParticleSystem>();
        //foreach(Transform t in _transform)
        //{

        //}
    }

    private void OnEnable()
    {
        particle.Play(true);
    }
    private void OnParticleSystemStopped()
    {
        
        pool.ReturnObject(state,rootObject);
    }
}