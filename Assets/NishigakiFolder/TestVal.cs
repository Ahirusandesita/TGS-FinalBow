using UnityEngine;
//
//CreateDay:
//Creator  :
//
interface ITestVal
{

}

public class TestVal: OVRMonoscopic
{
	private Vector3 _tester = default;

    //Can be instantiated from outside

    private void Start()
    {
        _tester = new Vector3();
    }

    private void Update()
	{

	}

}