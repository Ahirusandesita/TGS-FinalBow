// --------------------------------------------------------- 
// LockOnSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

public interface IFLockOnSystem
{
    GameObject LockOnTarget { get; set; }
    void TargetLockOn(Transform bowTransform);

    void DestroyUI();
}

public class LockOnSystem : MonoBehaviour , IFLockOnSystem
{
    #region variable 
    public GameObject LockOnTarget { get; set; }

    private GameObject _temporaryTarget = default;

    private ArrowEnchantment _enchant = default;

    public LockOnUISystem _lockOnUI { get; set; }

    private LayerMask _enemyLayer = 1 << 6;

    private float _lockOnTime = default;

    private float _distance = default;

    private float _mostNearDistance = default;

    private bool _onTarget = false;



    private const float DISITION_TIME = 1f;

    private const float INFINITY = Mathf.Infinity;
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _enchant = GameObject.FindGameObjectWithTag(InhallLibTags.ArrowEnchantmentController).GetComponent<ArrowEnchantment>();
    }

    public void TargetLockOn(Transform bowTransform)
    {
        if (_enchant.EnchantmentNowState == EnchantmentEnum.EnchantmentState.homing)
        {
            EnemyStats[] moves = GameObject.FindObjectsOfType<EnemyStats>();
            List<GameObject> enemys = new List<GameObject>();
            for (int i = 0; i < moves.Length; i++)
            {
                if ((1 << moves[i].gameObject.layer & _enemyLayer.value) != 0 && moves[i].gameObject.activeSelf == true)
                {
                    enemys.Add(moves[i].gameObject);
                }
            }
            List<GameObject> searchEnemys = new List<GameObject>();
            searchEnemys = ConeDecision.ConeInObjects(bowTransform, enemys, 30f, 100000f, 1);
            if (searchEnemys.Count == 0)
            {
                ReSetTarget();
                return;
            }
            else
            {
                if (_temporaryTarget == null)
                {
                    ReSetTarget();
                    for (int i = 0; i < searchEnemys.Count; i++)
                    {
                        _distance = Vector3.Distance(searchEnemys[i].transform.position, bowTransform.position);
                        if (_distance < _mostNearDistance)
                        {
                            _temporaryTarget = searchEnemys[i];
                            _mostNearDistance = _distance;
                        }
                    }
                    _mostNearDistance = INFINITY;
                    _lockOnTime = 0f;
                    try
                    {
                        _lockOnUI = _temporaryTarget.gameObject.GetComponent<LockOnUISystem>();
                    }
                    catch
                    {
                        _lockOnUI = null;
                    }
                }

                _onTarget = false;

                for (int i = 0; i < searchEnemys.Count; i++)
                {
                    if (_temporaryTarget == searchEnemys[i])
                    {
                        _onTarget = true;
                        i = searchEnemys.Count;
                    }
                }

                if (_onTarget)
                {
                    _lockOnTime += Time.deltaTime;
                    try
                    {
                        _lockOnUI.LockOnNow(_lockOnTime);
                    }
                    catch
                    {
                    }
                    if (_lockOnTime > DISITION_TIME && LockOnTarget == null)
                    {
                        LockOnTarget = _temporaryTarget;
                    }
                }
                else
                {
                    ReSetTarget();
                }
            }
        }
    }

    private void ReSetTarget()
    {
        DestroyUI();
        _temporaryTarget = null;
        LockOnTarget = null;
    }

    public GameObject TargetSet(ArrowMove arrow)
    {
        _temporaryTarget = null;
        return LockOnTarget;
    }

    public void DestroyUI()
    {
        try
        {
            _lockOnUI.LockOnEnd();
        }
        catch
        {
            // UI‚ªnothing
        }
    }

 #endregion
}