// --------------------------------------------------------- 
// EAttackMoveBase.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class EAttackMoveBase : MonoBehaviour
{
    public TagObject _PoolSystemTagData = default;

    public TagObject _PlayerControllerTagData = default;


    [Tooltip("取得したObjectPoolSystemクラス")]
    protected ObjectPoolSystem _objectPoolSystem = default;

    [Tooltip("取得したCashObjectInformationクラス")]
    protected CashObjectInformation _cashObjectInformation = default;

    [Tooltip("取得したPlayerHitZoneクラス")]
    protected PlayerHitZone _playerHitZone = default;

    [Tooltip("移動可能フラグ")]
    protected bool _canMove = default;

    [Tooltip("生存時間")]
    protected float _lifeTime = default;

    [Tooltip("自身のTransformのキャッシュ")]
    protected Transform _transform = default;

    [SerializeField, Tooltip("攻撃の速さ")]
    protected float _attackMoveSpeed = 20f;


    /// <summary>
    /// まっすぐ飛ぶ挙動
    /// </summary>
    public virtual void Straight()
    {
        // Z軸方向へ飛ばす
        _transform.Translate(Vector3.forward * _attackMoveSpeed * Time.deltaTime);
    }

    public void CanMove()
    {
        _canMove = false;
    }
}
