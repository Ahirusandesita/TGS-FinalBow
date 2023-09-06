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
    [SerializeField] float maxRag = 0.5f;

    [SerializeField] int createIndex = 10;

    [SerializeField] LineRenderer line = default;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        float end = line.GetPosition(line.positionCount - 1).z;
        float oneStep = end / line.positionCount;
        for(int i = 1; i < line.positionCount - 1; i++)
        {
            line.SetPosition(i, new Vector3(RamdomValue(), RamdomValue(), oneStep * i));
        }
    }

    private float RamdomValue()
    {
        return Random.Range(-maxRag, maxRag);
    }
}