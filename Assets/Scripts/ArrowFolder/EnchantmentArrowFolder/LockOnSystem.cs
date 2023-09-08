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
    void TargetLockOn(Vector3 bowPosition , Vector3 bowRotation);

}

public class LockOnSystem : MonoBehaviour , IFLockOnSystem
{
    #region variable 
    public GameObject LockOnTarget { get; set; }

    private GameObject _temporaryTarget = default;

    private ArrowEnchantment _enchant = default;

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

    public void TargetLockOn(Vector3 bowPosition , Vector3 bowRotation)
    {
        if (_enchant.EnchantmentNowState == EnchantmentEnum.EnchantmentState.homing)
        {
            EnemyStats[] moves = GameObject.FindObjectsOfType<EnemyStats>();
            List<GameObject> enemys = new List<GameObject>();
            if (enemys.Count == 0)
            {
                ReSetTarget();
                return;
            }
            else
            {
                if (_temporaryTarget == null)
                {
                    for (int i = 0; i < enemys.Count; i++)
                    {
                        _distance = Vector3.Distance(enemys[i].transform.position, bowPosition);
                        if (_distance < _mostNearDistance)
                        {
                            _temporaryTarget = enemys[i];
                            _mostNearDistance = _distance;
                        }
                    }
                    _mostNearDistance = INFINITY;
                    _lockOnTime = 0f;
                }

                _onTarget = false;

                for (int i = 0; i < enemys.Count; i++)
                {
                    if (_temporaryTarget == enemys[i])
                    {
                        _onTarget = true;
                        i = enemys.Count;
                        print("範囲内　：　"+ _temporaryTarget.name);
                    }
                }

                if (_onTarget)
                {
                    _lockOnTime += Time.deltaTime;
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
        _temporaryTarget = null;
        LockOnTarget = null;
    }

    public GameObject TargetSet(ArrowMove arrow)
    {
        return LockOnTarget;
    }

 #endregion
}