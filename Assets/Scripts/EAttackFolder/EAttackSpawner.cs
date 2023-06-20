// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Takayanagi
// --------------------------------------------------------- 
using UnityEngine;

public class EAttackSpawner : MonoBehaviour
{
    [Tooltip("プレイヤーのTransform")]
    public Transform playerPlace = default;

    [Tooltip("自身のTransformのキャッシュ")]
    private Transform _transform = default;

    private void Start()
    {
        _transform = this.transform;
    }

    private void Update()
    {
        // 本番はイベントフラグもしくは移動フラグが立ったら
        //if (Input.GetKeyDown(KeyCode.U))
        //{
            // プレイヤーの方向へ角度を変える
            _transform.LookAt(playerPlace);
        //}
    }
}
