// --------------------------------------------------------- 
// RootEnableFalseToMyTrue.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CashObjectInformation))]
public class RootEnableFalseToMyTrue : MonoBehaviour, IFRootFalseEvent
{
    [SerializeField] ParticleSystem particle;
    ObjectPoolSystem pool;
    CashObjectInformation information;
    private void Awake()
    {
        pool = FindObjectOfType<ObjectPoolSystem>();
        information = GetComponent<CashObjectInformation>();
    }
    public void RootFalseAction()
    {
        transform.SetParent(null);
        print(gameObject + "aaa");
        particle.Stop();
        StartCoroutine(Pool());
    }
    IEnumerator Pool()
    {
        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        pool.ReturnObject(information);
    }
}