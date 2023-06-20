// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class MouseSp : MonoBehaviour
{
    float time1 = 0f;
    float time2 = 0f;
    float pow;
    Vector3 pos1 = Vector3.zero;
    Vector3 pos2 = Vector3.zero;

    string txt = default;
    
    // Start is called before the first frame update
    void Start()
    {
        time1 = Time.time;
        SetMousePos(ref pos1);

    }

   

    // Update is called once per frame
    void FixedUpdate()
    {
        time2 = Time.time;
        SetMousePos(ref pos2);

        float difTime = time2 - time1;
        Vector3 difPos = pos2 - pos1;
        
        txt = (difPos.magnitude / difTime).ToString();
        time1 = time2;
        pos1 = pos2;
    }

    private void SetMousePos(ref Vector3 pos)
    {
        pos = Input.mousePosition;
        pos.z = 10;
        pos = Camera.main.ScreenToWorldPoint(pos);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 1000, 100), txt);
    }

    
}
