// --------------------------------------------------------- 
// BirdMoveFirst.cs 
// 
// CreateDay: 2023/06/21
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

public class BirdMoveFirst : BirdMoveBase
{
    protected override void EachMovement(ref float movedDistance)
    {
        // 終了条件分岐
        base.EachMovement(ref movedDistance);
        
        // 移動する（移動方向のベクトル * 移動速度）
        _transform.Translate((_goalPosition - _startPosition).normalized * _movementSpeed * Time.deltaTime, Space.World); 　// 第二引数ないとバグる
        // 移動量を加算
        movedDistance += ((_goalPosition - _startPosition).normalized * _movementSpeed * Time.deltaTime).magnitude;
    }
}