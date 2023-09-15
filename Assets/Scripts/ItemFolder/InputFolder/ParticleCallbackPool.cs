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
    [SerializeField]CashObjectInformation root = default;
    //Transform _transform = default;

    //int particles = 0;
    private void Awake()
    {
        pool = FindObjectOfType<ObjectPoolSystem>();
        particle = GetComponent<ParticleSystem>();
        if(root is null)
        {
            root = transform.GetComponent<CashObjectInformation>();

            if (root is null)
            {
                root = transform.parent.GetComponent<CashObjectInformation>();
            }
        }
        
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
        
        pool.ReturnObject(root);
    }
}