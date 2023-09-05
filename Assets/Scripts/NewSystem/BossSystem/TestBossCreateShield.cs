// --------------------------------------------------------- 
// TestBossCreateShield.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestBossCreateShield : NewTestBossAttackBase
{
    #region variable 

    [SerializeField] float _shieldSpawnMaxDistance = 10f;
    [Tooltip("シールド出現前方の距離")]
    [SerializeField] float _shieldSpawnFowardDistance = 5f;
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

    }

    protected override void Start()
    {
        base.Start();
        _shieldSpawnRootPoint = transform.position + Vector3.forward * _shieldSpawnFowardDistance;
    }

    private void ShieldAttack()
    {
            // シールド仮
            for (int i = 0; i < NUMBER_OF_SHIELDS; i++)
            {
                Transform shield = default;
                shield = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
                Vector2 randomPoint = RandomCirclePoint(_shieldSpawnMaxDistance);
                shield.position = _shieldSpawnRootPoint + (Vector3)randomPoint;
            }

            // 右に行くか左に行くかの処理
        
    }

    private Vector2 RandomCirclePoint(float maxDistance)
    {
        return UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(0, maxDistance);
    }



}