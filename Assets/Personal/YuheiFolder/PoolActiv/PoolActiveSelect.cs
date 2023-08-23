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
        for (int i = 0; i < activeClass.activeDatas.Count; i++)
        {
            activeClass.activeDatas[i].ActiveCompoent.enabled = false;         
        }
    }
}