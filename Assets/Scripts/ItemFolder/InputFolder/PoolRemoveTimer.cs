// --------------------------------------------------------- 
// PoolRemoveTimer.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class PoolRemoveTimer : MonoBehaviour
{
    ObjectPoolSystem pool = default;
    CashObjectInformation cash = default;
    WaitForSeconds wait = new WaitForSeconds(waitTime);
    const float waitTime = 5f;
    float cacheTime = 0f;
    private void Start()
    {
        pool = FindObjectOfType<ObjectPoolSystem>();
        cash = GetComponent<CashObjectInformation>();
    }

    private void OnEnable()
    {
        cacheTime = Time.time;
        StartCoroutine(DestroyTimer());
    }
    
    IEnumerator DestroyTimer()
    {
        yield return wait;

        pool.ReturnObject(cash);
    }
}