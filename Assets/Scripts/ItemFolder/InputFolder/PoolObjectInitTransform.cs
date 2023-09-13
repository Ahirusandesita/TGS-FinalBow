// --------------------------------------------------------- 
// PoolObjectInitTransform.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class PoolObjectInitTransform : MonoBehaviour
{
    Vector3 _initScale = default;
    private void Awake()
    {
        _initScale = transform.localScale;
    }

    private void OnDisable()
    {
        transform.localScale = _initScale;
    }
}