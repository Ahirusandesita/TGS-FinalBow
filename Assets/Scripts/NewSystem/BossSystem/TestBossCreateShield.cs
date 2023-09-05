// --------------------------------------------------------- 
// TestBossCreateShield.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestBossCreateShield : NewTestBossAttackBase
{
    #region variable 

    [SerializeField] float _shieldSpawnMaxDistance = 10f;
    [Tooltip("シールド出現前方の距離")]
    [SerializeField] float _shieldSpawnFowardDistance = 5f;
    [Tooltip("シールド重なりの抑制")]
    [SerializeField] float _shieldCantSpawnArea = 1f;
    [SerializeField] GameObject shieldPrehub = default;
    Transform[] shields = new Transform[NUMBER_OF_SHIELDS];
    List<Vector3> _shieldPositionCache = new ();
    BossAttackTestTest enemyAttack = default;
    Vector3 _shieldSpawnRootPoint = default;

    

    const int NUMBER_OF_SHIELDS = 4;


    #endregion
    #region property
    #endregion

    protected override void AttackAnimation()
    {

    }

    protected override void AttackProcess()
    {
        ShieldAttack();

    }

    protected override void Start()
    {
        base.Start();

        _shieldSpawnRootPoint = transform.position + Vector3.forward * _shieldSpawnFowardDistance;

        enemyAttack = GetComponent<BossAttackTestTest>();

        for(int i = 0;i < NUMBER_OF_SHIELDS; i++)
        {
            shields[i] = Instantiate(shieldPrehub).transform;
            shields[i].gameObject.SetActive(false);
        }
    }

    private void ShieldAttack()
    {
        enemyAttack.OneShot(PoolEnum.PoolObjectType.normalBullet, transform);
        // シールド仮
        for (int i = 0; i < NUMBER_OF_SHIELDS; i++)
        {
            Vector2 randomPoint = SetRamdomPoint();

            _shieldPositionCache.Add(randomPoint);

            shields[i].gameObject.SetActive(true);

            shields[i].position = _shieldSpawnRootPoint + (Vector3)randomPoint;
        }

        _shieldPositionCache = default;
    }

    private Vector2 SetRamdomPoint()
    {
        Vector2 randomPoint = RandomCirclePoint(_shieldSpawnMaxDistance);

        while (!CheckDistance(randomPoint))
        {
            randomPoint = RandomCirclePoint(_shieldSpawnMaxDistance);
        }

        return randomPoint;

        bool CheckDistance(Vector2 randomPoint)
        {
            foreach (Vector3 cache in _shieldPositionCache)
            {
                if (Vector3.Distance(cache, randomPoint) < _shieldCantSpawnArea)
                {
                    return false;
                }
            }
            return true;
        }
    }

    private Vector2 RandomCirclePoint(float maxDistance)
    {
        return UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(0, maxDistance);
    }



}