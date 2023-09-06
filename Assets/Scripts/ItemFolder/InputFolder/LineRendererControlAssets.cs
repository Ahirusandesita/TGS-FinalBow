// --------------------------------------------------------- 
// LineRendererControlAssets.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class LineRendererControlAssets
{
    LineRenderer[] lines;

    
    public void SetControllLine(GameObject obj)
    {
        lines = obj.GetComponentsInChildren<LineRenderer>();
    }

    public void LineSetIndex(float distance)
    {
        foreach(LineRenderer line in lines)
        {
            line.SetPosition(line.positionCount - 1, Vector3.forward * distance);
        }
    }
}