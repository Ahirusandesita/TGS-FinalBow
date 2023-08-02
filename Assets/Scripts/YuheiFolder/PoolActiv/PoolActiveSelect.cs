// --------------------------------------------------------- 
// PoolActiveSelect.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
public class PoolActiveSelect : MonoBehaviour
{
    public void Enabled(ActiveClass activeClass)
    {
        for (int i = 0; i < activeClass.GetOnlyActiveConponents.Count; i++)
        {
            activeClass.GetOnlyActiveConponents[i].enabled = false;
           
        }
    }
}