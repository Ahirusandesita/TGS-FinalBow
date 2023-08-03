// --------------------------------------------------------- 
// HurikoMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class HurikoMove : MonoBehaviour,IFCanTakeArrowButton
{
    #region variable 
    [SerializeField] float _distance = 3f;
    [SerializeField] float _addForce = 3f;
    [SerializeField] float _dicreaceForce = 3f;
    float force = 0f;
    Vector3 forceRote = default;
    Transform player = default;
    [SerializeField]Transform ball = default;
    Vector3 moveRote = default;



    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        player = GameObject.FindWithTag(InhallLibTags.PlayerController).transform;
        
    }

    private void Update()
    {
        Physics();
    }

    public void ButtonPush()
    {
        force += _addForce;
        forceRote = (transform.position - player.position).normalized;
        forceRote.y = 0;
    }

    private void Physics()
    {
           
    }
    #endregion
}