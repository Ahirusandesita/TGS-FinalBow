// --------------------------------------------------------- 
// HurikoMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class HurikoMove : MonoBehaviour, IFCanTakeArrowButton
{
    #region variable 
    [SerializeField] float _distance = 3f;
    [SerializeField] float _addForce = 30f;
    [SerializeField] float _dicreaceForce = 3f;
    [SerializeField]float force = 0f;
   [SerializeField] Vector3 forceRote = default;
    Transform player = default;
    Transform root = default;
    [SerializeField] Transform ball = default;
    [SerializeField] Vector3 moveRote = default;



    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        player = GameObject.FindWithTag(InhallLibTags.PlayerController).transform;
        root = transform.root;

    }

    private void Update()
    {
        Physics();
    }

    public void ButtonPush()
    {
        force += _addForce;
        forceRote = (root.position - player.position).normalized;
        forceRote.y = 0;
    }

    private void Physics()
    {

        Vector3 sideBallPos = ball.position;
        sideBallPos.y = 0;
        Vector3 sideMyPos = root.position;
        sideMyPos.y = 0;

        float nowDistance = Vector3.Distance(sideMyPos, sideBallPos);

        
        float percent = 1 - (nowDistance / _distance);

        if (force == 0)
        {
            forceRote *= -1;
            force = nowDistance * _addForce;
            if(nowDistance == _distance)
            {
                percent = 1;
            }
        }
        moveRote = (percent) * force * forceRote;

        force -= _dicreaceForce * Time.deltaTime;
       
        
        if (force < 0)
        {
            force = 0;
        }
        if (force > 30)
        {
            force = 30;
        }

        // “®‚­•”•ª
        ball.transform.Translate(Time.deltaTime * moveRote);
        Vector3 distanceY = ball.transform.position;
        distanceY.y = root.position.y - (_distance - nowDistance);
        ball.transform.position = distanceY;


    }

    GameObject IFCanTakeArrowButton.GetThisObject()
    {
        return gameObject;
    }


    #endregion
}