// --------------------------------------------------------- 
// UpdateLineRenderIndex.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class UpdateLineRenderIndex : MonoBehaviour
{
    float maxRag = 1f;

    [SerializeField] float ragRangePercent = 0.3f;

    LineRenderer line = default;

    const float needTime = 0.1f;

    float cacheTime = 0f;

    float oneIndexDistance = 0f;

    private void OnEnable()
    {
        line = GetComponent<LineRenderer>();

        cacheTime = 0;

        float distance = line.GetPosition(line.positionCount - 1).z;

        oneIndexDistance = distance / line.positionCount;

        maxRag = distance * ragRangePercent;

    }
    private void Update()
    {
        if (cacheTime + needTime < Time.time)
        {
            
            cacheTime = Time.time;
            for (int i = 1; i < line.positionCount - 1; i++)
            {
                line.SetPosition(i, new Vector3(RamdomValue(), RamdomValue(), oneIndexDistance * i));
            }
            
        }
    }

    private float RamdomValue()
    {
        return Random.Range(-maxRag, maxRag);
    }
}