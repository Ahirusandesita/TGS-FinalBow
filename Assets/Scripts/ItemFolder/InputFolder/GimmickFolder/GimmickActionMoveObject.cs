// --------------------------------------------------------- 
// GimmickActionCaller.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
/// <summary>
/// ボタンとかプロペラでも呼べるやつ
/// </summary>
public interface IFGimmickCaller
{
    void GimmickAction();

    bool IsFinish { get; }

    bool Moving { get; }
}

public interface IFGimmickCallerUsePower : IFGimmickCaller
{
    void GimmickAction(float power);
}
public class GimmickActionMoveObject : MonoBehaviour, IFGimmickCallerUsePower
{
    [SerializeField] Transform moveTransform = default;
    [SerializeField] Transform startTransform = default;
    [SerializeField] Transform goalTransform = default;
    [SerializeField] float moveSpeed = 5f;
    public bool IsFinish => isFinish;
    public bool Moving => moving;
    bool isFinish = false;
    bool moving = false;
    Vector3 moveVec = default;
    Vector3 startPosition = default;
    Vector3 goalPosition = default;
    float distance = default;
    float movedDistance = 0f;
    Vector3 calcMove = default;
    float calcMoveDistance = default;

    private void Start()
    {

        startPosition = startTransform.position;
        moveTransform.position = startPosition;
        goalPosition = goalTransform.position;
        Vector3 direction = goalPosition - startPosition;
        moveVec = direction.normalized;
        distance = direction.magnitude;
        calcMove = moveSpeed * Time.deltaTime * moveVec;
        calcMoveDistance = calcMove.magnitude;

    }

    void Move()
    {
        moveTransform.Translate(calcMove);
        movedDistance += calcMoveDistance;

        CheckEnd();
    }


    void Move(float power)
    {
        moveTransform.Translate(calcMove * power);
        movedDistance += calcMoveDistance;

        CheckEnd();
    }
    private void CheckEnd()
    {
        moving = true;
        if (movedDistance > distance)
        {
            transform.position = goalPosition;
            isFinish = true;
        }
    }
    public void GimmickAction()
    {
        Move();
    }

    public void GimmickAction(float power)
    {
        Move(power);
    }
}