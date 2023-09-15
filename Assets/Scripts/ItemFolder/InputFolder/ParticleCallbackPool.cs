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
    Transform _transform = default;

    int particles = 0;
    private void Awake()
    {
        pool = FindObjectOfType<ObjectPoolSystem>();
      
        foreach(Transform t in _transform)
        {

        }
    }

    private void OnEnable()
    {
        
    }
}