using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private ObjectPoolSystem _objPoolSys = default;

    // Start is called before the first frame update
    void Start()
    {
        _objPoolSys = this.GetComponent<ObjectPoolSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // _objPoolSys.CallObject(Vector3.zero);
        }
    }
}
