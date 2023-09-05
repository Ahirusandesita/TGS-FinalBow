// --------------------------------------------------------- 
// TestBossTeleport.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestBossTeleport : NewTestBossMoveBase
{
    [Tooltip("ÉèÅ[Évãxåeéûä‘")]
    [SerializeField] float _teleportationStopTime;
    [Tooltip("ÉèÅ[Évâ~îºåa")]
    [SerializeField] float _teleportRange;
    [Tooltip("ÉèÅ[Évâ~íÜêSãóó£")]
    [SerializeField] float _teleportMiddleDistance;

    const int NUMBER_OF_TELEPORTS = 4;

    Vector3 _teleportStartPosition = default;
    Vector3 _rightTeleportPoint = default;
    Vector3 _leftTeleportPoint = default;

    int teleportCount = 0;

    float _cacheTime = 0f;

    bool _isStart = true;


    protected override void Start()
    {
        base.Start();
        _rightTeleportPoint = transform.position + Vector3.right * _teleportMiddleDistance;
        _leftTeleportPoint = transform.position + Vector3.left * _teleportMiddleDistance;
        _isStart = true;
    }
    protected override void MoveAnimation()
    {

    }

    protected override void MoveProcess()
    {
        WarpStart();

        Telepotation();
    }

    private void WarpStart()
    {
        if (_isStart)
        {
            _isStart = false;
            teleportCount = 0;

            _teleportStartPosition = transform.position;
        }
    }
    private void Telepotation()
    {

        if(_cacheTime + _teleportationStopTime < Time.time)
        {
            if(teleportCount >= NUMBER_OF_TELEPORTS)
            {
                transform.position = _teleportStartPosition;
                isMove.Value = false;
                _isStart = true;
                return;
            }
            Vector2 teleportPoint = RandomCirclePoint(_teleportRange);

            if (teleportCount % 2 == 0)
            {
                transform.position = _rightTeleportPoint + (Vector3)teleportPoint;
            }
            else
            {
                transform.position = _leftTeleportPoint + (Vector3)teleportPoint;
            }

            teleportCount++;

            _cacheTime = Time.time;
        }
    }
       

    private Vector2 RandomCirclePoint(float maxDistance)
    {
        return UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(0, maxDistance);
    }
}
