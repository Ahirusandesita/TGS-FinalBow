// --------------------------------------------------------- 
// ItemShotDemoUltra.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ItemShotDemoUltra : MonoBehaviour
{


    [SerializeField]
    private GameObject _itemObject = default;

    [SerializeField]
    private Transform _spawner = default;

    [SerializeField]
    private GameObject target = default;


    private void Start()
    {
        StartCoroutine(A());
    }

    private void Update()
    {
        transform.LookAt(target.transform);


        
    }


    private IEnumerator A()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(0.2f);
            if(Random.Range(0, 10) == 5)
            {
                Instantiate(_itemObject, _spawner.position, transform.rotation);
            }
        }

    }
}