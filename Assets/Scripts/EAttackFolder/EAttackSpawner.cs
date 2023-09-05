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

    /// <summary>
    /// ステージの正面のベクトル
    /// </summary>
    public Vector3 StageFrontPosition
    {
        set { _stageFrontPosition = value; }
    }

    private void Start()
    {
        _transform = this.transform;
        _playerPlace = GameObject.FindWithTag("PlayerController").transform;

        // 呼び出し漏れを防ぐため、まずは「プレイヤーに攻撃」で初期化
        RotateAttackSpawner(DirectionType_AtAttack.player);
    }

    /// <summary>
    /// 攻撃方向の変更
    /// </summary>
    /// <param name="attackDirection"></param>
    public void RotateAttackSpawner(DirectionType_AtAttack attackDirection)
    {
        switch (attackDirection)
        {
            case DirectionType_AtAttack.player:

                // プレイヤーの方向へ角度を変える
                _transform.LookAt(_playerPlace);

                break;
            case DirectionType_AtAttack.front:

                // ステージの正面方向を向く
                _transform.LookAt(_stageFrontPosition);

                break;
        }
    }
}
