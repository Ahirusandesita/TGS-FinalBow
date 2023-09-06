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

    const int oneIndexDistance = 3;
    public void SetControllLine(GameObject obj)
    {
        lines = obj.GetComponentsInChildren<LineRenderer>();
    }

    public void LineSetIndex(float distance)
    {
        foreach(LineRenderer line in lines)
        {
            float end = line.GetPosition(line.positionCount - 1).z;

            int createIndex = (int)((end / oneIndexDistance) + 1);

            line.positionCount = createIndex;
            line.SetPosition(line.positionCount - 1, Vector3.forward * distance);
        }
    }
}