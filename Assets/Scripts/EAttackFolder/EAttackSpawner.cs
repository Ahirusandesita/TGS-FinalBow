// --------------------------------------------------------- 
// EAttackSpawner.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;

public class EAttackSpawner : MonoBehaviour
{
    [Tooltip("プレイヤーのTransform")]
    private Transform _playerPlace = default;

    [Tooltip("自身のTransformのキャッシュ")]
    private Transform _transform = default;

    [Tooltip("ステージの正面のベクトル")]
    private Vector3 _stageFrontPosition = default;

    [Tooltip("攻撃方向")]
    private DirectionType_AtAttack _attackDirection = default;

    /// <summary>
    /// スポナーの回転が完了
    /// </summary>
    private bool IsCompletedSpawnerRotate { get; }

    /// <summary>
    /// ステージの正面のベクトル
    /// </summary>
    public Vector3 StageFrontPosition
    {
        set { _stageFrontPosition = value; }
    }

    /// <summary>
    /// 攻撃方向
    /// </summary>
    public DirectionType_AtAttack AttackDirection
    {
        set { _attackDirection = value; }
    }

    private void Start()
    {
        _transform = this.transform;
        _playerPlace = GameObject.FindWithTag("PlayerController").transform;

        // 呼び出し漏れを防ぐため、まずは「プレイヤーに攻撃」で初期化
        //RotateAttackSpawner(DirectionType_AtAttack.player);
    }

    private void Update()
    {
        RotateAttackSpawner();
    }

    /// <summary>
    /// 攻撃方向の変更
    /// </summary>
    private void RotateAttackSpawner()
    {
        switch (_attackDirection)
        {
            case DirectionType_AtAttack.player:

                // プレイヤーの方向へ角度を変える
                _transform.LookAt(_playerPlace);

                break;
            case DirectionType_AtAttack.front:

                // ステージの正面方向を向く
                _transform.rotation = Quaternion.LookRotation(_stageFrontPosition, Vector3.up);

                break;
        }
    }
}
