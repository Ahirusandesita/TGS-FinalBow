// --------------------------------------------------------- 
// bakuha.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class bakuha : MonoBehaviour
{
    #region variable 
    [SerializeField] int x = 100;
    [SerializeField] int y = 100;
    [SerializeField] int z = 100;

    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.AddComponent(typeof (BirdStats));

        for(int i =0;i < x; i++)
        {
            for(int k = 0; k < y; k++)
            {
                for(int m = 0; m < z; m++)
                {
                    Instantiate(obj, new Vector3(x, y, z), Quaternion.identity);
                }
            }
        }

    }



    #endregion
}