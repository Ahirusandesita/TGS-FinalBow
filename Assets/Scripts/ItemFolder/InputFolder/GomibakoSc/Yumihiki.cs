// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class Yumihiki : MonoBehaviour
{
    MouseInput pt;
    Vector3 basepos;
    Vector3 pos1;
    Vector3 pos2;
    float time1;
    float time2;

    private void Start()
    {
        basepos = transform.position;
        pos1 = transform.position;
        time1 = Time.time;
        SetMousePos(ref pos1);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Vector3.Distance(pt.target, transform.position) < 1f)
            {
                Grap();
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        time2 = Time.time;
        SetMousePos(ref pos2);

        float difTime = time2 - time1;
        Vector3 difPos = pos2 - pos1;
        
        time1 = time2;
        pos1 = pos2;
    }

    private void SetMousePos(ref Vector3 pos)
    {
        pos = Input.mousePosition;
        pos.z = 10;
        pos = Camera.main.ScreenToWorldPoint(pos);
    }

    Vector3 GetMouse()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10;
        return Camera.main.ScreenToWorldPoint(pos);
    }

    private void Grap()
    {
        transform.localPosition = GetMouse();
    }

}
