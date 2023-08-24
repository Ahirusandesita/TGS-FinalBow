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
    [Tooltip("軸")]
    [SerializeField] Vector3 axis = Vector3.up;
    [Tooltip("回転スピード")]
    [SerializeField] float turnSpeed = 45f;
    [Tooltip("最大角度")]
    [SerializeField] float limitAngle = 180f;
    Quaternion endRotation = default;
    float movedAngle = 0f;
    bool isFinish = false;
    bool moving = false;
    public bool IsFinish => isFinish;
    public bool Moving => moving;

    private void Start()
    {
        endRotation = transform.rotation * Quaternion.AngleAxis(limitAngle, axis);
    }
    public void GimmickAction()
    {
        moving = true;
        float move = turnSpeed * Time.deltaTime;
        transform.Rotate(axis,move );
        movedAngle += move;
        if(movedAngle > limitAngle)
        {
            transform.rotation = endRotation;
        }
    }
}