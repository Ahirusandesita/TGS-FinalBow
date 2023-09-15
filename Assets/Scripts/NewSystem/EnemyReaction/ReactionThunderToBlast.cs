// --------------------------------------------------------- 
// ReactionThunderToBlast.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ReactionThunderToBlast : MonoBehaviour, InterfaceReaction.IThunderReaction
{

    #region variable 
    [SerializeField] GameObject effect = default;
    [SerializeField] Transform blastPosition = default;

    const float PARALYSIS_TIME = 1f;

    
    Animator anim = default;
    CommonEnemyStats stats = default;
    ObjectPoolSystem pool = default;

    WaitForSeconds wait = new(PARALYSIS_TIME);
    bool _end = false;
    bool _isStart = false; 
    public bool ReactionEnd { get => _end; set => _end = value; }



    #endregion

    #region method

    private void Awake()
    {
        anim = GetComponent<Animator>();

        stats = GetComponent<CommonEnemyStats>();

        pool = FindObjectOfType<ObjectPoolSystem>();

        if(blastPosition is null)
        {
            blastPosition = transform;
        }
    }

    private void OnEnable()
    {
        _end = false;
        _isStart = false;
        effect.SetActive(false);
    }
   

    public bool IsComplete()
    {
        return ReactionEnd;
    }

    public void Reaction(Transform t1, Vector3 t2)
    {
        if (_isStart)
        {
            return;
        }
        _isStart = true;
        
        StartCoroutine(Action());
    }
    
    private IEnumerator Action() 
    {
        anim.SetTrigger("Thunder");
      
        effect.SetActive(true);
        yield return wait;
        pool.CallObject(EffectPoolEnum.EffectPoolState.thunderBlast, blastPosition.position);
        effect.SetActive(false);
        _end = true;
        _isStart = false;

    }
    #endregion
}