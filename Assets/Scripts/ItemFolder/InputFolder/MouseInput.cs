// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    RaycastHit hit;
    public Vector3 target;
    // Update is called once per frame
    void Update()
    {
        
        Ray ray = new Ray(Camera.main.transform.position, GetMouse() - Camera.main.transform.position);
        
        if (Physics.Raycast(ray, out hit, 10f))
        {
            print(hit.transform.position);
            target = hit.transform.position;
        }
        else
        {
            target = GetMouse();
        }
        transform.position = target;
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - GetMouse());
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, GetMouse());
    }

    Vector3 GetMouse()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10;
        return Camera.main.ScreenToWorldPoint(pos);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0,0,100,100), GetMouse().ToString());
    }
}
