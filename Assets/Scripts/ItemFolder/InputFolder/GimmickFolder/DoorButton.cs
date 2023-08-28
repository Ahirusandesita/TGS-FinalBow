// --------------------------------------------------------- 
// DoorButton.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
/// <summary>
/// 使い方
/// door(動かしたいトランスフォーム)を設定
/// てかこれいる？
/// </summary>
public class DoorButton : MonoBehaviour, IFCanTakeArrowButton
{
    #region variable 
    [SerializeField] Transform leftDoor = default;
    [SerializeField] Transform rightDoor = default;
    [SerializeField] float moveDistance = 5f;
    [SerializeField] float moveSpeed = 2.5f;
    bool canMove = true;
    bool canPush = true;
    readonly WaitForFixedUpdate wait = new WaitForFixedUpdate();
    #endregion

    #region method


    #endregion
    public void ButtonPush()
    {
        if(canPush)
        StartCoroutine(DoorMove());
    }

    private IEnumerator DoorMove()
    {
        canPush = false;
        float move = moveSpeed * Time.fixedDeltaTime;
        float movedDistance = 0f;
        while (canMove)
        {
            yield return wait;
            leftDoor.Translate(Vector3.left * move);
            rightDoor.Translate(Vector3.right * move);
            movedDistance += move;
            if(movedDistance > moveDistance)
            {
                canMove = false;
            }
        }
    }

    GameObject IFCanTakeArrowButton.GetThisObject()
    {
        return gameObject;
    }
}