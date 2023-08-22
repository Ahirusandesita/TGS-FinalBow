// --------------------------------------------------------- 
// ButtonObjectMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ButtonObjectMove : MonoBehaviour, IFCanTakeArrowButton
{
    [SerializeField] Transform startTransform = default;
    [SerializeField] Transform goalTransform = default;
    [SerializeField] Transform moveTransform = default;
    [SerializeField] float moveSpeed = 5f;
    bool moving = false;
    Vector3 moveVec = default;
    Vector3 startPosition = default;
    Vector3 goalPosition = default;
    float distance = default;
    WaitForFixedUpdate wait = new WaitForFixedUpdate();

    public void ButtonPush()
    {
        if (moving == false)
        {
            moving = true;
            startPosition = startTransform.position;
            moveTransform.position = startPosition;
            goalPosition = goalTransform.position;
            Vector3 direction = goalPosition - startPosition;
            moveVec = direction.normalized;
            distance = direction.magnitude;
            StartCoroutine(Move());

        }
    }

    IEnumerator Move()
    {
        float movedDistance = 0f;
        Vector3 calcMove = moveSpeed * Time.fixedDeltaTime * moveVec;
        float calcMoveDistance = calcMove.magnitude;

        while (movedDistance < distance)
        {
            moveTransform.Translate(calcMove);
            movedDistance += calcMoveDistance;
            yield return wait;

        }

        transform.position = goalPosition;
    }
}