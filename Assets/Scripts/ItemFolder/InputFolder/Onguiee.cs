// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class Onguiee : MonoBehaviour
{
   public string one { private get; set; }

   public string sec { private get; set; }

   public string thr { private get; set; }


    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 1000, 100), one);
        GUI.Label(new Rect(0, 100, 1000, 100), sec);
        GUI.Label(new Rect(0, 200, 1000, 100), thr);
    }
}
