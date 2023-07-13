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
    public static List<OriginalCollider> originalColliders = new List<OriginalCollider>();

    /// <summary>
    /// ヒットしているかを判定するためのデリゲート
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    private delegate bool Contain(Vector3 me);

    /// <summary>
    /// 床にヒットしているか
    /// </summary>
    private Contain Contain_Collider;

    private delegate bool ContainAll(Vector3[] mes);

    private ContainAll Contains;

    /// <summary>
    /// 埋まりこみ防止用変数取得デリゲート
    /// </summary>
    /// <returns></returns>
    private delegate ColliderObjectBase.AdjustmentPosint Adjustment();

    /// <summary>
    /// 床に埋まりこまないようにするための変数取得
    /// </summary>
    private Adjustment Adjustment_Collider = default;

    private delegate float AdjustmentAll(Vector3 me);

    private AdjustmentAll AdjustmentY;
    private AdjustmentAll AdjustmentX;
    private AdjustmentAll AdjustmentZ;

    private delegate HitZone.HitDistanceScale ColliderScale();

    private ColliderScale Scale;

    private delegate int ColliderObjectLayer();
    ColliderObjectLayer ColliderObjectLayerNumber;

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
        if (Contain_Collider != null)
        {
            //前回触れていた空間に触れているか
            //触れていたらそれ以外の空間を検知する必要がないのでTrueを返す　オブジェクト分比較せずに済む
            if (Contain_Collider(me))
            {
                return true;
            }
        }

        //触れていなかったらすべてのオブジェクトの中から触れているものがあるか検索する
        for (int i = 0; i < originalColliders.Count; i++)
        {
            //触れている空間があればそれを次から比較するために代入する　Trueを返す
            if (originalColliders[i].IsHit(me))
            {
                Contain_Collider = new Contain(originalColliders[i].IsHit);
                Adjustment_Collider = new Adjustment(originalColliders[i].PositionAdjustmentPoint);

                Contains = new ContainAll(originalColliders[i].IsHit2);
                AdjustmentY = new AdjustmentAll(originalColliders[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(originalColliders[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(originalColliders[i].PushOutFromColliderZ);
                Scale = new ColliderScale(originalColliders[i].GetDistanceScale);
                ColliderObjectLayerNumber = new ColliderObjectLayer(originalColliders[i].GetGameObjectLayer);

                return true;
            }
        }
        return false;
    }
    public bool IsContainObjectTrigger(Vector3 me)
    {
        for (int i = 0; i < originalColliders.Count; i++)
        {
            //触れている空間があればそれを次から比較するために代入する　Trueを返す
            if (originalColliders[i].IsHit(me))
            {
                return true;
            }
        }
        return false;
    }


    public bool IsContainObjectFloor2(Vector3[] mes)
    {
        if(Contains != null)
        {
            if (Contains(mes))
            {
                return true;
            }
        }

        for (int i = 0; i < originalColliders.Count; i++)
        {
            //触れている空間があればそれを次から比較するために代入する　Trueを返す
            if (originalColliders[i].IsHit2(mes))
            {
                Contain_Collider = new Contain(originalColliders[i].IsHit);
                Adjustment_Collider = new Adjustment(originalColliders[i].PositionAdjustmentPoint);

                Contains = new ContainAll(originalColliders[i].IsHit2);
                AdjustmentY = new AdjustmentAll(originalColliders[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(originalColliders[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(originalColliders[i].PushOutFromColliderZ);
                Scale = new ColliderScale(originalColliders[i].GetDistanceScale);
                ColliderObjectLayerNumber = new ColliderObjectLayer(originalColliders[i].GetGameObjectLayer);


                return true;
            }
        }
        return false;

    }

    public bool IsContainObjectFloor3(Vector3 point,Vector3 size)
    {

        for (int i = 0; i < originalColliders.Count; i++)
        {
            //触れている空間があればそれを次から比較するために代入する　Trueを返す
            if (originalColliders[i].IsHit3(point,size))
            {
                Contain_Collider = new Contain(originalColliders[i].IsHit);
                Adjustment_Collider = new Adjustment(originalColliders[i].PositionAdjustmentPoint);

                Contains = new ContainAll(originalColliders[i].IsHit2);
                AdjustmentY = new AdjustmentAll(originalColliders[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(originalColliders[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(originalColliders[i].PushOutFromColliderZ);
                Scale = new ColliderScale(originalColliders[i].GetDistanceScale);
                ColliderObjectLayerNumber = new ColliderObjectLayer(originalColliders[i].GetGameObjectLayer);


                return true;
            }
        }
        return false;
    }


    public bool IsNowContainAll(Vector3 me)
    {
        if (Contain_Collider != null)
        {
            //前回触れていた空間に触れているか
            //触れていたらそれ以外の空間を検知する必要がないのでTrueを返す　オブジェクト分比較せずに済む
            if (Contain_Collider(me))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsContainObjectAll(Vector3[] mes)
    {
        //Contain型のデリゲートがNullではないとき
        if (Contains != null)
        {
            //前回触れていた空間に触れているか
            //触れていたらそれ以外の空間を検知する必要がないのでTrueを返す　オブジェクト分比較せずに済む
            if (Contains(mes))
            {
                return true;
            }
        }

        //触れていなかったらすべてのオブジェクトの中から触れているものがあるか検索する
        for (int i = 0; i < originalColliders.Count; i++)
        {
            //触れている空間があればそれを次から比較するために代入する　Trueを返す
            if (originalColliders[i].IsHit2(mes))
            {
                Contains = new ContainAll(originalColliders[i].IsHit2);
                AdjustmentY = new AdjustmentAll(originalColliders[i].PushOutFromColliderY);
                AdjustmentX = new AdjustmentAll(originalColliders[i].PushOutFromColliderX);
                AdjustmentZ = new AdjustmentAll(originalColliders[i].PushOutFromColliderZ);
                return true;
            }
        }
        return false;
    }




   

    /// <summary>
    /// 埋まりこみ防止用の変数を取得する
    /// </summary>
    /// <returns></returns>
    public ColliderObjectBase.AdjustmentPosint GetAdjustmentPosition_Floor()
    {
        return Adjustment_Collider();
    }


    /// <summary>
    /// Y軸のコライダーサイズ
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public float GetAdjustmentY(Vector3 me)
    {
        return AdjustmentY(me);
    }

    /// <summary>
    /// X軸のコライダーサイズ
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public float GetAdjustmentX(Vector3 me)
    {
        return AdjustmentX(me);
    }

    /// <summary>
    /// Z軸のコライダーサイズ
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public float GetAdjustmentZ(Vector3 me)
    {
        return AdjustmentZ(me);
    }


    /// <summary>
    /// スケール
    /// </summary>
    /// <returns></returns>
    public HitZone.HitDistanceScale GetHitDistanceScale()
    {
        return Scale();
    }


    public int GetHitObjectLayerNumber()
    {
        if (ColliderObjectLayerNumber != null)
        {
            int layerNumber = 1;
            for(int i = 0; i < ColliderObjectLayerNumber(); i++)
            {
                layerNumber = layerNumber << 1;
            }
            return layerNumber;
        }
        return 0;
    }

    /// <summary>
    /// 無駄にすべてのオブジェクトを検索して比較しないようにするために使う関数
    /// </summary>
    /// <param name="contain"></param>
    /// <param name="me"></param>
    /// <param name="colliderObjectBases"></param>
    /// <returns></returns>
    private bool IsContain(ref Contain contain, Vector3 me, List<OriginalCollider> colliderObjectBases,ref Adjustment adjustment)
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
                adjustment = new Adjustment(colliderObjectBases[i].PositionAdjustmentPoint);
                return true;
            }
        }
        return false;
    }




    #endregion
}