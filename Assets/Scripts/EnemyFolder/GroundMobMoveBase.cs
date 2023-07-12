// --------------------------------------------------------- 
// GroundMobMoveBase.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

/// <summary>
/// 地上雑魚の挙動の基盤
/// </summary>
public class GroundMobMoveBase : EnemyMoveBase
{


    private void Start()
    {
        
    }

    private void Update()
    {
        MoveSequence();
    }


    protected override void MoveSequence()
    {

    }

    private void Jump()
    {

    }

    private void CrabWalk()
    {

    }

    private void Stop()
    {

    }

    public override void OrignalOnCollisionEnter_HitFloor()
    {

    }

    public override void OrignalOnCollisionEnter()
    {

    }



    // 垂直ジャンプ
    // 前方に歩いてくる
    // 攻撃する（別スクリプトから呼ぶので保留）
    // 止まる
    // ↑↑↑それぞれ関数にまとめる
    // MoveSequenceは一旦ほっといていいよ
}