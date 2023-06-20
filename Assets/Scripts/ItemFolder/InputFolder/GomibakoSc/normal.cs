// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class normal : MonoBehaviour
{
    Vector3 vec = new Vector3(0.01f, 5f, 0.01f);
    Quaternion qc;
    // Start is called before the first frame update
    void Start()
    {
        print(vec + ">>" + vec.normalized);
        print(Quaternion.Euler(vec));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
