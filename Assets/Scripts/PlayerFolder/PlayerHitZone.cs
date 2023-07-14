// --------------------------------------------------------- 
// PlayerDamageZone.cs 
// 
// CreateDay: 2023/06/18
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class PlayerHitZone : MonoBehaviour
{
    #region variable 
    //プレイヤーの座標
    public Transform _playerTransform = default;
    /// <summary>
    /// 当たり判定のサイズ
    /// </summary>
    public float _hitZoneScale;


    private PlayerStats _playerStats;

    private HitZone hitZone;
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        hitZone = new HitZone(_hitZoneScale,_playerTransform.position);


        if (GameObject.FindGameObjectsWithTag("CanvasController").Length == 0)
        {
            enabled = false;
        }
        _playerStats = this.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        hitZone.SetPosition(_playerTransform.position);
    }
    public void HitZone(Vector3 attackPosition)
    {
        if (hitZone.IsHit(attackPosition))
        {
            _playerStats.PlayerDamage(1);
        }
    }
    #endregion
}