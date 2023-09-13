// --------------------------------------------------------- 
// StringCreater.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class StringCreater : MonoBehaviour
{
    [SerializeField]
    private Transform _stringOver = default;
    [SerializeField]
    private Transform _stringCenter = default;
    [SerializeField]
    private Transform _stringUnder = default;
    [SerializeField]
    private LineRenderer _string;

    private Vector3 _OverPosition, _CenterPosition, _UnderPosition;

    private void Awake()
    {

    }
 
    private void Start ()
    {
        _string.positionCount = 3;
    }

    private void Update ()
    {
        _string.SetPosition(0, _stringOver.position);
        _string.SetPosition(1, _stringCenter.position);
        _string.SetPosition(2, _stringUnder.position);
    }
}