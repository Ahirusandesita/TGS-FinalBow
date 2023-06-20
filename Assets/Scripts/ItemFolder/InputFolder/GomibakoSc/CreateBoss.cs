// --------------------------------------------------------- 
// CreateBoss.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class CreateBoss : MonoBehaviour
{
    #region variable 
    public int createValue = 30;

    public GameObject obj;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        for(int i = 0; i < createValue; i++)
        {
            Instantiate(obj, transform.position , transform.rotation);
        }
    }
    #endregion
}