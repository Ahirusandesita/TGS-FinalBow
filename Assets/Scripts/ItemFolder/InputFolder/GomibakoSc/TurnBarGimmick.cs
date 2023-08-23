// --------------------------------------------------------- 
// turnBarGimmick.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TurnBarGimmick : MonoBehaviour, IFGimmickCaller
{
    [SerializeField] Vector3 axis = Vector3.up;
    [SerializeField] float turnSpeed = 45f;
    bool isFinish = false;
    public bool IsFinish => isFinish;

    private Quaternion rotationValue = default;
    private void Start()
    {
        Init();
    }

    private void OnValidate()
    {
        Init();
    }
    private void Init()
    {
        rotationValue = Quaternion.AngleAxis(turnSpeed,axis);
    }
    public void GimmickAction()
    {
        
    }
}