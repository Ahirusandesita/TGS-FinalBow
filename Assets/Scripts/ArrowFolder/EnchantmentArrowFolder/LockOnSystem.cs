// --------------------------------------------------------- 
// LockOnSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public interface IFLockOnSystem
{
    GameObject LockOnTarget { get; set; }
    void TargetLockOn(Transform bowTransform);

}

public class LockOnSystem : MonoBehaviour , IFLockOnSystem
{
    #region variable 
    public GameObject LockOnTarget { get; set; }

    private GameObject _temporaryTarget = default;

    private ArrowEnchantment _enchant = default;

    private LockOnUISystem _lockOnUI = default;

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
                enemys.Add(moves[i].gameObject);
            }
            List<GameObject> searchEnemys = new List<GameObject>();
            searchEnemys = ConeDecision.ConeInObjects(bowTransform, enemys, 60f, 100000f, 1);
            if (searchEnemys.Count == 0)
            {
                print("誰もいない  " + bowTransform.rotation.eulerAngles);
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
                        print("範囲内　：　"+ _temporaryTarget.name);
                    }
                }

                if (_onTarget)
                {
                    _lockOnTime += Time.deltaTime;
                    DestroyUI();
                    if (_lockOnTime > DISITION_TIME && LockOnTarget == null)
                    {
                        LockOnTarget = _temporaryTarget;
                        print("ロックオン完了");
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

    private void DestroyUI()
    {
        try
        {
            _lockOnUI.LockOnEnd();
        }
        catch
        {
            // UIがnothing
        }
    }

 #endregion
}