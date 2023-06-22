// --------------------------------------------------------- 
// BirdMoveSecond.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class BirdMoveSecond : BirdMoveBase
{
    #region variable 

    Vector3 _sideMoveVector = default;

    float _moveY = default;

    float _sideMoveSpeed = default;

    float _moveSpeedY = default;

    bool _moveDown = default;

    #endregion
    #region property
    #endregion
    #region method

    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeVariables();
        // 最初から正面を向かせる
        _transform.rotation = Quaternion.Euler(Vector3.up * 180f);
        // 横移動ベクトル
        _sideMoveVector = GetSideMoveVector();
        // 下か上か決める
        _moveDown = CheckDown();
    }


    protected override void InitializeVariables()
    {
        _needRotateTowardDirectionOfTravel = false;
    }

    // スタートからゴールへいく
    // 左行き:下　右行き:上
    public override void MoveSequence()
    {
        
    }

    /// <summary>
    /// カメラのポジションから見て左に行くか調べる
    /// </summary>
    /// <returns>左行きならtrue</returns>
    private bool CheckDown()
    {
        // 敵からプレイヤーの方向ベクトル
        Vector3 enemyToPlayerVector = Camera.main.transform.position - _transform.position;

        // 前方ベクトルと敵とプレイヤー方向ベクトルの外積をもとめる
        Vector3 cross = Vector3.Cross(_transform.forward, enemyToPlayerVector);

        // 外積から左右判定
        if(cross.y < 0)
        {
            // 敵から見て右にプレイヤーがいるので下に行く
            return true;
        }

        return false;
    }

    /// <summary>
    /// 横移動のベクトルをもとめる
    /// </summary>
    /// <returns>横移動ベクトル</returns>
    private Vector3 GetSideMoveVector()
    {
        return _goalPosition - _startPosition;
    }

    private bool CheckMoveFinish()
    {
        return false;
    }
    #endregion
}