// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class SettingBowObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // �ł΂悤
        StaticBowObject.BowManagerQue = this.gameObject.GetComponent<BowManager>();
    }

    
}
