// --------------------------------------------------------- 
// ContainObject.cs 
// 
// CreateDay: 2023/07/07
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトがヒットしているか判断するクラス
/// </summary>
public class ContainObject
{
    #region variable 
    /// <summary>
    /// 全フロアクラスのオブジェクト
    /// </summary>
    public static List<Floor> floors = new List<Floor>();

    /// <summary>
    /// 全壁クラスのオブジェクト
    /// </summary>
    public static List<Wall> walls = new List<Wall>();

    /// <summary>
    /// ヒットしているかを判定するためのデリゲート
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    private delegate bool Contain(Vector3 me);

    /// <summary>
    /// 床にヒットしているか
    /// </summary>
    private Contain Contain_Floor;

    /// <summary>
    /// 壁にヒットしているか
    /// </summary>
    private Contain Contain_Wall;

    /// <summary>
    /// 埋まりこみ防止用変数取得デリゲート
    /// </summary>
    /// <returns></returns>
    private delegate float Adjustment();

    /// <summary>
    /// 床に埋まりこまないようにするための変数取得
    /// </summary>
    private Adjustment Adjustment_Floor = default;

    /// <summary>
    /// 壁に埋まりこまないようにするための変数取得
    /// </summary>
    private Adjustment Adjustment_Wall =default;

    #endregion
    #region property
    #endregion
    #region method

    /// <summary>
    /// 床に触れているか
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public bool IsContainObjectFloor(Vector3 me)
    {
        //Contain型のデリゲートがNullではないとき
        if (Contain_Floor != null)
        {
            //前回触れていた空間に触れているか
            //触れていたらそれ以外の空間を検知する必要がないのでTrueを返す　オブジェクト分比較せずに済む
            if (Contain_Floor(me))
            {
                return true;
            }
        }

        //触れていなかったらすべてのオブジェクトの中から触れているものがあるか検索する
        for (int i = 0; i < floors.Count; i++)
        {
            //触れている空間があればそれを次から比較するために代入する　Trueを返す
            if (floors[i].IsHit(me))
            {
                Contain_Floor = new Contain(floors[i].IsHit);
                Adjustment_Floor = new Adjustment(floors[i].PositionAdjustment);
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// 壁に触れているか
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public bool IsContainObjectWall(Vector3 me)
    {
        if (IsContain(ref Contain_Wall, me, walls, ref Adjustment_Wall))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 埋まりこみ防止用の変数を取得する
    /// </summary>
    /// <returns></returns>
    public float GetAdjustmentPosition_Floor()
    {
        return Adjustment_Floor();
    }

    /// <summary>
    /// 埋まりこみ防止用の変数を取得する
    /// </summary>
    /// <returns></returns>
    public float GetAdjustmentPosition_Wall()
    {
        return Adjustment_Wall();
    }


    /// <summary>
    /// 無駄にすべてのオブジェクトを検索して比較しないようにするために使う関数
    /// </summary>
    /// <param name="contain"></param>
    /// <param name="me"></param>
    /// <param name="colliderObjectBases"></param>
    /// <returns></returns>
    private bool IsContain(ref Contain contain,Vector3 me,List<Floor> colliderObjectBases,ref Adjustment adjustment)
    {
        //Contain型のデリゲートがNullではないとき
        if(contain != null)
        {
            //前回触れていた空間に触れているか
            //触れていたらそれ以外の空間を検知する必要がないのでTrueを返す　オブジェクト分比較せずに済む
            if (contain(me))
            {
                return true;
            }
        }

        //触れていなかったらすべてのオブジェクトの中から触れているものがあるか検索する
        for (int i = 0; i < colliderObjectBases.Count; i++)
        {
            //触れている空間があればそれを次から比較するために代入する　Trueを返す
            if (colliderObjectBases[i].IsHit(me))
            {
                contain = new Contain(colliderObjectBases[i].IsHit);
                adjustment = new Adjustment(colliderObjectBases[i].PositionAdjustment);
                return true;
            }
        }
        return false;
    }
    private bool IsContain(ref Contain contain, Vector3 me, List<Wall> colliderObjectBases, ref Adjustment adjustment)
    {
        //Contain型のデリゲートがNullではないとき
        if (contain != null)
        {
            //前回触れていた空間に触れているか
            //触れていたらそれ以外の空間を検知する必要がないのでTrueを返す　オブジェクト分比較せずに済む
            if (contain(me))
            {
                return true;
            }
        }

        //触れていなかったらすべてのオブジェクトの中から触れているものがあるか検索する
        for (int i = 0; i < colliderObjectBases.Count; i++)
        {
            //触れている空間があればそれを次から比較するために代入する　Trueを返す
            if (colliderObjectBases[i].IsHit(me))
            {
                contain = new Contain(colliderObjectBases[i].IsHit);
                adjustment = new Adjustment(colliderObjectBases[i].PositionAdjustment);
                return true;
            }
        }
        return false;
    }



    #endregion
}