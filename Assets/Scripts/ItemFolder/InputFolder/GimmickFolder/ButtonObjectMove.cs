// --------------------------------------------------------- 
// ButtonObjectMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
/// <summary>
/// 使い方
/// トランスフォームを登録する
/// ボタンをおす
/// 終わり
/// 拡張性皆無
/// </summary>
public class ButtonObjectMove : MonoBehaviour, IFCanTakeArrowButton
{
    [Tooltip("動き始める位置")]
    [SerializeField] Transform startTransform = default;
    [Tooltip("終了位置")]
    [SerializeField] Transform goalTransform = default;
    [Tooltip("動くトランスフォーム")]
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

    GameObject IFCanTakeArrowButton.GetThisObject()
    {
        return gameObject;
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