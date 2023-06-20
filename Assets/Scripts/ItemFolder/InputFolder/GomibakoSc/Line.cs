// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer line;
    public GameObject obj;
    // Update is called once per frame
    void Update()
    {
        line.SetPosition(1, obj.transform.localPosition);
    }
}
