// --------------------------------------------------------- 
// BodyChipLife.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BodyChipLife : MonoBehaviour
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method
    private ObjectPoolSystem objectPoolSystem;
    private CashObjectInformation cashObjectInformation;

    private void Awake()
    {
        objectPoolSystem = GameObject.FindWithTag(InhallLibTags.PoolSystem).GetComponent<ObjectPoolSystem>();
        cashObjectInformation = this.GetComponent<CashObjectInformation>();
    }
    private void OnEnable()
    {
        StartCoroutine(Life());
    }

    private IEnumerator Life()
    {
        yield return new WaitForSeconds(2f);
        objectPoolSystem.ReturnObject(cashObjectInformation);
    }
    #endregion
}